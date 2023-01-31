using PX.Commerce.Core;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.IN;
using PX.Commerce.Objects;
using PX.Commerce.Core.API;
using PX.Api.ContractBased.Models;

namespace PX.Commerce.Custom
{
    public abstract class CCOrderBaseProcessor<TGraph, TEntityBucket, TPrimaryMapped> : BCProcessorSingleBase<TGraph, TEntityBucket, TPrimaryMapped>, IProcessor
        where TGraph : PXGraph
        where TEntityBucket : class, IEntityBucket, new()
        where TPrimaryMapped : class, IMappedEntity, new()
    {
        public CCHelper helper = PXGraph.CreateInstance<CCHelper>();

        protected BCBinding currentBinding;
        protected InventoryItem refundItem;

        public override void Initialise(IConnector iconnector, ConnectorOperation operation)
        {
            base.Initialise(iconnector, operation);
            currentBinding = GetBinding();
            BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();

            helper.Initialize(this);
        }

        #region Refunds
        public virtual SalesOrderDetail InsertRefundAmountItem(decimal amount, StringValue branch)
        {
            decimal quantity = 1;
            BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();
            if (string.IsNullOrWhiteSpace(bindingExt.ReasonCode))
                throw new PXException("Reason code required");

            SalesOrderDetail detail = new SalesOrderDetail();
            detail.InventoryID = refundItem.InventoryCD?.TrimEnd().ValueField();
            detail.OrderQty = quantity.ValueField();
            detail.UOM = refundItem.BaseUnit.ValueField();
            detail.Branch = branch;
            detail.UnitPrice = amount.ValueField();
            detail.ManualPrice = true.ValueField();
            detail.ReasonCode = bindingExt?.ReasonCode?.ValueField();
            return detail;

        }
        #endregion

    }


}
