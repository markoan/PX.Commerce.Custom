using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
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
    public class CCPaymentEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary { get => Payment;  }
        public IMappedEntity[] Entities => new IMappedEntity[] { Primary };

        public MappedPayment Payment;
        public MappedOrder Order;

    }

	public class CCPaymentsRestrictor : BCBaseRestrictor, IRestrictor
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

	[BCProcessor(typeof(CCConnector), BCEntitiesAttribute.Payment, "Payment",
          IsInternal = false,
          Direction = SyncDirection.Import,
          PrimaryDirection = SyncDirection.Import,
          PrimarySystem = PrimarySystem.Extern,
          PrimaryGraph = typeof(PX.Objects.AR.ARPaymentEntry),
          ExternTypes = new Type[] { typeof(InvoiceData) },
          LocalTypes = new Type[] { typeof(PX.Commerce.Core.API.Payment) },
          AcumaticaPrimaryType = typeof(PX.Objects.AR.ARPayment),
          AcumaticaPrimarySelect = typeof(Search<PX.Objects.AR.ARPayment.refNbr, Where<PX.Objects.AR.ARPayment.docType, Equal<ARDocType.payment>>>),
          Requires = new string[] {BCEntitiesAttribute.Order}

      )]
    [BCProcessorRealtime(PushSupported = false, HookSupported = false)]
    public class CCPaymentProcessor : BCProcessorSingleBase<CCPaymentProcessor, CCPaymentEntityBucket, MappedPayment>, IProcessor
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

        public override PXTransactionScope WithTransaction(Action action)
        {
            action();
            return null;
        }
        #endregion

        public override IEnumerable<MappedPayment> PullSimilar(IExternEntity entity, out string uniqueField)
        {
            InvoiceData externEntity = (InvoiceData)entity;

            uniqueField = externEntity.InvoiceId.ToString();

            if (string.IsNullOrEmpty(uniqueField))
                return null;

            List<MappedPayment> result = new List<MappedPayment>();
            foreach (PX.Objects.AR.ARPayment item in helper.PaymentByExternalRef.Select(uniqueField))
            {
                Payment data = new Payment() { SyncID = item.NoteID, SyncTime = item.LastModifiedDateTime };
                result.Add(new MappedPayment(data, data.SyncID, data.SyncTime));
            }
            return result;
        }

        public override void ControlDirection(CCPaymentEntityBucket bucket, BCSyncStatus status, ref bool shouldImport, ref bool shouldExport, ref bool skipSync, ref bool skipForce)
        {
            MappedPayment payment = bucket.Payment;
            if (!payment.IsNew)
                if (payment.Local?.Status?.Value == PX.Objects.AR.Messages.Voided)
                {
                    shouldImport = false;
                    skipForce = true;// if payment is already voided cannot make any changes to it so skip force.
                    skipSync = true;
                    UpdateStatus(payment, status.LastOperation);// to update extern hash in case of payment if its voided or captured in acumatica
                }
                else if (payment.Local?.Status?.Value != PX.Objects.AR.Messages.CCHold)
                {
                    shouldImport = false;
                    skipSync = true;
                    skipForce = true;// if payment is not cchold then it is already capture so skip force sync
                    UpdateStatus(payment, status.LastOperation);// to update extern hash in case Shopify payment if its voided or captured in acumatica
                }
        }


        #region Pull
        public override MappedPayment PullEntity(Guid? localID, Dictionary<string, object> fields)
        {
            Payment impl = cbapi.GetByID<Payment>(localID);
            if (impl == null) return null;

            MappedPayment obj = new MappedPayment(impl, impl.SyncID, impl.SyncTime);

            return obj;
        }
        public override MappedPayment PullEntity(String externID, String jsonObject)
        {
            var data = paymentDataProvider.GetByID(externID.KeySplit(1));
            if (data == null) return null;

            MappedPayment obj = new MappedPayment(data, new Object[] { data.OrderId, data.InvoiceId }.KeyCombine(), data.UpdatedAt.ToDate(false));

            return obj;
        }
        #endregion


        #region Import
        public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForImport(CCPaymentEntityBucket bucket, BCSyncStatus bcstatus)
        {
            throw new NotImplementedException();
        }

        public override void MapBucketImport(CCPaymentEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketImport(CCPaymentEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }
        #endregion Import

        #region Export
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForExport(CCPaymentEntityBucket bucket, BCSyncStatus bcstatus)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketExport(CCPaymentEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }
        #endregion Export


    }
}
