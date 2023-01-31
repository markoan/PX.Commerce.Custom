using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.IN;
using PX.Api.ContractBased.Models;

namespace PX.Commerce.Custom
{
    //EntityBucket to map 
    public class CCShipmentEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary => Shipment;

        public IMappedEntity[] Entities => new IMappedEntity[] { Primary };

        public MappedShipment Shipment;
        public List<MappedOrder> Orders = new List<MappedOrder>();
    }


    public class CCShipmentsRestrictor : BCBaseRestrictor, IRestrictor
    {
        public virtual FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
        {
            #region Shipments
            return base.Restrict<MappedShipment>(mapped, delegate (MappedShipment obj)
            {
                if (obj.Local != null)
                {
                    if (obj.Local.Confirmed?.Value == false)
                    {
                        return new FilterResult(FilterStatus.Invalid,
                                PXMessages.Localize(BCMessages.LogShipmentSkippedNotConfirmed));
                    }

                    if (obj.Local.OrderNoteIds != null)
                    {
                        BCBindingExt binding = processor.GetBindingExt<BCBindingExt>();

                        Boolean anyFound = false;
                        foreach (var orderNoeId in obj.Local?.OrderNoteIds)
                        {
                            if (processor.SelectStatus(BCEntitiesAttribute.Order, orderNoeId) == null) continue;

                            anyFound = true;
                        }
                        if (!anyFound)
                        {
                            return new FilterResult(FilterStatus.Ignore,
                                PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogShipmentSkippedNoOrder, obj.Local.ShipmentNumber?.Value ?? obj.Local.SyncID.ToString()));
                        }
                    }
                }

                return null;
            });
            #endregion
        }

        public virtual FilterResult RestrictImport(IProcessor processor, IMappedEntity mapped)
        {
            return null;
        }
    }


    [BCProcessor(typeof(CCConnector), BCEntitiesAttribute.Shipment, "Shipment",
          IsInternal = false,
          Direction = SyncDirection.Bidirect,
          PrimaryDirection = SyncDirection.Import,
          PrimarySystem = PrimarySystem.Extern,
          PrimaryGraph = typeof(SOShipmentEntry),
          ExternTypes = new Type[] { typeof(ShipmentData) },
          LocalTypes = new Type[] { typeof(PX.Commerce.Core.API.BCShipments) },
          GIScreenID = BCConstants.GenericInquiryShipmentDetails,
          DetailTypes = new String[] { BCEntitiesAttribute.ShipmentLine, BCCaptions.ShipmentLine, BCEntitiesAttribute.ShipmentBoxLine, BCCaptions.ShipmentLineBox },
          AcumaticaPrimaryType = typeof(PX.Objects.SO.SOShipment),
          AcumaticaPrimarySelect = typeof(PX.Objects.SO.SOShipment.shipmentNbr),
          Requires = new string[] { BCEntitiesAttribute.Order }
      )]
    [BCProcessorRealtime(PushSupported = false, HookSupported = false)]
    public class CCShipmentProcessor : BCProcessorSingleBase<CCShipmentProcessor, CCShipmentEntityBucket, MappedShipment>, IProcessor
    {
        protected ShipmentRestDataProvider shipmentDataProvider;
        protected AccountRestDataProvider accountRestDataProvider;
        protected OrderRestDataProvider orderRestDataProvider;
        protected List<BCShippingMappings> shippingMappings;
        //protected IEnumerable<InventoryLocationData> inventoryLocations;
        protected BCBinding currentBinding;
        protected BCBindingExt currentBindingExt;
        protected BCBindingCustom currentCustomSettings;


        #region Constructor
        public override void Initialise(IConnector iconnector, ConnectorOperation operation)
        {
            base.Initialise(iconnector, operation);
            currentBinding = GetBinding();
            currentBindingExt = GetBindingExt<BCBindingExt>();
            currentCustomSettings = GetBindingExt<BCBindingCustom>();

            var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());

            shipmentDataProvider = new ShipmentRestDataProvider(client);
            accountRestDataProvider = new AccountRestDataProvider(client);
            orderRestDataProvider = new OrderRestDataProvider(client);
            shippingMappings = PXSelectReadonly<BCShippingMappings,
                Where<BCShippingMappings.bindingID, Equal<Required<BCShippingMappings.bindingID>>>>
                .Select(this, Operation.Binding).Select(x => x.GetItem<BCShippingMappings>()).ToList();

        }
        #endregion

        public override void NavigateLocal(IConnector connector, ISyncStatus status)
        {
            SOOrderShipment orderShipment = PXSelect<SOOrderShipment, Where<SOOrderShipment.shippingRefNoteID, Equal<Required<SOOrderShipment.shippingRefNoteID>>>>.Select(this, status?.LocalID);
            if (orderShipment.ShipmentType == SOShipmentType.DropShip)//dropshipment
            {
                PX.Objects.PO.POReceiptEntry extGraph = PXGraph.CreateInstance<PX.Objects.PO.POReceiptEntry>();
                EntityHelper helper = new EntityHelper(extGraph);
                helper.NavigateToRow(extGraph.GetPrimaryCache().GetItemType().FullName, status.LocalID, PXRedirectHelper.WindowMode.NewWindow);

            }
            if (orderShipment.ShipmentType == SOShipmentType.Issue && orderShipment.ShipmentNoteID == null) //Invoice
            {
                PX.Objects.PO.POReceiptEntry extGraph = PXGraph.CreateInstance<PX.Objects.PO.POReceiptEntry>();
                EntityHelper helper = new EntityHelper(extGraph);
                helper.NavigateToRow(extGraph.GetPrimaryCache().GetItemType().FullName, status.LocalID, PXRedirectHelper.WindowMode.NewWindow);

            }
            else
            {
                SOShipmentEntry extGraph = PXGraph.CreateInstance<SOShipmentEntry>();
                EntityHelper helper = new EntityHelper(extGraph);
                helper.NavigateToRow(extGraph.GetPrimaryCache().GetItemType().FullName, status.LocalID, PXRedirectHelper.WindowMode.NewWindow);

            }

        }


        #region Pull
        public override MappedShipment PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
        {
            Core.API.BCShipments impl = cbapi.GetByID<Core.API.BCShipments>(localID);
            if (impl == null) return null;

            MappedShipment obj = new MappedShipment(impl, impl.SyncID, impl.SyncTime);

            return obj;
        }

        public override MappedShipment PullEntity(String externID, String externalInfo)
        {
            ShipmentData data = shipmentDataProvider.GetByID(externID);
            if (data == null) return null;

            MappedShipment obj = new MappedShipment(data, data.IncrementId?.ToString(), data.UpdatedAt.ToDate(false));
            return obj;
        }

        #endregion

        #region Export
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForExport(CCShipmentEntityBucket bucket, BCSyncStatus bcstatus)
        {
            throw new NotImplementedException();
        }


        public override void MapBucketExport(CCShipmentEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketExport(CCShipmentEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }
        #endregion Export

        #region Import
        public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForImport(CCShipmentEntityBucket bucket, BCSyncStatus status)
        {
            throw new NotImplementedException();
        }

        public override void MapBucketImport(CCShipmentEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketImport(CCShipmentEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }
        #endregion Import
        protected virtual void MapFilterFields(List<BCShipmentsResult> results, BCShipments impl)
        {
            impl.OrderNoteIds = new List<Guid?>();
            foreach (var result in results)
            {
                impl.ShippingNoteID = result.NoteID;
                impl.VendorRef = result.InvoiceNbr;
                impl.ShipmentNumber = result.ShipmentNumber;
                impl.ShipmentType = result.ShipmentType;
                impl.LastModified = result.LastModifiedDateTime;
                impl.Confirmed = result.Confirmed;
                impl.ExternalShipmentUpdated = result.ExternalShipmentUpdated;
                impl.OrderNoteIds.Add(result.OrderNoteID.Value);
            }
        }

        protected virtual void GetDropShipmentByShipmentNbr(BCShipments bCShipments)
        {
            bCShipments.POReceipt = new PurchaseReceipt();
            bCShipments.POReceipt.ShipmentNbr = bCShipments.ShipmentNumber;
            bCShipments.POReceipt.VendorRef = bCShipments.VendorRef;
            bCShipments.POReceipt.Details = new List<PurchaseReceiptDetail>();

            foreach (PXResult<SOLineSplit, PX.Objects.PO.POOrder, SOOrder> item in PXSelectJoin<SOLineSplit,
                InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderNbr, Equal<SOLineSplit.pONbr>>,
                InnerJoin<SOOrder, On<SOLineSplit.orderNbr, Equal<SOOrder.orderNbr>>>>,
                Where<SOLineSplit.pOReceiptNbr, Equal<Required<SOLineSplit.pOReceiptNbr>>>>
            .Select(this, bCShipments.ShipmentNumber.Value))
            {
                SOLineSplit lineSplit = item.GetItem<SOLineSplit>();
                SOOrder line = item.GetItem<SOOrder>();
                PX.Objects.PO.POOrder poOrder = item.GetItem<PX.Objects.PO.POOrder>();
                PurchaseReceiptDetail detail = new PurchaseReceiptDetail();
                detail.SOOrderNbr = lineSplit.OrderNbr.ValueField();
                detail.SOLineNbr = lineSplit.LineNbr.ValueField();
                detail.SOOrderType = lineSplit.OrderType.ValueField();
                detail.ReceiptQty = lineSplit.ShippedQty.ValueField();
                detail.ShipVia = poOrder.ShipVia.ValueField();
                detail.SONoteID = line.NoteID.ValueField();
                bCShipments.POReceipt.Details.Add(detail);
            }
        }


        protected virtual void GetOrderShipment(BCShipments bCShipments)
        {
            if (bCShipments.ShipmentType?.Value == SOShipmentType.DropShip)
                GetDropShipmentByShipmentNbr(bCShipments);
            else if (bCShipments.ShipmentType.Value == SOShipmentType.Invoice)
                GetInvoiceByShipmentNbr(bCShipments);
            else
                bCShipments.Shipment = cbapi.GetByID<Shipment>(bCShipments.ShippingNoteID.Value);

        }

        protected virtual void GetInvoiceByShipmentNbr(BCShipments bCShipment)
        {
            bCShipment.Shipment = new Shipment();
            bCShipment.Shipment.Details = new List<ShipmentDetail>();

            foreach (PXResult<ARTran, SOOrder> item in PXSelectJoin<ARTran, InnerJoin<SOOrder, On<ARTran.sOOrderNbr, Equal<SOOrder.orderNbr>>>,
            Where<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.sOOrderType, Equal<Required<ARTran.sOOrderType>>>>>
            .Select(this, bCShipment.ShipmentNumber.Value, bCShipment.OrderType.Value))
            {
                ARTran line = item.GetItem<ARTran>();
                ShipmentDetail detail = new ShipmentDetail();
                detail.OrderNbr = line.SOOrderNbr.ValueField();
                detail.OrderLineNbr = line.SOOrderLineNbr.ValueField();
                detail.OrderType = line.SOOrderType.ValueField();
                bCShipment.Shipment.Details.Add(detail);
            }
        }
        protected virtual EntityStatus GetShipment(CCShipmentEntityBucket bucket, BCShipments bCShipment)
        {
            if (bCShipment.ShippingNoteID == null || bCShipment.ShippingNoteID.Value == Guid.Empty) return EntityStatus.None;
            bCShipment.Shipment = cbapi.GetByID<Shipment>(bCShipment.ShippingNoteID.Value);
            if (bCShipment.Shipment == null || bCShipment.Shipment?.Details?.Count == 0)
                return EntityStatus.None;

            MappedShipment obj = bucket.Shipment = bucket.Shipment.Set(bCShipment, bCShipment.ShippingNoteID.Value, bCShipment.LastModified.Value);
            EntityStatus status = EnsureStatus(obj, SyncDirection.Export);

            IEnumerable<ShipmentDetail> lines = bCShipment.Shipment.Details
                .GroupBy(r => new { OrderType = r.OrderType.Value, OrderNbr = r.OrderNbr.Value })
                .Select(r => r.First());
            foreach (ShipmentDetail line in lines)
            {
                SalesOrder orderImpl = cbapi.Get<SalesOrder>(new SalesOrder() { OrderType = line.OrderType.Value.SearchField(), OrderNbr = line.OrderNbr.Value.SearchField() });
                if (orderImpl == null) throw new PXException(BCMessages.OrderNotFound, bCShipment.Shipment.ShipmentNbr.Value);
                MappedOrder orderObj = new MappedOrder(orderImpl, orderImpl.SyncID, orderImpl.SyncTime);
                EntityStatus orderStatus = EnsureStatus(orderObj);

                if (orderObj.ExternID == null) throw new PXException(BCMessages.OrderNotSyncronized, orderImpl.OrderNbr.Value);

                bucket.Orders.Add(orderObj);
            }
            return status;
        }

    }
}
