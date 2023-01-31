using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
    public class CCImageEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary => Image;
        public IMappedEntity[] Entities => new IMappedEntity[] { Primary };
        public MappedProductImage Image;
    }

    [BCProcessor(typeof(CCConnector), BCEntitiesAttribute.ProductImage, BCCaptions.ProductImage,
        IsInternal = false, 
        Direction = SyncDirection.Export,
        PrimaryDirection =SyncDirection.Export,
        PrimarySystem = PrimarySystem.Local,
        PrimaryGraph = typeof(PX.SM.WikiFileMaintenance),
        ExternTypes = new Type[] {},
        LocalTypes = new Type[] {},
        AcumaticaPrimaryType = typeof(PX.SM.UploadFileWithIDSelector),

        URL = "products/{0}/images/{1}",
        Requires = new string[] { },
        RequiresOneOf = new string[] { BCEntitiesAttribute.StockItem + "." + BCEntitiesAttribute.NonStockItem + "." + BCEntitiesAttribute.ProductWithVariant }
       )]
    public class CCImageProcessor : BCProcessorBulkBase<CCImageProcessor, CCImageEntityBucket, MappedStockItem>, IProcessor
    {
        protected IParentRestDataProvider<ProductData> productDataProvider;
        protected IChildRestDataProvider<ProductImageData> productImageDataProvider;
        protected Dictionary<string, List<ProductImageData>> existingImages;
        protected UploadFileMaintenance uploadGraph;
        protected BCBinding currentBinding;

        #region Constructor
        public override void Initialise(IConnector iconnector, ConnectorOperation operation)
        {
            base.Initialise(iconnector, operation);
            currentBinding = GetBinding();

            var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());

            productDataProvider = new ProductRestDataProvider(client);
            productImageDataProvider = new ProductImageRestDataProvider(client);
            uploadGraph = PXGraph.CreateInstance<UploadFileMaintenance>();
            existingImages = new Dictionary<string, List<ProductImageData>>();
        }
        #endregion

        #region Import
        public override void FetchBucketsImport()
        {
            throw new NotImplementedException();
        }
        public override void SaveBucketsImport(List<CCImageEntityBucket> buckets)
        {
            throw new NotImplementedException();
        }

        public override List<CCImageEntityBucket> GetBucketsImport(List<BCSyncStatus> ids)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Export

        public override void FetchBucketsExport()
        {
            GetBucketsExport(null);
        }

        public override void SaveBucketsExport(List<CCImageEntityBucket> buckets)
        {
            throw new NotImplementedException();

        }

        public override List<CCImageEntityBucket> GetBucketsExport(List<BCSyncStatus> ids)
        {
            throw new NotImplementedException();

        }



        #endregion Export
    }
}
