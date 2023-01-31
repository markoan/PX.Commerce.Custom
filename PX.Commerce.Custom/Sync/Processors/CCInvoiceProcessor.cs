using PX.Api.ContractBased.Models;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.SO;
using PX.Common;
using PX.Data.Licensing;

namespace PX.Commerce.Custom
{
    //EntityBucket to map 
    public class CCInvoiceEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary { get => Invoice;  }
        public IMappedEntity[] Entities => new IMappedEntity[] { Primary };

        public MappedInvoice Invoice;
        public MappedOrder Order;

    }

	public class CCInvoiceRestrictor : BCBaseRestrictor, IRestrictor
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

	[BCProcessor(typeof(CCConnector), "IV", "Invoice",
          IsInternal = false,
          Direction = SyncDirection.Export,
          PrimaryDirection = SyncDirection.Export,
          PrimarySystem = PrimarySystem.Local,
          PrimaryGraph = typeof(PX.Objects.SO.SOInvoiceEntry),
          ExternTypes = new Type[] { typeof(InvoicePostData) },
          LocalTypes = new Type[] { typeof(Invoice) },
          AcumaticaPrimaryType = typeof(PX.Objects.SO.SOInvoice),
          AcumaticaPrimarySelect = typeof(Search<PX.Objects.SO.SOInvoice.refNbr, Where<PX.Objects.SO.SOInvoice.docType, Equal<ARDocType.invoice>>>),
          Requires = new string[] {BCEntitiesAttribute.Order}

      )]
    [BCProcessorRealtime(PushSupported = false, HookSupported = false)]
    public class CCInvoiceProcessor : BCProcessorSingleBase<CCInvoiceProcessor, CCInvoiceEntityBucket, MappedInvoice>, IProcessor
    {

        public CCHelper helper = PXGraph.CreateInstance<CCHelper>();

        protected OrderRestDataProvider orderDataProvider;
        protected PaymentRestDataProvider paymentDataProvider;
        

        protected BCBinding currentBinding;
        protected BCBindingExt currentBindingExt;
        protected BCBindingCustom currentCustomBinding;

        #region Constructor
        public override void Initialise(IConnector iconnector, ConnectorOperation operation)
        {
            base.Initialise(iconnector, operation);
            currentBinding = GetBinding();
            currentBindingExt = GetBindingExt<BCBindingExt>();
            currentCustomBinding = GetBindingExt<BCBindingCustom>();

            var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());
            orderDataProvider = new OrderRestDataProvider(client);
            paymentDataProvider = new PaymentRestDataProvider(client);

            helper.Initialize(this);
        }

        #endregion

        

        public override MappedInvoice PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
        {
            Invoice inv = cbapi.GetByID<Invoice>(localID);
            if (inv == null) return null;

            MappedInvoice obj = new MappedInvoice(inv, inv.SyncID, inv.SyncTime);

            return obj;
        }

        public override MappedInvoice PullEntity(string externID, string externalInfo)
        {
            throw new NotImplementedException();
        }

        #region Import
        public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForImport(CCInvoiceEntityBucket bucket, BCSyncStatus bcstatus)
        {
            throw new NotImplementedException();
        }

        public override void MapBucketImport(CCInvoiceEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketImport(CCInvoiceEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }
        #endregion Import

        #region Export
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            //Getting all the Invoice from Acumatica 
            IEnumerable<Invoice> acumInvoices = cbapi.GetAll<Invoice>(new Invoice { RefNbr = new StringReturn() },
                minDateTime, maxDateTime, filters);

            int countNum = 0;
            List<IMappedEntity> mappedList = new List<IMappedEntity>();

            //Create a bucket for each Acumatica Invoice
            foreach (Invoice item in acumInvoices)
            {
                // Skip not external store orders                         

                var soOrdShip =  SelectFrom<SOOrderShipment>.InnerJoin<ARRegister>.
                                On<SOOrderShipment.invoiceType.IsEqual<ARRegister.docType>.
                                And<SOOrderShipment.invoiceNbr.IsEqual<ARRegister.refNbr>>>.
                                Where<ARRegister.noteID.IsEqual<@P.AsGuid>>.View.
                                Select(this,item.SyncID.Value).AsEnumerable()
                                .Cast<PXResult<SOOrderShipment, ARRegister>>()
                                .ToArray();
  
                Boolean externalOrder = true;
                foreach ((var order, var inv) in soOrdShip)
                {
                    if (SelectStatus(BCEntitiesAttribute.Order, order.OrderNoteID) == null)
                    {
                        // Not external Order
                        externalOrder = false;
                        break;
                    }
                        
                }
                if (!externalOrder)
                {
                        continue;
                }
            

                IMappedEntity mappedObj = new MappedInvoice(item, item.SyncID, item.SyncTime);

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

        public override EntityStatus GetBucketForExport(CCInvoiceEntityBucket bucket, BCSyncStatus bcstatus)
        {
            //Search Invoice in Acumatica
            Invoice inv = BCExtensions.GetSharedSlot<Invoice>(bcstatus.LocalID.ToString()) ?? cbapi.GetByID<Invoice>(bcstatus.LocalID, GetCustomFieldsForExport());

            if (inv == null) return EntityStatus.None;
            //Create the bucket
            MappedInvoice mappedInv = bucket.Invoice = bucket.Invoice.Set(inv, inv.SyncID, inv.SyncTime);
            EntityStatus status = EnsureStatus(bucket.Invoice, SyncDirection.Export);

            return status;
        }

        public override void MapBucketExport(CCInvoiceEntityBucket bucket, IMappedEntity existing)
        {
            //Getting objects from the EntityBucket
            MappedInvoice invoiceObj = bucket.Invoice;

            Invoice invImpl = invoiceObj.Local;

            //Get Order Shipment
            SOOrderShipment soOrdShip = SelectFrom<SOOrderShipment>.InnerJoin<ARRegister>.
                               On<SOOrderShipment.invoiceType.IsEqual<ARRegister.docType>.
                               And<SOOrderShipment.invoiceNbr.IsEqual<ARRegister.refNbr>>>.
                               Where<ARRegister.noteID.IsEqual<@P.AsGuid>>.View.
                               SelectSingleBound(this, null,invoiceObj.LocalID.Value);

            // Get Order
            SOOrder order = SelectFrom<SOOrder>.Where<SOOrder.orderType.IsEqual<@P.AsString>.
                                And<SOOrder.orderNbr.IsEqual<@P.AsString>>>.View.
                                SelectSingleBound(this, null, soOrdShip.OrderType, soOrdShip.OrderNbr);

            string orderIncrementId = order?.CustomerRefNbr?.Split('-')[0].Trim();

            // Create s a external Invoice
            InvoicePostData invPost = invoiceObj.Extern = new InvoicePostData();

            invPost.IsEmail = 1;
            invPost.IsIncludeComment = 1;
            invPost.Comment = soOrdShip.InvoiceNbr;
            invPost.OrderNbr = orderIncrementId;
            invPost.ItemsList = new Dictionary<string, string>();

            // Invoice Details
            //Dictionary<string, string> itemsDict = new Dictionary<string, string>();

            PXResultset<ARTran> invDetails =  SelectFrom<ARTran>.
                                Where<ARTran.tranType.IsEqual<@P.AsString>.
                                And<ARTran.refNbr.IsEqual<@P.AsString>>>.
                                View.Select(this,soOrdShip.InvoiceType, soOrdShip.InvoiceNbr);
            foreach  (ARTran line in invDetails)
            {
                SOLine orderLine = SelectFrom<SOLine>.Where<SOLine.orderType.IsEqual<@P.AsString>.
                                And<SOLine.orderNbr.IsEqual<@P.AsString>.
                                And<SOLine.lineNbr.IsEqual<@P.AsInt>>>>.View.
                                SelectSingleBound(this, null, line.SOOrderType, line.SOOrderNbr, line.SOOrderLineNbr);
                BCSOLineExt lineExt = orderLine.GetExtension<BCSOLineExt>();

                if (lineExt != null)
                {
                    invPost.ItemsList.Add(lineExt.ExternalRef, line.Qty.ToString());
                }
            }


        }


        public override void SaveBucketExport(CCInvoiceEntityBucket bucket, IMappedEntity existing, string operation)
        {
            ////Setting the bucket to an object
            //MappedInvoice mappedObj = bucket.Invoice;

            ////external Invoice
            //InvoicePostData invPostData = mappedObj.Extern;

            throw new NotImplementedException();



        }


        #endregion Export


    }
}
