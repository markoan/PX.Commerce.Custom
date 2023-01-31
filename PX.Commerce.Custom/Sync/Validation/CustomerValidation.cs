using PX.Commerce.Core;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.Sync.Validation
{
	/// <summary>
	/// Example of a validator for customer accounts.
	/// </summary>
	public class CustomerValidator : BCBaseValidator, ISettingsValidator, IExternValidator
	{
		public int Priority { get { return 0; } }

		public virtual void Validate(IProcessor iproc)
		{
			Validate<CCCustomerProcessor>(iproc, (processor) =>
			{
				BCBindingExt storeExt = processor.GetBindingExt<BCBindingExt>();
				if (storeExt.CustomerNumberingID == null && BCDimensionMaskAttribute.GetAutoNumbering(CustomerRawAttribute.DimensionName) == null)
					throw new PXException(ConnectorMessages.NoCustomerNumbering);

				if (storeExt.CustomerClassID == null)
				{
					ARSetup arSetup = PXSelect<ARSetup>.Select(processor);
					if (arSetup.DfltCustomerClassID == null)
						throw new PXException(ConnectorMessages.NoCustomerClass);
				}

			});
			Validate<CCLocationProcessor>(iproc, (processor) =>
			{
				BCBindingExt storeExt = processor.GetBindingExt<BCBindingExt>();
				if (storeExt.CustomerNumberingID == null && BCDimensionMaskAttribute.GetAutoNumbering(CustomerRawAttribute.DimensionName) == null)
					throw new PXException(ConnectorMessages.NoCustomerNumbering);
				if (storeExt.LocationNumberingID == null && BCDimensionMaskAttribute.GetAutoNumbering(LocationActiveAttribute.DimensionName) == null)
					throw new PXException(ConnectorMessages.NoLocationNumbering);

			});
		}

        public void Validate(IProcessor processor, IExternEntity entity)
        {
            
        }

    }
}
