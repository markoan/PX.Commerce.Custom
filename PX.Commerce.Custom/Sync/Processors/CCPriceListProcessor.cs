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
using System.Reflection;
using PX.Common;
using PX.Api.ContractBased.Models;
using PX.Objects.IN;
using PX.Objects.GL;
using PX.Objects.AR;
using static PX.SM.PXSyncPriority;

namespace PX.Commerce.Custom
{
	//EntityBucket to map 
	public class CCPriceListEntityBucket : EntityBucketBase, IEntityBucket
	{
		public IMappedEntity Primary => Price;
		public IMappedEntity[] Entities => new IMappedEntity[] { Primary };

		public MappedPriceList Price;
	}

	[BCProcessor(typeof(CCConnector), BCEntitiesAttribute.PriceList, BCCaptions.PriceList,
		IsInternal = false,
		Direction = SyncDirection.Export,
		PrimaryDirection = SyncDirection.Export,
		PrimarySystem = PrimarySystem.Local,
		ExternTypes = new Type[] { typeof(PriceList) },
		LocalTypes = new Type[] { typeof(PriceListSalesPrice) },
		AcumaticaPrimaryType = typeof(ARPriceClass),
		URL = "products/{0}",
		Requires = new string[] { BCEntitiesAttribute.Customer },
		RequiresOneOf = new string[] { BCEntitiesAttribute.StockItem + "." + BCEntitiesAttribute.NonStockItem + "." + BCEntitiesAttribute.ProductWithVariant }
	)]
	public class CCPriceListProcessor : BCProcessorBulkBase<CCPriceListProcessor, CCPriceListEntityBucket, MappedPriceList>, IProcessor
	{
		public CCHelper helper = PXGraph.CreateInstance<CCHelper>();

		protected PriceListRestDataProvider priceListRestDataProvider;

		protected Dictionary<string, string> customerGroups;
		protected List<ARPriceClass> customerPriceClasses;


		#region Constructor
		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);
			var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());
			priceListRestDataProvider = new PriceListRestDataProvider(client);

			customerPriceClasses = PXSelect<ARPriceClass>.Select(this).Select(c => (ARPriceClass)c).ToList();

			helper.Initialize(this);
		}
		#endregion

		#region Common

		#endregion

		#region Import
		public override void FetchBucketsImport()
		{
			throw new NotImplementedException();
		}
		public override List<CCPriceListEntityBucket> GetBucketsImport(List<BCSyncStatus> ids)
		{
			throw new NotImplementedException();
		}
		public override void SaveBucketsImport(List<CCPriceListEntityBucket> buckets)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Export

		public override void FetchBucketsExport()
		{
			GetBucketsExport(null);
		}

		public override List<CCPriceListEntityBucket> GetBucketsExport(List<BCSyncStatus> ids)
        {
            throw new NotImplementedException();
        }

        public override void MapBucketExport(CCPriceListEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketsExport(List<CCPriceListEntityBucket> buckets)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
