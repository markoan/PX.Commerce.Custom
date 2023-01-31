using PX.Commerce.Core;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.Sync.Validation
{
	public class GeneralValidator : BCBaseValidator, ISettingsValidator, IExternValidator
	{
		public int Priority { get { return int.MaxValue; } }

		public virtual void Validate(IProcessor processor)
		{
			if (PXAccess.FeatureInstalled<FeaturesSet.subItem>() == true)
				throw new PXException(ConnectorMessages.FeatureNotSupported, "Inventory Subitems");
			if (PXAccess.FeatureInstalled<FeaturesSet.financialStandard>() == false)
				throw new PXException(ConnectorMessages.FeatureRequired, "Standard Financials");
			if (PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() == false)
				throw new PXException(ConnectorMessages.FeatureRequired, "Business Accounts Location");
			if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() == false)
				throw new PXException(ConnectorMessages.FeatureRequired, " Distribution");
		}

		public virtual void Validate(IProcessor processor, IExternEntity entity)
		{
			RunAttributesValidation(processor, entity);
		}
	}
}
