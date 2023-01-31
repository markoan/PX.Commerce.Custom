using PX.Api.ContractBased.Models;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
    //EntityBucket to map 
    public class CCAccountEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary => Customer;
        public IMappedEntity[] Entities => new IMappedEntity[] { Customer, Location };

        public CustomerLocation ConnectorGeneratedAddress;
        public MappedCustomer Customer;
        public List<MappedLocation> Locations;
        public MappedLocation Location;

        public List<MappedAddress> Addresses;
        public MappedAddress Address;


        public override IMappedEntity[] PreProcessors =>
            new IMappedEntity[] { Contact };

        public CustomerContact ConnectorCustomerContact;
        public MappedContact Contact;
        public List<MappedContact> Contacts;
        public List<CustomerSalesPerson> SalesPersons;
    }

    public class CCAccountRestrictor : BCBaseRestrictor, IRestrictor
    {
        public virtual FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
        {
            return base.Restrict<MappedCustomer>(mapped, delegate (MappedCustomer obj)
            {
                BCBindingExt bindingExt = processor.GetBindingExt<BCBindingExt>();
                PX.Objects.AR.Customer guestCustomer = bindingExt.GuestCustomerID != null ? PX.Objects.AR.Customer.PK.Find((PXGraph)processor, bindingExt.GuestCustomerID) : null;

                if (guestCustomer != null && obj.Local != null && obj.Local.CustomerID?.Value != null)
                {
                    if (guestCustomer.AcctCD.Trim() == obj.Local.CustomerID?.Value)
                    {
                        return new FilterResult(FilterStatus.Invalid,
                            PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogCustomerSkippedGuest, obj.Local.CustomerID?.Value ?? obj.Local.SyncID.ToString()));
                    }
                }
                return null;
            });
        }

        public virtual FilterResult RestrictImport(IProcessor processor, IMappedEntity mapped)
        {
            return null;
        }
    }


    [BCProcessor(typeof(CCConnector), CCEntitiesAttributes.Customer, "Customers",
          IsInternal = false,
          Direction = SyncDirection.Export,
          PrimaryDirection = SyncDirection.Export,
          PrimarySystem = PrimarySystem.Local,
          PrimaryGraph = typeof(PX.Objects.AR.CustomerMaint),
          ExternTypes = new Type[] { typeof(AccountData) },
          LocalTypes = new Type[] { typeof(Customer) },
          AcumaticaPrimaryType = typeof(PX.Objects.AR.Customer),
          AcumaticaPrimarySelect = typeof(PX.Objects.AR.Customer.acctCD)
      )]
    [BCProcessorRealtime(PushSupported = true, HookSupported = false)]
    public class CCAccountProcessor : CCLocationBaseProcessor<CCAccountProcessor, CCAccountEntityBucket, MappedCustomer>, IProcessor
    {
        bool isLocationActive;
        protected List<string> SalesPersons;

        public PXSelect<PX.Objects.AR.ARStatementCycle> statementCycles;

        #region Initialization
        public override void Initialise(IConnector iconnector, ConnectorOperation operation)
        {
            base.Initialise(iconnector, operation);
            currentBinding = GetBinding();
            isLocationActive = ConnectorHelper.GetConnectorBinding(operation.ConnectorType, operation.Binding).ActiveEntities.Any(x => x == BCEntitiesAttribute.Address);

            if (isLocationActive)
            {
                locationProcessor = PXGraph.CreateInstance<CCLocationProcessor>();
                ((CCLocationProcessor)locationProcessor).Initialise(iconnector, operation.Clone().With(_ => { _.EntityType = BCEntitiesAttribute.Address; return _; }));
            }

            helper.Initialize(this);
        }
        #endregion Initialization

        #region Export Process 
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            //Getting all customers from Acumatica system
            IEnumerable<Core.API.Customer> acumCustomers = cbapi.GetAll<Core.API.Customer>(new Core.API.Customer { CustomerID = new StringReturn() },
                minDateTime, maxDateTime, filters);

            int countNum = 0;
            List<IMappedEntity> mappedList = new List<IMappedEntity>();

            //Create a bucket for each Acumatica customer
            foreach (Core.API.Customer customer in acumCustomers)
            {
                IMappedEntity mappedObj = new MappedCustomer(customer, customer.SyncID, customer.SyncTime);

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

        public override EntityStatus GetBucketForExport(CCAccountEntityBucket bucket, BCSyncStatus bcstatus)
        {
            //Search customer in Acumatica
            Core.API.Customer customer = BCExtensions.GetSharedSlot<Core.API.Customer>(bcstatus.LocalID.ToString()) ?? cbapi.GetByID<Core.API.Customer>(bcstatus.LocalID, GetCustomFieldsForExport());

            if (customer == null) return EntityStatus.None;
            //Create the bucket
            MappedCustomer mappedCustomer = bucket.Customer = bucket.Customer.Set(customer, customer.SyncID, customer.SyncTime);
            EntityStatus status = EnsureStatus(bucket.Customer, SyncDirection.Export);

            return status;
        }

        public override void MapBucketExport(CCAccountEntityBucket bucket, IMappedEntity existing)
        {

            //Getting objects from the EntityBucket
            MappedCustomer customerObj = bucket.Customer;

            List<MappedContact> contactsBucket = bucket.Contacts;

            Customer customerImpl = customerObj.Local;
            Core.API.Contact contactImpl = customerImpl.MainContact;
            Core.API.Contact primaryContact = customerImpl.PrimaryContact;
            Core.API.Address addressImpl = contactImpl.Address;
            
            //Create a new account
            AccountData accountData = customerObj.Extern = new AccountData();
           
            // Set values in account object for export
            accountData.Id = customerObj.ExternID;
            accountData.Name = customerImpl.CustomerName.Value;

            /// ANy business logic here

        }

        public override void SaveBucketExport(CCAccountEntityBucket bucket, IMappedEntity existing, string operation)
        {
            // Sample definition

            MappedCustomer obj = bucket.Customer;

            AccountData customerData = null;

            try
            {
                if (obj.ExternID == null || existing == null)
                {
                    customerData = accountDataProvider.Create(obj.Extern);
                }
                else
                {
                    customerData = accountDataProvider.Update(obj.Extern, obj.ExternID);
                }
                obj.Extern = customerData;
                obj.AddExtern(customerData, customerData.Id?.ToString(), customerData.UpdatedAt.ToDate(false));
                UpdateStatus(obj, operation);
            }
            catch (Exception ex)
            {
                throw;
            }

            // update ExternalRef
            string externalRef = APIHelper.ReferenceMake(customerData.Id?.ToString(), GetBinding().BindingName);

            string[] keys = obj.Local?.AccountRef?.Value?.Split(';');
            if (keys?.Contains(externalRef) != true)
            {
                if (!string.IsNullOrEmpty(obj.Local?.AccountRef?.Value))
                    externalRef = new object[] { obj.Local?.AccountRef?.Value, externalRef }.KeyCombine();

                if (externalRef.Length < 50)
                    PXDatabase.Update<BAccount>(
                                      new PXDataFieldAssign(typeof(BAccount.acctReferenceNbr).Name, PXDbType.NVarChar, externalRef),
                                      new PXDataFieldRestrict(typeof(BAccount.noteID).Name, PXDbType.UniqueIdentifier, obj.Local.NoteID?.Value)
                                      );
            }
        }
        #endregion Export Process 



        #region Import process
        public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            // Example implementation (update to your needs)

            int count = 0;
            List<IMappedEntity> mappedList = new List<IMappedEntity>();
            IEnumerable<AccountData> accounts = accountDataProvider.GetAll();
            try
            {
                foreach (AccountData account in accounts)
                {
                    if (string.IsNullOrEmpty(account.Name))
                    {
                        LogError(Operation.LogScope(), "Account name cannot be empty", account.Id);
                        continue;
                    }

                    IMappedEntity mappedAcc = new MappedCustomer(account, account.Id, account.UpdatedAt.ToDate(false));
                    mappedList.Add(mappedAcc);
                    count++;
                    if (count % BatchFetchCount == 0)
                    {
                        ProcessMappedListForImport(ref mappedList, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (mappedList.Any())
                {
                    ProcessMappedListForImport(ref mappedList, true);
                }
            }
        }

        public override EntityStatus GetBucketForImport(CCAccountEntityBucket bucket, BCSyncStatus bcstatus)
        {
            AccountData data = BCExtensions.GetSharedSlot<AccountData>(bcstatus.ExternID) ?? accountDataProvider.GetByID(bcstatus.ExternID);
            if (data == null) return EntityStatus.None;

            MappedCustomer obj = bucket.Customer = bucket.Customer.Set(data, data.Id?.ToString(), data.UpdatedAt.ToDate(false));
            EntityStatus status = EnsureStatus(obj, SyncDirection.Import);

            return status;
        }

        public override void MapBucketImport(CCAccountEntityBucket bucket, IMappedEntity existing)
        {

            throw new NotImplementedException();
        }

        public override void SaveBucketImport(CCAccountEntityBucket bucket, IMappedEntity existing, string operation)
        {

            throw new NotImplementedException();
        }

        #endregion Import Process
        public override MappedCustomer PullEntity(string externID, string externalInfo)
        {
            throw new NotImplementedException();
        }

        public override MappedCustomer PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
        {
            throw new NotImplementedException();
        }

    }

}
