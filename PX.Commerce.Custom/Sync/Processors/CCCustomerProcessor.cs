using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Common;
using System.Reflection;
using PX.Api.ContractBased.Models;
using Location = PX.Objects.CR.Standalone.Location;
using Newtonsoft.Json;
using System.Net;
using CommerceContact = PX.Commerce.Core.API.Contact;
using AcumContact = PX.Objects.CR.Contact;

namespace PX.Commerce.Custom
{
	//Bucket for customer entity, including the Location
	public class CCCustomerEntityBucket : EntityBucketBase, IEntityBucket
	{
		public IMappedEntity Primary { get => Contact; }
		public IMappedEntity[] Entities => new IMappedEntity[] { Contact, ContactAddress };
		public CustomerLocation ConnectorGeneratedAddress;
		public MappedContact Contact;
		public List<MappedContactAddress> ContactAddresses;
		public MappedContactAddress ContactAddress;
		public MappedCustomer Customer;
	}

	public class CCCustomerRestrictor : BCBaseRestrictor, IRestrictor
	{
		
		public virtual FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
		{
				return null;
		}
		
		public virtual FilterResult RestrictImport(IProcessor processor, IMappedEntity mapped)
		{
			return null;
		}
	}

	[BCProcessor(typeof(CCConnector), CCEntitiesAttributes.Contact, "Contacts",
		IsInternal = false,
		Direction = SyncDirection.Bidirect,
		PrimaryDirection = SyncDirection.Export,
		PrimarySystem = PrimarySystem.Local,
		PrimaryGraph = typeof(PX.Objects.CR.ContactMaint),
		ExternTypes = new Type[] { typeof(ContactData) },
		LocalTypes = new Type[] { typeof(Core.API.Contact) },
		AcumaticaPrimaryType = typeof(PX.Objects.CR.Contact),
		AcumaticaPrimarySelect = typeof(PX.Objects.CR.Contact.contactID)
	)]
	[BCProcessorRealtime(PushSupported = true, HookSupported = false)]
	public class CCCustomerProcessor : BCProcessorSingleBase<CCCustomerProcessor, CCCustomerEntityBucket, MappedContact>, IProcessor
	{
		protected AccountRestDataProvider accountDataProvider;
		protected CustomerRestDataProvider customerDataProvider;
		protected BCBinding currentBinding;
		protected CCLocationProcessor addressProcessor;
		protected CCRestClient client;


		bool isLocationActive;
		public CCHelper helper = PXGraph.CreateInstance<CCHelper>();

		#region Initialization
		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);
			currentBinding = GetBinding();
			client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());
			customerDataProvider = new CustomerRestDataProvider(client);

			addressProcessor = PXGraph.CreateInstance<CCLocationProcessor>();
			((CCLocationProcessor)addressProcessor).Initialise(iconnector, operation.Clone().With(_ => { _.EntityType = BCEntitiesAttribute.Address; return _; }));

			accountDataProvider = new AccountRestDataProvider(client);

			helper.Initialize(this);
		}
		#endregion

		#region Common
		public override PXTransactionScope WithTransaction(System.Action action)
		{
			action();
			return null;
		}


		public override MappedContact PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
		{
			Core.API.Contact impl = cbapi.GetByID<Core.API.Contact>(localID);
			if (impl == null) return null;

			MappedContact obj = new MappedContact(impl, impl.SyncID, impl.SyncTime);

			return obj;
		}

		public override MappedContact PullEntity(String externID, String externalInfo)
		{
			ContactData data = customerDataProvider.GetByID(externID);
			if (data == null) return null;

			MappedContact obj = new MappedContact(data, data.Id?.ToString(), data.DateModifiedAt.ToDate(false));
			return obj;
		}

		public override IEnumerable<MappedContact> PullSimilar(IExternEntity entity, out String uniqueField)
		{
			uniqueField = ((ContactData)entity)?.Email;

			List<MappedContact> result = new List<MappedContact>();
			if (uniqueField != null)
			{
				foreach (PX.Objects.CR.Contact item in helper.ContactsByEmail.Select(uniqueField, "PN"))
				{
					PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact)item;
					Core.API.Contact data = new Core.API.Contact()
					{
						SyncID = contact.NoteID,
						FirstName = contact.FirstName.ValueField(),
						LastName = contact.LastName.ValueField(),
						SyncTime = contact.LastModifiedDateTime
					};
					result.Add(new MappedContact(data, data.SyncID, data.SyncTime));
					BCExtensions.SetSharedSlot<Core.API.Contact>(this.GetType(), data.SyncID?.ToString(), data);
				}
			}
			else if (uniqueField == null)
			{
				uniqueField = ((ContactData)entity)?.Phone ?? "";
				foreach (PX.Objects.CR.Contact item in helper.ContactsByPhone.Select(uniqueField, uniqueField))
				{
					PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact)item;
					Core.API.Contact data = new Core.API.Contact()
					{
						FirstName = contact.FirstName.ValueField(),
						LastName = contact.LastName.ValueField(),
						SyncTime = contact.LastModifiedDateTime
					};
					result.Add(new MappedContact(data, data.SyncID, data.SyncTime));
					BCExtensions.SetSharedSlot<Core.API.Contact>(this.GetType(), data.SyncID?.ToString(), data);
				}
			}
			else
				return null;

			if (result == null || result?.Count == 0) return null;

			return result;
		}
		public override IEnumerable<MappedContact> PullSimilar(ILocalEntity entity, out String uniqueField)
		{
			uniqueField = ((Core.API.Contact)entity)?.Email?.Value;
			string queryField = string.Empty;

			if (uniqueField != null)
			{
				queryField = nameof(ContactData.Email);
			}
			else if (uniqueField == null && !string.IsNullOrWhiteSpace(((Core.API.Contact)entity)?.Phone1?.Value))
			{
				queryField = nameof(ContactData.Phone);
				uniqueField = ((Core.API.Contact)entity)?.Phone1?.Value;
			}
			else
				return null;

			IEnumerable<ContactData> datas = customerDataProvider.GetByQuery(queryField, uniqueField);
			if (datas == null) return null;
			
			return datas.Select(data => new MappedContact(data, data.Id.ToString(), data.DateModifiedAt.ToDate(false)));
		}
		
		#endregion

		#region Import
		public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
			//Use API to get all the customers from external system
			IEnumerable<ContactData> datas = customerDataProvider.GetAll();
			int countNum = 0;
			//List to save customer buckets 
			List<IMappedEntity> mappedList = new List<IMappedEntity>();
			try
			{
				foreach (ContactData data in datas)
				{
					
					if (string.IsNullOrEmpty(data.FirstName) && string.IsNullOrEmpty(data.LastName))
					{
						LogWarning(Operation.LogScope(), BCMessages.CustomerNameIsEmpty, data.Id);
						continue;
					}

					//Create a bucket for each customer 
					CCCustomerEntityBucket bucket = CreateBucket();
					MappedContact obj = bucket.Contact = bucket.Contact.Set(data, data.Id?.ToString(), data.DateModifiedAt.ToDate(false));

					mappedList.Add(obj);
					countNum++;

					if(countNum % BatchFetchCount == 0)
                    {
						ProcessMappedListForImport(ref mappedList, true);
                    }
					//Set pending status for that row 
					EntityStatus status = EnsureStatus(obj, SyncDirection.Import);
				}
			} catch (Exception e)
            {
				LogError(Operation.LogScope(), "Cannot fetch buckets for import", e.Message);
			} finally
            {
				if(mappedList.Any())
                {
					ProcessMappedListForImport(ref mappedList, true);
                }
            }
		}

		public override EntityStatus GetBucketForImport(CCCustomerEntityBucket bucket, BCSyncStatus bcstatus)
		{
			ContactData data = BCExtensions.GetSharedSlot<ContactData>(bcstatus.ExternID) ?? customerDataProvider.GetByID(bcstatus.ExternID, 
				includeAllAddresses: true);

			MappedContact obj = bucket.Contact = bucket.Contact.Set(data, data.Id?.ToString(), data.DateModifiedAt.ToDate(false));
			
			EntityStatus status = EnsureStatus(obj, SyncDirection.Import);

			return status;
		}

		public override void MapBucketImport(CCCustomerEntityBucket bucket, IMappedEntity existing)
		{
			MappedContact mappedObj = bucket.Contact;
			//Create a new local customer and assign it to our bucket
			CommerceContact acumContact = mappedObj.Local = new CommerceContact();
			acumContact.Custom = GetCustomFieldsForImport();

			CommerceContact presented = existing?.Local != null ? existing?.Local as CommerceContact : null;

			//Checkin if there is an existing contact
			AcumContact contact = PXSelect<AcumContact, 
				Where<AcumContact.noteID, Equal<Required<AcumContact.noteID>>>>.Select(this, mappedObj.LocalID);

			//General Customer Info
			string firstLastName = (mappedObj.Extern.FirstName?.Equals(mappedObj.Extern.LastName, StringComparison.InvariantCultureIgnoreCase)) == false ?
				(String.Concat(mappedObj.Extern.FirstName ?? string.Empty, " ", mappedObj.Extern.LastName ?? string.Empty)).Trim() : mappedObj.Extern.FirstName ?? string.Empty;

			acumContact.ContactClass = presented != null ? presented.ContactClass : (mappedObj.LocalID == null || existing?.Local == null ? helper.GetSubstituteLocalByExtern("EXTCONTACTCLASS", GetBindingExt<BCBindingExt>().CustomerClassID?.ValueField().Value, "CUSTOM") : null).ValueField();

			bool noEmail = string.IsNullOrWhiteSpace(mappedObj.Extern.Email);

			acumContact.FirstName = mappedObj.Extern.FirstName.ValueField();
			acumContact.LastName = mappedObj.Extern.LastName.ValueField();
			acumContact.Attention = firstLastName.ValueField();
			acumContact.Email = mappedObj.Extern.Email.ValueField();
			acumContact.Phone1 = mappedObj.Extern.Phone.ValueField();
			acumContact.Active = true.ValueField();
			acumContact.DoNotEmail = (noEmail ? false : true).ValueField();
			acumContact.Active = true.ValueField();
			

			//Address
			Core.API.Address addressImpl = acumContact.Address = new Core.API.Address();
			bucket.ContactAddresses = new List<MappedContactAddress>();
			StringValue shipVia = null;
			BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();
			if (bindingExt.CustomerClassID != null)
			{
				PX.Objects.AR.CustomerClass customerClass = PXSelect<PX.Objects.AR.CustomerClass, Where<PX.Objects.AR.CustomerClass.customerClassID, 
					Equal<Required<PX.Objects.AR.CustomerClass.customerClassID>>>>.Select(this, bindingExt.CustomerClassID);
				if (customerClass != null)
				{
					addressImpl.Country = customerClass.CountryID.ValueField(); // no address is present then set country from customer class
					shipVia = customerClass.ShipVia.ValueField();
				}
			}

            CAddressDataSingle addressObj = null;

			//acumCustomer.CustomerName = (firstLastName ?? addressObj?.Company ?? mappedCustomer.ExternID.ToString()).ValueField();
			acumContact.CompanyName = acumContact.FullName = acumContact.FirstName;
		}


		public override void SaveBucketImport(CCCustomerEntityBucket bucket, IMappedEntity existing, String operation)
		{
			MappedContact mappedObj = bucket.Contact;
			CommerceContact impl = null;

			PX.Objects.CR.ContactMaint graph = PXGraph.CreateInstance<PX.Objects.CR.ContactMaint>();

			//graph.Contact.Current = PXSelect<AcumContact, Where<AcumContact.contactID,
				//					 Equal<Required<AcumContact.contactID>>>>.Select(graph, mappedObj.LocalID.Value);

			PXSelectBase<PX.Objects.CR.CRContactClass> custClass = new PXSelect<CRContactClass,
				Where<PX.Objects.AR.CustomerClass.customerClassID, Equal<Required<PX.Objects.AR.CustomerClass.customerClassID>>>>(graph);

			//graph.Contact.Current.d = ((PX.Objects.AR.CustomerClass)custClass.Select(impl.CustomerClass)).CustomerClassID.ToInt();

			//After customer created successfully, it ensures system couldn't rollback customer creation if customer location fails later.
			using (var transaction = base.WithTransaction(delegate ()
			{
				impl = cbapi.Put<CommerceContact>(mappedObj.Local, mappedObj.LocalID);
				mappedObj.AddLocal(impl, impl.SyncID, impl.SyncTime);
				UpdateStatus(mappedObj, operation);
			})) { transaction?.Complete(); };

			Location location = PXSelectJoin<Location,
			InnerJoin<PX.Objects.CR.Contact, On<Location.locationID, Equal<PX.Objects.CR.Contact.defAddressID>>>,
			Where<AcumContact.noteID, Equal<Required<AcumContact.noteID>>>>.Select(this, impl.SyncID);

			if (bucket.ConnectorGeneratedAddress != null && location != null)
			{
				CustomerLocation addressImpl = cbapi.Put<CustomerLocation>(bucket.ConnectorGeneratedAddress, location?.NoteID);
			}
			if (isLocationActive)
			{
				bool lastmodifiedUpdated = false;
				List<Location> locations;
				graph = PXGraph.CreateInstance<PX.Objects.CR.ContactMaint>();
				graph.Contact.Current = PXSelect<AcumContact, Where<AcumContact.contactID,
										 Equal<Required<AcumContact.contactID>>>>.Select(graph, impl.ContactID.Value);

				locations = graph.GetExtension<PX.Objects.AR.CustomerMaint.LocationDetailsExt>().Locations.Select().RowCast<Location>().ToList();
				//create/update other address and create status line(including Main)
				var alladdresses = SelectStatusChildren(mappedObj.SyncID).Where(s => s.EntityType == BCEntitiesAttribute.Address)?.ToList();

				if (bucket.ContactAddresses.Count > 0)
				{
					if (alladdresses.Count == 0)// we copy into main if its first location
					{
						var main = bucket.ContactAddresses.FirstOrDefault(x => x.Extern.IsDefaultBilling == true);

						if (location != null && main.LocalID == null)
						{
							main.LocalID = location.NoteID; //if location already created
						}
					}
				}

				foreach (var loc in bucket.ContactAddresses)
				{
					try
					{
						//loc.Local.Customer = impl.ContactID.ToString().ValueField();
						var status = EnsureStatus(loc, SyncDirection.Import, persist: false);
					if (loc.LocalID != null && !locations.Any(x => x.NoteID == loc.LocalID)) continue; // means deletd location
						if (status == EntityStatus.Pending || Operation.SyncMethod == SyncMode.Force)
						{
							lastmodifiedUpdated = true;
							Core.API.Address addressImpl = cbapi.Put<Core.API.Address>(loc.Local, loc.LocalID);

							loc.AddLocal(addressImpl, addressImpl.SyncID, addressImpl.SyncTime);
							UpdateStatus(loc, operation);
						}
					}
					catch (Exception ex)
					{
						LogError(Operation.LogScope(loc), ex);
						UpdateStatus(loc, BCSyncOperationAttribute.LocalFailed, message: ex.Message);
					}

				}
				DateTime? date = null;
				bool updated = UpdateDefault(bucket, locations, graph);
				if (lastmodifiedUpdated || updated)
				{
					date = helper.GetUpdatedDate(impl.ContactID?.ToString(), date);
				}
				mappedObj.AddLocal(impl, impl.SyncID, date ?? graph.Contact.Current?.LastModifiedDateTime ?? impl.SyncTime);
				UpdateStatus(mappedObj, operation);

			}
		}



		protected virtual bool UpdateDefault(CCCustomerEntityBucket bucket, List<Location> locations, PX.Objects.CR.ContactMaint graph)
		{
			var obj = bucket.Contact;
			bool updated = false;
			if (obj.LocalID != null)
			{
				var addressReferences = SelectStatusChildren(obj.SyncID).Where(s => s.EntityType == BCEntitiesAttribute.Address && s.Deleted == false)?.ToList();
				var deletedValues = addressReferences?.Where(x => bucket.ContactAddresses.All(y => x.ExternID != y.ExternID)).ToList();

				var mappeddefault = bucket.ContactAddresses.FirstOrDefault(x => x.Extern.IsDefaultBilling == true); // get new default mapped address

				if (deletedValues != null && deletedValues.Count > 0)
				{


					updated = true;
				}
			}
			return updated;
		}



		#endregion

		#region Export
		public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
			//Getting all contacts from Acumatica systema
			IEnumerable<CommerceContact> acumContacts = cbapi.GetAll<CommerceContact>(new CommerceContact { ContactID = new IntReturn() }, 
				minDateTime, maxDateTime, filters);

			int countNum = 0;
			List<IMappedEntity> mappedList = new List<IMappedEntity>();

			//Create a bucket for each Acumatica customer
			foreach (CommerceContact contact in acumContacts)
			{
				IMappedEntity mappedObj = new MappedContact(contact, contact.SyncID, contact.SyncTime);

				mappedList.Add(mappedObj);
				countNum++;
				if (countNum % BatchFetchCount == 0)
				{
					ProcessMappedListForExport(ref mappedList);
				}
			}
			if (mappedList.Any())
			{
				ProcessMappedListForExport(ref mappedList);
			}
		}

		public override EntityStatus GetBucketForExport(CCCustomerEntityBucket bucket, BCSyncStatus bcstatus)
		{
			//Search customer in Acumatica
			CommerceContact contact = BCExtensions.GetSharedSlot<CommerceContact>(bcstatus.LocalID.ToString()) ?? cbapi.GetByID<CommerceContact>(bcstatus.LocalID, GetCustomFieldsForExport());
			
			if (contact == null) return EntityStatus.None;
			//Create the bucket
			MappedContact mappedCustomer = bucket.Contact = bucket.Contact.Set(contact, contact.SyncID, contact.SyncTime);
			EntityStatus status = EnsureStatus(bucket.Contact, SyncDirection.Export);

			//Customer
			BCSyncStatus customerStatus = PXSelectJoin<BCSyncStatus,
				InnerJoin<PX.Objects.AR.Customer, On<BCSyncStatus.localID, Equal<PX.Objects.AR.Customer.noteID>>>,
				Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>,
					And<BCSyncStatus.connectorType, Equal<Required<BCSyncStatus.connectorType>>,
					And<BCSyncStatus.bindingID, Equal<Required<BCSyncStatus.bindingID>>,
					And<BCSyncStatus.entityType, Equal<Required<BCSyncStatus.entityType>>>>>>>
				.Select(this, mappedCustomer.Local?.BusinessAccount?.Value?.Trim(), bcstatus.ConnectorType, bcstatus.BindingID, BCEntitiesAttribute.Customer);

			if (customerStatus != null)
            {
				Customer customerImpl = cbapi.GetByID<Customer>(customerStatus.LocalID, GetCustomFieldsForExport());

				MappedCustomer customerObj = bucket.Customer = bucket.Customer.Set(customerImpl, customerImpl.SyncID, customerStatus.LocalTS);

				customerObj.AddExtern(customerObj.Extern, customerStatus.ExternID, customerStatus.ExternTS);
			}

			return status;
		}


		public override void MapBucketExport(CCCustomerEntityBucket bucket, IMappedEntity existing)
		{
			//Getting objects from the EntityBucket
			MappedContact mappedObj = bucket.Contact;
			MappedContactAddress address = bucket.ContactAddress;
			MappedCustomer account = bucket.Customer;

			//Setting Acumatica customer to an object
			CommerceContact acumContact = mappedObj.Local;
			Core.API.Address addressImpl = acumContact.Address;

			//Create a new external customer
			ContactData customerData = mappedObj.Extern = new ContactData();

			//Customer
			customerData.Id = mappedObj.ExternID?.ToLong();

			//Contact			
			customerData.FirstName = acumContact?.FirstName?.Value ?? acumContact?.LastName?.Value;
			customerData.LastName = acumContact?.LastName?.Value ?? acumContact?.FirstName?.Value;
			customerData.Email = acumContact.Email?.Value != null ? acumContact.Email?.Value : "";
			customerData.Phone = acumContact.Phone1?.Value ?? acumContact.Phone2?.Value;


			var addressReference = SelectStatusChildren(mappedObj.SyncID).Where(s => s.EntityType == BCEntitiesAttribute.Address)?.ToList();
			
			var mapped = addressReference?.FirstOrDefault(x => x.LocalID == addressImpl.Id.Value);
			if(mapped == null || (mapped!= null && mapped.Deleted == false) && addressImpl!=null)
            {
				MappedContactAddress mappedAddress = new MappedContactAddress(addressImpl, addressImpl.Id.Value, DateTime.UtcNow, mappedObj.SyncID);
				mappedAddress.Extern = MapLocationExport(mappedAddress, mappedObj);

				if(addressReference?.Count == 0 && existing != null)
                {
					//mappedLoc.ExternID = CheckIfExists(mappedLoc.Local, existing, bucket.ContactAddresses, acumContact.FullName?.Value);
                }

				bucket.ContactAddress = mappedAddress;
			}
			
			
			
		}


		public override object GetAttribute(CCCustomerEntityBucket bucket, string attributeID)
		{
			MappedContact obj = bucket.Contact;
			CommerceContact impl = obj.Local;
			return impl.Attributes?.FirstOrDefault(x => string.Equals(x?.AttributeDescription?.Value, attributeID, StringComparison.InvariantCultureIgnoreCase));

		}
		public override void AddAttributeValue(CCCustomerEntityBucket bucket, string attributeID, object attributeValue)
		{
			MappedContact obj = bucket.Contact;
			CommerceContact impl = obj.Local;
			impl.Attributes = impl.Attributes ?? new List<AttributeValue>();
			AttributeValue attribute = new AttributeValue();
			attribute.AttributeID = new StringValue() { Value = attributeID };
			attribute.ValueDescription = new StringValue() { Value = attributeValue?.ToString() };
			impl.Attributes.Add(attribute);
		}

		public override void SaveBucketExport(CCCustomerEntityBucket bucket, IMappedEntity existing, String operation)
		{
			//Setting the bucket to an object
			MappedContact mappedContact = bucket.Contact;
			MappedContactAddress mappedAddress = bucket.ContactAddress;

			//Customer
			ContactData customerData = mappedContact.Extern;
			string id, updateResult = "";
			try
			{
				
				//If not exists in the system, we create a new one
				if (mappedContact.ExternID == null || existing == null)
				{
					
					id = customerDataProvider.Create(mappedContact.Extern);
					customerData.Id = long.Parse(id);
					mappedContact.Extern.Id = long.Parse(id);
					mappedContact.ExternID = id;
					
				}
				else //Otherwise, we update it
				{
					CustomerPutJson putBody = new CustomerPutJson()
					{
						Id = Int32.Parse(mappedContact.ExternID),
						Customer = mappedContact.Extern
					
					};
					updateResult = customerDataProvider.Update(putBody);
					if (!HttpStatusCode.OK.ToString().Equals(updateResult))
					{
						throw new PXException(ConnectorMessages.UpdateError, "customer", updateResult);
					}
				}

				mappedContact.AddExtern(customerData, customerData.Id?.ToString(), customerData.DateModifiedAt.ToDate(false));
				UpdateStatus(mappedContact, operation);
			}
			catch (Exception ex)
			{
				throw;
			}

			// update ExternalRef
			string externalRef = APIHelper.ReferenceMake(customerData.Id?.ToString(), GetBinding().BindingName);

			string[] keys = mappedContact.Local?.ContactID?.ToString()?.Split(';');
		
			//Address
			bool locationUpdated = false;

            if (mappedContact.Local.Address != null) {
				MappedContactAddress address = mappedAddress;
				try
				{
					BCSyncStatus bCSyncStatus = null;
						if (address.Local != null)
						{
							bCSyncStatus = PXSelect<BCSyncStatus,
										Where<BCSyncStatus.connectorType, Equal<Current<BCEntity.connectorType>>,
										And<BCSyncStatus.bindingID, Equal<Current<BCEntity.bindingID>>,
										And<BCSyncStatus.entityType, Equal<Required<BCSyncStatus.entityType>>,
										And<BCSyncStatus.localID, Equal<Required<BCSyncStatus.localID>>>>>>>.Select(this, BCEntitiesAttribute.Address, address.LocalID);
						}

						var status = EnsureStatus(mappedAddress, SyncDirection.Export);

						if (status == EntityStatus.Pending || Operation.SyncMethod == SyncMode.Force)
						{
							string locOperation;
							locationUpdated = true;
							CAddressDataAll externalAddress = null;
							string result = string.Empty;
							if (mappedContact.Local.Address != null)
							{
								Core.API.Address acumAddress = mappedContact.Local.Address;
								//Build address data 
								externalAddress = new CAddressDataAll();
								CAddressDataSingle single = address.Extern = new CAddressDataSingle();
								externalAddress.Company = single.Company = mappedContact.Local.CompanyName.Value;
								externalAddress.FirstName = single.FirstName = mappedContact.Local.FirstName.Value;
								externalAddress.LastName = single.LastName = mappedContact.Local.LastName.Value;
								externalAddress.StreetArray = new List<string>();
								externalAddress.StreetArray.Add(string.IsNullOrEmpty(acumAddress.AddressLine1?.Value) == true ? string.Empty : acumAddress.AddressLine1?.Value);
								if (!string.IsNullOrEmpty(acumAddress.AddressLine2?.Value))
								{
									externalAddress.StreetArray.Add(acumAddress.AddressLine2?.Value);
								}
								single.Street = acumAddress.AddressLine1?.Value + " " + acumAddress.AddressLine2?.Value;
								externalAddress.City = single.City = acumAddress.City.Value;
								externalAddress.Region = single.Region = acumAddress.State.Value;
								externalAddress.CountryId = single.CountryCode = acumAddress.Country.Value;
								externalAddress.PostCode = single.PostalCode = acumAddress.PostalCode.Value == null ? string.Empty : acumAddress.PostalCode.Value;
								externalAddress.Telephone = single.Phone = string.Empty;
	
								externalAddress.IsDefaultBilling = 1;
								externalAddress.IsDefaultShipping = 1;
								single.IsDefaultBilling = true;
								single.IsDefaultShipping = true;
							}

							mappedAddress.AddExtern(address.Extern, address.ExternID.ToString(),DateTime.UtcNow);
							address.ParentID = mappedContact.SyncID;
					}
				}
				catch (Exception ex)
				{
					LogError(Operation.LogScope(address), ex);
					UpdateStatus(address, BCSyncOperationAttribute.ExternFailed, message: ex.Message);
				}
				if (locationUpdated)
				{
					customerData = customerDataProvider.GetByID(customerData.Id.ToString());
					mappedContact.AddExtern(customerData, customerData.Id?.ToString(), customerData.DateModifiedAt.ToDate(false));
				}

			}
			
			#endregion
		}

		//Helpers
		protected virtual CAddressDataSingle MapLocationExport(MappedContactAddress addressObj, MappedContact customerObj)
		{
			Core.API.Address locationImpl = addressObj.Local;
            CAddressDataSingle addressData = new CAddressDataSingle();

			var result = PXSelectJoin<PX.Objects.CR.Location,
			InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>,
			Where<PX.Objects.CR.BAccount.noteID, Equal<Required<PX.Objects.CR.BAccount.noteID>>>>.Select(this, customerObj.LocalID);


			//Contact
			Core.API.Contact contactImpl = customerObj.Local;
			addressData.Company = contactImpl.FullName?.Value ?? string.Empty;
			addressData.FirstName = contactImpl.Attention?.Value ?? string.Empty;
			addressData.Phone = contactImpl.Phone1?.Value ?? contactImpl.Phone2?.Value;

			//Address
			Core.API.Address addressImpl = contactImpl.Address;

			addressData.Street = string.Join("\n",new List<string> { addressImpl.AddressLine1?.Value, addressImpl.AddressLine2?.Value });

			addressData.Region = addressImpl.AddressLine2?.Value;
			addressData.City = addressImpl.City?.Value;
			addressData.CountryCode = addressImpl.Country?.Value;

			addressData.PostalCode = addressImpl.PostalCode?.Value;

			addressData.IsDefaultBilling = true;
			addressData.IsDefaultShipping = true;

			return addressData;
		}


	}
}
