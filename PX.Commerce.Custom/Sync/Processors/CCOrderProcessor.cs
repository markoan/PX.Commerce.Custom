using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Objects.SO;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Common;
using PX.Api.ContractBased.Models;
using PX.Objects.IN;
using static PX.SM.PXSyncPriority;
using System.Reflection;
using PX.Objects.AR;

namespace PX.Commerce.Custom
{
    //EntityBucket to map 
    public class CCOrderEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary { get => Order; }

        public IMappedEntity[] Entities => new IMappedEntity[] { Order };

        public override IMappedEntity[] PreProcessors { get => new IMappedEntity[] { Contact }; }
        public override IMappedEntity[] PostProcessors { get => Enumerable.Empty<IMappedEntity>().Concat(Payments).Concat(Shipments).ToArray(); }


        public MappedOrder Order;
		public MappedCustomer Customer;
		public MappedContact Contact;
        //public MappedLocation Location;
		public MappedContactAddress Location;
		public List<MappedPayment> Payments = new List<MappedPayment>();
        public List<MappedShipment> Shipments = new List<MappedShipment>();
    }

    public class CCOrderRestrictor : BCBaseRestrictor, IRestrictor
    {
        #region Restrict Export
        public FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
        {
			//throw new NotImplementedException();
			return new FilterResult();
        }
        #endregion Restrict Export

        #region Restrict Import
        public virtual FilterResult RestrictImport(IProcessor processor, IMappedEntity mapped)
        {
            return base.Restrict<MappedOrder>(mapped, delegate (MappedOrder obj)
            {
				BCBindingCustom customBinding = processor.GetBindingExt<BCBindingCustom>();
                BCBindingExt bindingExt = processor.GetBindingExt<BCBindingExt>();

				//Skip orders that were created before SyncOrdersFrom
				if(obj.Extern != null && bindingExt.SyncOrdersFrom != null && DateTime.Parse(obj.Extern.CreatedAt) < bindingExt.SyncOrdersFrom)
                {
					return new FilterResult(FilterStatus.Ignore, PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogOrderSkippedCreatedBeforeSyncOrdersFrom,
						obj.Extern.IncrementId, bindingExt.SyncOrdersFrom.Value.Date.ToString("d")));
                }

				return null;
            });
        }
		#endregion Restrict Import
    }


	[BCProcessor(typeof(CCConnector), BCEntitiesAttribute.Order, BCCaptions.Order,
		  IsInternal = false,
		  Direction = SyncDirection.Bidirect,
		  PrimaryDirection = SyncDirection.Import,
		  PrimarySystem = PrimarySystem.Extern,
		  PrimaryGraph = typeof(SOOrderEntry),
		  ExternTypes = new Type[] { typeof(OrderData) },
		  LocalTypes = new Type[] { typeof(SalesOrder) },
		  DetailTypes = new String[] { BCEntitiesAttribute.OrderLine, BCCaptions.OrderLine, BCEntitiesAttribute.OrderAddress, BCCaptions.OrderAddress },
		  AcumaticaPrimaryType = typeof(PX.Objects.SO.SOOrder),
		  AcumaticaPrimarySelect = typeof(Search<PX.Objects.SO.SOOrder.orderNbr>),
		  Requires = new string[] { BCEntitiesAttribute.Customer }
	  )]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.OrderLine, EntityName = BCCaptions.OrderLine, AcumaticaType = typeof(PX.Objects.SO.SOLine))]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.OrderAddress, EntityName = BCCaptions.OrderAddress, AcumaticaType = typeof(PX.Objects.SO.SOOrder))]
	[BCProcessorRealtime(PushSupported = false, HookSupported = false)]
	[BCProcessorExternCustomField(BCConstants.OrderItemProperties, CustomCaptions.Properties, nameof(OrderItem.ProductCustomAttributes), typeof(OrderItem), new Type[] { typeof(SalesOrderDetail) }, readAsCollection: true)]
	public class CCOrderProcessor : CCOrderBaseProcessor<CCOrderProcessor, CCOrderEntityBucket, MappedOrder>
	{
		protected CCPaymentProcessor paymentProcessor = PXGraph.CreateInstance<CCPaymentProcessor>();
		protected OrderRestDataProvider orderDataProvider;
		protected StoreRestDataProvider storeDataProvider;
		protected CustomerRestDataProvider customerDataProvider;
		protected AccountRestDataProvider accountRestDataProvider;
		protected PaymentRestDataProvider paymentRestDataProvider;
		protected BCBindingCustom currentSettings;
		protected List<string> skipOrderItems;
		public PXSelect<BCShippingMappings, Where<BCShippingMappings.bindingID, Equal<Required<BCShippingMappings.bindingID>>,
			And<BCShippingMappings.shippingZone, Equal<Required<BCShippingMappings.shippingZone>>, And<BCShippingMappings.shippingMethod, Equal<Required<BCShippingMappings.shippingMethod>>>>>> bcShippingMappings;
		public PXSelect<State, Where<State.name, Equal<Required<State.name>>, Or<State.stateID, Equal<Required<State.stateID>>>>> states;

		public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryCD, Equal<Required<PX.Objects.IN.InventoryItem.inventoryCD>>>> SearchProduct;

		#region Initialization
		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);

			currentSettings = GetBindingExt<BCBindingCustom>();

			var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());

			orderDataProvider = new OrderRestDataProvider(client);
			storeDataProvider = new StoreRestDataProvider(client);

			customerDataProvider = new CustomerRestDataProvider(client);
			accountRestDataProvider = new AccountRestDataProvider(client);
			paymentRestDataProvider = new PaymentRestDataProvider(client);
			skipOrderItems = new List<string>();

			if (GetEntity(BCEntitiesAttribute.Payment)?.IsActive == true)
			{
				paymentProcessor.Initialise(iconnector, operation.Clone().With(_ => { _.EntityType = BCEntitiesAttribute.Payment; return _; }));
			}
		}
		#endregion

		#region Common

		public override MappedOrder PullEntity(Guid? localID, Dictionary<String, Object> fields)
		{
			SalesOrder impl = cbapi.GetByID<SalesOrder>(localID);
			if (impl == null) return null;

			MappedOrder obj = new MappedOrder(impl, impl.SyncID, impl.SyncTime);

			return obj;
		}
		public override MappedOrder PullEntity(string externID, string jsonObject)
		{
			OrderData data = orderDataProvider.GetByID(externID);
			if (data == null) return null;

			MappedOrder obj = new MappedOrder(data, data.IncrementId?.ToString(), data.CreatedAt.ToDate(false));

			return obj;
		}

		public override IEnumerable<MappedOrder> PullSimilar(IExternEntity entity, out string uniqueField)
		{
			uniqueField = ((OrderData)entity)?.IncrementId?.ToString();
			if (string.IsNullOrEmpty(uniqueField))
				return null;
			uniqueField = APIHelper.ReferenceMake(uniqueField, currentBinding.BindingName);

			List<MappedOrder> result = new List<MappedOrder>();
			List<string> orderTypes = new List<string>() { GetBindingExt<BCBindingExt>()?.OrderType };
		
			helper.TryGetCustomOrderTypeMappings(ref orderTypes);

			foreach (SOOrder item in helper.OrderByTypesAndCustomerRefNbr.Select(orderTypes.ToArray(), uniqueField))
			{
				SalesOrder data = new SalesOrder() { SyncID = item.NoteID, SyncTime = item.LastModifiedDateTime, ExternalRef = item.CustomerRefNbr?.ValueField() };
				result.Add(new MappedOrder(data, data.SyncID, data.SyncTime));
			}
			return result;
		}

		public override bool ShouldFilter(SyncDirection direction, BCSyncStatus status)
		{
			return status != null && (status.ExternID == null || status.LocalID == null);
		}

		public override void ControlDirection(CCOrderEntityBucket bucket, BCSyncStatus status, ref bool shouldImport, ref bool shouldExport, ref bool skipSync, ref bool skipForce)
		{
			MappedOrder order = bucket.Order;

			if ((shouldImport || Operation.SyncMethod == SyncMode.Force) && !order.IsNew && order?.ExternID != null && order?.LocalID != null && (order.Local?.Status?.Value == PX.Objects.SO.Messages.Completed || order.Local?.Status?.Value == PX.Objects.SO.Messages.Cancelled))
			{
				if ((status.Status == BCSyncStatusAttribute.Synchronized || status.Status == BCSyncStatusAttribute.Pending || status.Status == BCSyncStatusAttribute.Failed) &&
						order?.Extern?.Status == "complete")
				{
					skipForce = true;
					skipSync = true;
					status.LastOperation = BCSyncOperationAttribute.ManuallySynced;
					status.LastErrorMessage = null;
					UpdateStatus(order, status.LastOperation, status.LastErrorMessage);
					shouldImport = false;// if order is canceled or completed in ERP and Fullfilled in External System then skip import and mark order as synchronized
				}
				else 
				{
					var orderTotal = (decimal)int.Parse(order.Extern.Total);
					var itemTotals = order.Extern?.Items?.Sum(x => long.Parse(x.Quantity)) ?? 0;
					var refundStatus = SelectStatusChildren(order.SyncID).FirstOrDefault(x => x.EntityType == BCEntitiesAttribute.OrderRefunds);
					if (refundStatus != null)// to check if there were any refunds processed before order was completed
					{
						var details = SelectStatusDetails(refundStatus.SyncID);
						var refundsBeforeOrderCompleted = details?.Where(x => x.EntityType == BCEntitiesAttribute.Payment)?.Select(x => x.ExternID.KeySplit(0))?.ToList();

					}

					//We should prevent order from sync if it is updated by refunds
					if (orderTotal == order.Local.OrderTotal?.Value && itemTotals == (order.Local?.Details?.Where(x => x.InventoryID?.Value != refundItem?.InventoryCD?.Trim())?.Sum(x => x.OrderQty?.Value ?? 0) ?? 0))
					{
						DateTime? orderdate = order.Extern.CreatedAt.ToDate(false);
					}
				}
			}
		}
		#endregion

		#region Import 
		public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
			BCBindingExt currentBindingExt = GetBindingExt<BCBindingExt>();
			var minDate = minDateTime == null || (minDateTime != null && currentBindingExt.SyncOrdersFrom != null && minDateTime < currentBindingExt.SyncOrdersFrom) ? currentBindingExt.SyncOrdersFrom : minDateTime;
			FilterOrders filter = new FilterOrders { Status = OrderStatus.Any, Fields = "id,name,source_name,financial_status,updated_at,created_at,cancelled_at,closed_at", Order = "updated_at asc" };
			if (minDateTime != null) filter.ProcessedAtMin = minDateTime.Value.ToLocalTime().ToString();
			if (maxDateTime != null) filter.ProcessedAtMax = maxDateTime.Value.ToLocalTime().ToString();

			IEnumerable<OrderData> datas = orderDataProvider.GetAll(filter);

			int countNum = 0;
			List<IMappedEntity> mappedList = new List<IMappedEntity>();
			try
			{
				foreach (OrderData data in datas)
				{
					IMappedEntity obj = new MappedOrder(data, data.IncrementId?.ToString(), data.CreatedAt.ToDate(false));

					mappedList.Add(obj);
					countNum++;
					if (countNum % BatchFetchCount == 0)
					{
						ProcessMappedListForImport(ref mappedList, true);

					}
				}
			}
			finally
			{
				if (mappedList.Any())
				{
					ProcessMappedListForImport(ref mappedList, true);
				}
			}
		}

		public override EntityStatus GetBucketForImport(CCOrderEntityBucket bucket, BCSyncStatus syncstatus)
		{
            throw new NotImplementedException();

        }


        public override void MapBucketImport(CCOrderEntityBucket bucket, IMappedEntity existing)
		{
            throw new NotImplementedException();

	
		}

		public override void SaveBucketImport(CCOrderEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();

        }
        #endregion Import

        #region Export
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();

        }


        public override EntityStatus GetBucketForExport(CCOrderEntityBucket bucket, BCSyncStatus syncstatus)
        {
            throw new NotImplementedException();

        }

        public override void SaveBucketExport(CCOrderEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();

        }
        #endregion Export

    }
}
