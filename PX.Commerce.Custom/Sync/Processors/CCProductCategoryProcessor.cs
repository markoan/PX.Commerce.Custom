using PX.Api.ContractBased.Models;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;




namespace PX.Commerce.Custom
{
    public class CCProductCategoryEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary => Category;
        public IMappedEntity[] Entities => new IMappedEntity[] { Primary };
        public override IMappedEntity[] PreProcessors { get => new IMappedEntity[] { LocalParent, ExternParent }; }

        public MappedCategory Category;
        public MappedCategory LocalParent;
        public MappedCategory ExternParent;
    }

    [BCProcessor(typeof(CCConnector), CCEntitiesAttributes.SalesCategory, BCCaptions.SalesCategory,
        IsInternal = false,
        Direction = SyncDirection.Bidirect,
        PrimaryDirection = SyncDirection.Export,
        PrimarySystem = PrimarySystem.Local,
        PrimaryGraph = typeof(PX.Objects.IN.INCategoryMaint),
        ExternTypes = new Type[] { typeof(ProductCategoryData) },
        LocalTypes = new Type[] { typeof(BCItemSalesCategory) },
        AcumaticaPrimaryType = typeof(PX.Objects.IN.INCategory),
        AcumaticaPrimarySelect = typeof(PX.Objects.IN.INCategory.categoryID),
        URL = "categories/{0}",
        Requires = new string[] { })]
    [BCProcessorRealtime(PushSupported = false, HookSupported = false)]
    public class CCProductCategoryProcessor : BCProcessorSingleBase<CCProductCategoryProcessor, CCProductCategoryEntityBucket, MappedCategory>, IProcessor
    {

        public CCHelper helper = PXGraph.CreateInstance<CCHelper>();

        protected ProductRestDataProvider productDataProvider;
        protected ProductCategoryRestDataProvider categoryDataProvider;
        protected BCBinding currentBinding;
        protected BCBindingExt currentBindingExt;
        protected BCBindingCustom currentExternalBinding;

        #region Constructor
        public override void Initialise(IConnector iconnector, ConnectorOperation operation)
        {
            base.Initialise(iconnector, operation);
            currentBinding = GetBinding();
            currentBindingExt = GetBindingExt<BCBindingExt>();
            currentExternalBinding = GetBindingExt<BCBindingCustom>();

            cbapi.UseNoteID = true;

            var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());
            productDataProvider = new ProductRestDataProvider(client);
            categoryDataProvider = new ProductCategoryRestDataProvider(client);

            helper.Initialize(this);
        }
        #endregion

        #region Common
        public override MappedCategory PullEntity(Guid? localID, Dictionary<string, object> fields)
        {
            BCItemSalesCategory impl = cbapi.GetByID<BCItemSalesCategory>(localID);
            if (impl == null) return null;

            MappedCategory obj = new MappedCategory(impl, impl.SyncID, impl.SyncTime);

            return obj;
        }
        public override MappedCategory PullEntity(String externID, String jsonObject)
        {
            ProductCategoryData data = categoryDataProvider.GetByID(externID);
            if (data == null) return null;

            MappedCategory obj = new MappedCategory(data, externID, data.CalculateHash());

            return obj;
        }

        public override IEnumerable<MappedCategory> PullSimilar(IExternEntity entity, out String uniqueField)
        {
            uniqueField = ((ProductCategoryData)entity)?.Name;
            var parent = ((ProductCategoryData)entity)?.ParentId;
            if (uniqueField == null) return null;

            PX.Objects.IN.INCategory incategory = PXSelectJoin<PX.Objects.IN.INCategory,
                    LeftJoin<BCSyncStatus, On<PX.Objects.IN.INCategory.noteID, Equal<BCSyncStatus.localID>>>,
                    Where<BCSyncStatus.connectorType, Equal<Current<BCEntity.connectorType>>,
                        And<BCSyncStatus.bindingID, Equal<Current<BCEntity.bindingID>>,
                        And<BCSyncStatus.entityType, Equal<Current<BCEntity.entityType>>,
                        And<BCSyncStatus.externID, Equal<Required<BCSyncStatus.externID>>>>>>>.Select(this, parent);
            int parentId = incategory?.CategoryID ?? 0;
            BCItemSalesCategory[] impls = cbapi.GetAll<BCItemSalesCategory>(new BCItemSalesCategory() { Description = uniqueField.SearchField(), ParentCategoryID = parentId.SearchField() },
                filters: GetFilter(Operation.EntityType).LocalFiltersRows.Cast<PXFilterRow>().ToArray(), supportPagination: false).ToArray();
            if (impls == null) return null;
            if (impls.Length == 1)
            {
                var impl = impls.First();
                var existedStatus = BCSyncStatus.LocalIDIndex.Find(this, Operation.ConnectorType, Operation.Binding, Operation.EntityType, impl.SyncID);
                if (existedStatus != null && existedStatus.ExternID != null && existedStatus.ExternID != ((ProductCategoryData)entity)?.Id?.ToString())
                {
                    //Check the existed ExternID in BC whether has been deleted.
                    ProductCategoryData externResult = categoryDataProvider.GetByID(existedStatus.ExternID);
                    if (externResult == null)
                    {
                        existedStatus.ExternID = null;
                        Statuses.Update(existedStatus);
                        Statuses.Cache.Persist(existedStatus, PXDBOperation.Update);
                    }
                }
            }

            return impls.Select(impl => new MappedCategory(impl, impl.SyncID, impl.SyncTime));
        }

        public override void NavigateLocal(IConnector connector, ISyncStatus status)
        {
            PX.Objects.IN.INCategoryMaint extGraph = PXGraph.CreateInstance<PX.Objects.IN.INCategoryMaint>();
            PX.Commerce.Objects.BCINCategoryMaintExt extGraphExt = extGraph.GetExtension<PX.Commerce.Objects.BCINCategoryMaintExt>();
            extGraphExt.SelectedCategory.Current = PXSelect<PX.Commerce.Objects.BCINCategoryMaintExt.SelectedINCategory,
              Where<PX.Commerce.Objects.BCINCategoryMaintExt.SelectedINCategory.noteID, Equal<Required<PX.Commerce.Objects.BCINCategoryMaintExt.SelectedINCategory.noteID>>>>.Select(extGraph, status.LocalID);

            throw new PXRedirectRequiredException(extGraph, true, "Navigation");
        }
        #endregion

        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForExport(CCProductCategoryEntityBucket bucket, BCSyncStatus bcstatus)
        {
            throw new NotImplementedException();
        }

        public override EntityStatus GetBucketForImport(CCProductCategoryEntityBucket bucket, BCSyncStatus bcstatus)
        {
            throw new NotImplementedException();
        }

        public override void MapBucketExport(CCProductCategoryEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void MapBucketImport(CCProductCategoryEntityBucket bucket, IMappedEntity existing)
        {
            throw new NotImplementedException();
        }

        public override void SaveBucketExport(CCProductCategoryEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }


        public override void SaveBucketImport(CCProductCategoryEntityBucket bucket, IMappedEntity existing, string operation)
        {
            throw new NotImplementedException();
        }
    }
}
