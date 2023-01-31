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

namespace PX.Commerce.Custom
{
	public class CCLocationEntityBucket : EntityBucketBase, IEntityBucket
	{
		public IMappedEntity Primary => Location;
		public IMappedEntity[] Entities => new IMappedEntity[] { Location, Customer };

		public override IMappedEntity[] PreProcessors =>
			new IMappedEntity[] { Customer };

		public MappedLocation Location;
		public MappedCustomer Customer;
		public List<MappedAddress> Addresses;
		public MappedAddress Address;
	}

	public class CCLocationRestrictor : BCBaseRestrictor, IRestrictor
	{
		public virtual FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
		{
			return base.Restrict<MappedLocation>(mapped, delegate (MappedLocation obj)
			{
				BCBindingExt bindingExt = processor.GetBindingExt<BCBindingExt>();
				PX.Objects.AR.Customer guestCustomer = bindingExt.GuestCustomerID != null ? PX.Objects.AR.Customer.PK.Find((PXGraph)processor, bindingExt.GuestCustomerID) : null;

				// Do not export guest customer
				if (guestCustomer != null && obj.Local != null && obj.Local.Customer?.Value != null)
				{
					if (guestCustomer.AcctCD.Trim() == obj.Local.Customer?.Value)
					{
						return new FilterResult(FilterStatus.Invalid,
							PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogLocationSkippedGuest, obj.Local.LocationID?.Value ?? obj.Local.SyncID.ToString()));
					}
				}

				BCSyncStatus customeStatus = PXSelectJoin<BCSyncStatus,
					InnerJoin<PX.Objects.AR.Customer, On<BCSyncStatus.localID, Equal<PX.Objects.AR.Customer.noteID>>>,
					Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>,
						And<BCSyncStatus.connectorType, Equal<Required<BCSyncStatus.connectorType>>,
						And<BCSyncStatus.bindingID, Equal<Required<BCSyncStatus.bindingID>>,
						And<BCSyncStatus.entityType, Equal<Required<BCSyncStatus.entityType>>>>>>>
					.Select((PXGraph)processor, obj.Local?.Customer?.Value?.Trim(), processor.Operation.ConnectorType, processor.Operation.Binding, BCEntitiesAttribute.Customer);

				if (customeStatus?.ExternID == null)
				{
					//Skip if customer not synced
					return new FilterResult(FilterStatus.Invalid,
						PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogLocationSkippedCustomerNotSynced, obj.Local.Id?.ToString() ?? obj.Local.SyncID.ToString()));
				}

				return null;
			});
		}

		public virtual FilterResult RestrictImport(IProcessor processor, IMappedEntity mapped)
		{
			#region Locations
			return base.Restrict<MappedLocation>(mapped, delegate (MappedLocation obj)
			{
				if (processor.SelectStatus(CCEntitiesAttributes.Contact, obj.Extern?.Id?.ToString()) == null)
				{
					//Skip if contact not synced
					return new FilterResult(FilterStatus.Invalid,
						PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogLocationSkippedCustomerNotSynced, obj.Extern?.Id?.ToString(), obj.Extern?.Id?.ToString()));
				}

				return null;
			});
			#endregion
		}
	}

	[BCProcessor(typeof(CCConnector), BCEntitiesAttribute.Address, BCCaptions.Address,
		IsInternal = false,
		Direction = SyncDirection.Bidirect,
		PrimaryDirection = SyncDirection.Export,
		PrimarySystem = PrimarySystem.Local,
		PrimaryGraph = typeof(PX.Objects.AR.CustomerLocationMaint),
		ExternTypes = new Type[] { typeof(API.REST.LocationData) },
		LocalTypes = new Type[] { typeof(CustomerLocation) },
		AcumaticaPrimaryType = typeof(PX.Objects.CR.Location),
		URL = "companyAccounts/account/?id={0}",
		Requires = new string[] { CCEntitiesAttributes.Customer },
		ParentEntity = CCEntitiesAttributes.Customer
	)]
	public class CCLocationProcessor : CCLocationBaseProcessor<CCLocationProcessor, CCLocationEntityBucket, MappedLocation>, IProcessor
	{

		#region Constructor
		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);
		}
		#endregion
		public override MappedLocation PullEntity(string externID, string externalInfo)
		{
			throw new NotImplementedException();
		}

		public override MappedLocation PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
		{
			throw new NotImplementedException();
		}

		#region Import
		public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
		}
		public override EntityStatus GetBucketForImport(CCLocationEntityBucket bucket, BCSyncStatus syncstatus)
		{
            throw new NotImplementedException();

        }

        public override void MapBucketImport(CCLocationEntityBucket bucket, IMappedEntity existing)
		{
            throw new NotImplementedException();

        }
        public override void SaveBucketImport(CCLocationEntityBucket bucket, IMappedEntity existing, String operation)
		{
            throw new NotImplementedException();

        }
        #endregion

        #region Export
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
            throw new NotImplementedException();

        }
        public override EntityStatus GetBucketForExport(CCLocationEntityBucket bucket, BCSyncStatus syncstatus)
		{
			CustomerLocation locationImpl = cbapi.GetByID<CustomerLocation>(syncstatus.LocalID, GetCustomFieldsForExport());
			//Core.API.Address addressImpl = cbapi.GetByID<Core.API.Address>(syncstatus.LocalID, GetCustomFieldsForExport());

			if (locationImpl == null) return EntityStatus.None;

			//Location
			MappedLocation locationObj = bucket.Location = bucket.Location.Set(locationImpl, locationImpl.SyncID, locationImpl.SyncTime);
			EntityStatus status = EnsureStatus(bucket.Location, SyncDirection.Export);

			//Customer
			BCSyncStatus customerStatus = PXSelectJoin<BCSyncStatus,
				InnerJoin<PX.Objects.AR.Customer, On<BCSyncStatus.localID, Equal<PX.Objects.AR.Customer.noteID>>>,
				Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>,
					And<BCSyncStatus.connectorType, Equal<Required<BCSyncStatus.connectorType>>,
					And<BCSyncStatus.bindingID, Equal<Required<BCSyncStatus.bindingID>>,
					And<BCSyncStatus.entityType, Equal<Required<BCSyncStatus.entityType>>>>>>>
				.Select(this, locationObj.Local?.Customer?.Value?.Trim(), syncstatus.ConnectorType, syncstatus.BindingID, BCEntitiesAttribute.Customer);

	
			if (customerStatus == null) throw new PXException(BCMessages.CustomerNotSyncronized, locationImpl.Id?.ToString());

			Customer customerImpl = cbapi.GetByID<Customer>(customerStatus.LocalID, GetCustomFieldsForExport());

			MappedCustomer customerObj = bucket.Customer = bucket.Customer.Set(customerImpl, customerImpl.SyncID, customerStatus.LocalTS);

			customerObj.AddExtern(customerObj.Extern, customerStatus.ExternID, customerStatus.ExternTS);
			locationObj.ParentID = customerStatus.SyncID;

			//Addresses
			if (locationObj.Local.AddressOverride?.Value != true)
            {
				Address addressImpl = cbapi.GetByID<Address>(customerObj.Local.ShippingContact.Id, GetCustomFieldsForExport());

				MappedAddress addressObj = bucket.Address = bucket.Address.Set(addressImpl, addressImpl.SyncID, addressImpl.SyncTime);

				addressObj.AddExtern(addressObj.Extern, addressObj.ExternID, addressImpl.SyncTime);
				addressObj.ParentID = customerStatus.SyncID;
			}

			return status;
		}

		public override void MapBucketExport(CCLocationEntityBucket bucket, IMappedEntity existing)
		{
			MappedLocation addressObj = bucket.Location;
			MappedCustomer customerObj = bucket.Customer;

			if (customerObj == null || customerObj.ExternID == null) throw new PXException(BCMessages.CustomerNotSyncronized, addressObj.Local.Customer.Value);

			CustomerLocation locationImpl = addressObj.Local;

			LocationData locationData = new LocationData();

			addressObj.Extern = MapLocationExport(addressObj, customerObj);

		}

		public override void SaveBucketExport(CCLocationEntityBucket bucket, IMappedEntity existing, String operation)
		{
			MappedLocation obj = bucket.Location;

            LocationData locationData = null;
			if (obj.Extern.Id == null)
				obj.Extern.Id = bucket.Customer.ExternID;

			try
			{
				if (obj.ExternID == null || existing == null)
					locationData = locationDataProvider.Create(obj.Extern, bucket.Customer.ExternID);
				else
					locationData = locationDataProvider.Update(obj.Extern, obj.ExternID.KeySplit(0), obj.ExternID.KeySplit(1));

				obj.Extern = locationData;
				if (obj.Local.Active?.Value == false)
				{
					obj.Local.Active = true.ValueField();
					CustomerLocation addressImpl = cbapi.Put<CustomerLocation>(obj.Local, obj.LocalID);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			obj.AddExtern(locationData, new object[] { obj.Extern.Id, locationData.Id }.KeyCombine(), locationData.CalculateHash());
			UpdateStatus(obj, operation);
		}
		#endregion
	}
}
