using PX.Api.ContractBased.Models;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Objects.Substitutes;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.RelatedItems;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using PX.SM;

namespace PX.Commerce.Custom
{
    //EntityBucket to map Products from type stock item
    public class CCStockItemEntityBucket : EntityBucketBase, IEntityBucket
    {
        public IMappedEntity Primary => Product;

        public IMappedEntity[] Entities => new IMappedEntity[] { Primary };

        public override IMappedEntity[] PreProcessors { get => Enumerable.Empty<IMappedEntity>().Concat(Categories).ToArray(); }

        public MappedStockItem Product;
        public List<MappedCategory> Categories = new List<MappedCategory>();
    }

    public class CCStockItemRestrictor : BCBaseRestrictor, IRestrictor
    {
        public virtual FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
        {
            #region StockItems
            return base.Restrict<MappedStockItem>(mapped, delegate (MappedStockItem obj)
            {


                if (obj.Local != null && obj.Local.ExportToExternal?.Value == false)
                {
                    return new FilterResult(FilterStatus.Invalid,
                        PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogItemNoExport, obj.Local.InventoryID?.Value ?? obj.Local.SyncID.ToString()));
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


    [BCProcessor(typeof(CCConnector), CCEntitiesAttributes.StockItem, 
        BCCaptions.StockItem,
          IsInternal = false,
          Direction = SyncDirection.Bidirect,
          PrimaryDirection = SyncDirection.Export,
          PrimarySystem = PrimarySystem.Local,
          PrimaryGraph = typeof(InventoryItemMaint),
          ExternTypes = new Type[] { typeof(ProductData) },
          LocalTypes = new Type[] { typeof(StockItem) },
          AcumaticaPrimaryType = typeof(PX.Objects.IN.InventoryItem),
          AcumaticaPrimarySelect = typeof(Search<PX.Objects.IN.InventoryItem.inventoryCD, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>),
          URL = "products/{0}",
          Requires = new string[] { }
      )]
    //[BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductImage, EntityName = BCCaptions.ProductImage, AcumaticaType = typeof(BCInventoryFileUrls))]
    [BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductImage, EntityName = BCCaptions.ProductImage, AcumaticaType = typeof(BCUploadFileWithIDSelectorExt))]
    [BCProcessorDetail(EntityType = BCEntitiesAttribute.RelatedItem, EntityName = BCCaptions.RelatedItem, AcumaticaType = typeof(PX.Objects.IN.InventoryItem))]
    [BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductOption, EntityName = BCCaptions.ProductOption, AcumaticaType = typeof(PX.Objects.CS.CSAttribute))]
    [BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductOptionValue, EntityName = BCCaptions.ProductOption, AcumaticaType = typeof(PX.Objects.CS.CSAttributeDetail))]
    [BCProcessorRealtime(PushSupported = true, HookSupported = false,
        PushSources = new String[] { "BC-PUSH-Stocks" }, PushDestination = BCConstants.PushNotificationDestination)]
    public class CCStockItemProcessor : CCProductProcessor<CCStockItemProcessor, CCStockItemEntityBucket, MappedStockItem>, IProcessor
    {
        protected BCBinding currentBinding;
        protected BCBindingExt currentBindingExt;
        protected BCBindingCustom currentCustomBinding;
        
        #region Constructor
        public override void Initialise(IConnector connector, ConnectorOperation operation)
        {
            base.Initialise(connector, operation);
        }
        #endregion Constructor

        #region Common
        public override MappedStockItem PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
        {
            StockItem impl = cbapi.GetByID(localID,
                new StockItem()
                {
                    ReturnBehavior = ReturnBehavior.OnlySpecified,
                    Attributes = new List<AttributeValue>() { new AttributeValue() },
                    Categories = new List<CategoryStockItem>() { new CategoryStockItem() },
                    CrossReferences = new List<InventoryItemCrossReference>() { new InventoryItemCrossReference() },
                    VendorDetails = new List<StockItemVendorDetail>() { new StockItemVendorDetail() },
                    FileURLs = new List<InventoryFileUrls>() { new InventoryFileUrls() }
                });
            if (impl == null) return null;

            MappedStockItem obj = new MappedStockItem(impl, impl.SyncID, impl.SyncTime);

            return obj;
        }

        public override MappedStockItem PullEntity(String externID, String externalInfo)
        {
            ProductData data = productDataProvider.GetByID(externID);
            if (data == null) return null;

            MappedStockItem obj = new MappedStockItem(data, data.Id?.ToString(), data.DateUpdatedAt.ToDate(false));

            return obj;
        }
        #endregion

        #region Export
        public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            //Getting all the products from Acumatica 
            IEnumerable<Core.API.StockItem> acumItems = cbapi.GetAll<StockItem>(new Core.API.StockItem { InventoryID = new StringReturn() },
                minDateTime, maxDateTime, filters);

            int countNum = 0;
            List<IMappedEntity> mappedList = new List<IMappedEntity>();

            //Create a bucket for each Acumatica customer
            foreach (StockItem item in acumItems)
            {
                IMappedEntity mappedItem = new MappedStockItem(item, item.SyncID, item.SyncTime);

                mappedList.Add(mappedItem);
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

        public override EntityStatus GetBucketForExport(CCStockItemEntityBucket bucket, BCSyncStatus bcstatus)
        {
            //Search product in Acumatica
            //StockItem product = BCExtensions.GetSharedSlot<StockItem>(bcstatus.LocalID.ToString()) ?? cbapi.GetByID<StockItem>(bcstatus.LocalID, GetCustomFieldsForExport());
            StockItem product = cbapi.GetByID<StockItem>(bcstatus.LocalID,
                new StockItem()
                {
                    ReturnBehavior = ReturnBehavior.OnlySpecified,
                    Attributes = new List<AttributeValue>() { new AttributeValue() },
                    Categories = new List<CategoryStockItem>() { new CategoryStockItem() },
                    CrossReferences = new List<InventoryItemCrossReference>() { new InventoryItemCrossReference() },
                    VendorDetails = new List<StockItemVendorDetail>() { new StockItemVendorDetail() },
                    FileURLs = new List<InventoryFileUrls>() { new InventoryFileUrls() }

                },
                GetCustomFieldsForExport()
                );
            if (product == null) return EntityStatus.None;
            //Create the bucket
            MappedStockItem mappedProduct = bucket.Product = bucket.Product.Set(product, product.SyncID, product.SyncTime);
            EntityStatus status = EnsureStatus(mappedProduct, SyncDirection.Export);
            if (mappedProduct.Local.Categories != null)
            {
                foreach (CategoryStockItem item in mappedProduct.Local.Categories)
                {
                    if (!SalesCategories.ContainsKey(item.CategoryID.Value.Value))
                    {
                        BCItemSalesCategory implCat = cbapi.Get(new BCItemSalesCategory() { CategoryID = new IntSearch() { Value = item.CategoryID.Value } });

                        //CategoryStockItem lCat = cbapi.Get(new CategoryStockItem() { CategoryID = new IntSearch() { Value = item.CategoryID.Value } });
                        if (implCat == null) continue;
                        if (item.CategoryID.Value != null)
                        {
                            SalesCategories[item.CategoryID.Value.Value] = implCat.Description.Value;
                        }
                    }
                }
            }
            return status;
        }

        public override void MapBucketExport(CCStockItemEntityBucket bucket, IMappedEntity existing)
        {
            //Getting objects from the EntityBucket
            MappedStockItem mappedProduct = bucket.Product;
            //Check if the product already exists
            ProductData existingProduct = existing?.Extern as ProductData;
            
            //Availability
            string storeAvailability = BCItemAvailabilities.Convert(GetBindingExt<BCBindingExt>().Availability);

            bool? currentlyPublished = existingProduct?.Active == Status.Active;

            //Setting Acumatica product to an object
            StockItem stockItem = mappedProduct.Local;

            //Create a new External product
            ProductData externalProduct = mappedProduct.Extern = new ProductData();

            //Product properties for External
            externalProduct.Description = helper.ClearHTMLContent(stockItem.Content?.Value);
            externalProduct.Id = mappedProduct.ExternID?.ToString() ?? string.Empty;
            externalProduct.Name = stockItem.InventoryID?.Value ?? string.Empty;
            externalProduct.Price = stockItem.CurySpecificPrice?.Value.ToString() != "0" ? stockItem.CurySpecificPrice.Value.ToString() : CustomConstants.PRICE_DEFAULT;
            externalProduct.ShortDescription = stockItem.Description?.Value ?? string.Empty;
            externalProduct.Sku = stockItem.InventoryID?.Value.ToString().Replace(" ", "-") ?? string.Empty;
            externalProduct.TypeId = ProductType.Simple;
            externalProduct.Weight = stockItem.DimensionWeight?.Value.ToString() ?? string.Empty;
            externalProduct.TaxClassId = CustomConstants.TAXABLE_ITEM; // TODO: map to 0 or 2 depending on tax category. stockItem.TaxCategory.Value ?? 
            externalProduct.AttributeSetId = CustomConstants.ATTRIBUTE_ID_DEFAULT;
            externalProduct.DateUpdatedAt = stockItem.LastModified?.Value ?? null;

            if (!string.IsNullOrEmpty(stockItem.MetaDescription?.Value))
                externalProduct.MetaDescription = stockItem.MetaDescription?.Value ?? string.Empty;

            string availability = stockItem.Availability?.Value;
            string visibility = stockItem?.Visibility?.Value;
            string notAvailable = stockItem.NotAvailable?.Value;
            if (availability == null || availability == BCCaptions.StoreDefault) availability = storeAvailability;
            if (visibility == null || visibility == BCCaptions.StoreDefault) visibility = BCItemVisibility.Convert(GetBindingExt<BCBindingExt>().Visibility);
            if (notAvailable == null || notAvailable == BCCaptions.StoreDefault) notAvailable = BCItemNotAvailModes.Convert(GetBindingExt<BCBindingExt>().NotAvailMode);

            SetProductStatus(externalProduct, stockItem.ItemStatus?.Value, availability, visibility);


        }


        public override object GetAttribute(CCStockItemEntityBucket bucket, string attributeID)
        {
            MappedStockItem obj = bucket.Product;
            StockItem impl = obj.Local;
            return impl.Attributes?.Where(x => string.Equals(x?.AttributeID?.Value, attributeID, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

        }

        public override void AddAttributeValue(CCStockItemEntityBucket bucket, string attributeID, object attributeValue)
        {
            MappedStockItem obj = bucket.Product;
            StockItem impl = obj.Local;
            impl.Attributes = impl.Attributes ?? new List<AttributeValue>();
            AttributeValue attributeDetail = new AttributeValue();
            attributeDetail.AttributeID = new StringValue() { Value = attributeID };
            attributeDetail.ValueDescription = new StringValue() { Value = attributeValue.ToString() };
            impl.Attributes.Add(attributeDetail);
        }

        public override void SaveBucketExport(CCStockItemEntityBucket bucket, IMappedEntity existing, string operation)
        {
            //Setting the bucket to an object
            MappedStockItem mappedProduct = bucket.Product;

            //External Product
            ProductData productData = mappedProduct.Extern;
            string id = "";
            string response = "";
            try
            {
                //If not exists in the External system, we create a new one
                if (mappedProduct.ExternID == null || existing == null)
                {
                    id = productDataProvider.Create(mappedProduct.Extern);
                }
                else //Otherwise, we update it
                {
                    response = productDataProvider.Update(mappedProduct.Extern, mappedProduct.ExternID.ToInt() ?? 0);
                    id = mappedProduct.ExternID;

                    if (!HttpStatusCode.OK.ToString().Equals(response))
                    {
                        PXTrace.WriteError(ConnectorMessages.UpdateError, "stock item", response);
                        throw new PXException(ConnectorMessages.UpdateError, "stock item",response);
                    }
                }

                mappedProduct.AddExtern(productData, id, DateTime.UtcNow);

                PXResultset<UploadFile> filefound = PXSelectJoin<UploadFile,
                    InnerJoin<NoteDoc, On<NoteDoc.fileID, Equal<UploadFile.fileID>>,
                        InnerJoin<UploadFileRevision, On<UploadFileRevision.fileID, Equal<NoteDoc.fileID>>,
                            InnerJoin<InventoryItem, On<InventoryItem.noteID, Equal<NoteDoc.noteID>>>>>,
                        Where<InventoryItem.noteID, Equal<Required<InventoryItem.noteID>>>>.Select(this, mappedProduct?.Local?.NoteID.Value);
                var fileList = new List<String>();

                foreach (UploadFile file in filefound)
                {
                    fileList.Add(file.Name);
                }

                if (fileList.Count() > 0)
                {
                    SaveMainImage(mappedProduct, mappedProduct?.Local?.InventoryID.Value, fileList);
                }
                UpdateStatus(mappedProduct, operation);

            }
            catch (Exception ex)
            {
                PXTrace.WriteError("SAVE BUCKET ERROR: "+Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                throw;
            }
        }
        #endregion Export

        #region Import
        public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
        {
            
            IEnumerable<ProductData> products = productDataProvider.GetAll();

            if (products?.Count() > 0)
            {
                foreach (ProductData product in products)
                {
                    CCStockItemEntityBucket bucket = CreateBucket();

                    MappedStockItem obj = bucket.Product = bucket.Product.Set(product, product.Id?.ToString(), product.DateUpdatedAt.ToDate(true));
                    EntityStatus status = EnsureStatus(obj, SyncDirection.Import);
              
                }
            }
        }

        public override EntityStatus GetBucketForImport(CCStockItemEntityBucket bucket, BCSyncStatus syncStatus)
        {
            ProductData data = productDataProvider.GetByID(syncStatus.ExternID);
            if (data == null) return EntityStatus.None;
            MappedStockItem obj = bucket.Product = bucket.Product.Set(data, data.Id?.ToString(), data.DateUpdatedAt.ToDate(true));
            EntityStatus status = EnsureStatus(obj, SyncDirection.Import);

            if (data.Categories?.Count() > 0)
            {
                foreach (string categoryId in data.Categories)
                {
                    ProductCategoryData p = BCExtensions.GetSharedSlot<ProductCategoryData>(categoryId) ?? categoryDataProvider.GetByID(categoryId);
                    MappedCategory categoryObj = new MappedCategory(p, categoryId, DateTime.UtcNow);
                    EntityStatus categoryStatus = EnsureStatus(categoryObj);

                    if (categoryStatus == EntityStatus.Pending)
                    {
                        bucket.Categories.Add(categoryObj);
                    }
                }
            }

            return status;
        }

        public override void MapBucketImport(CCStockItemEntityBucket bucket, IMappedEntity existing)
        {
            BCBinding binding = GetBinding();
            MappedStockItem obj = bucket.Product;
            BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();
            BCBindingCustom customBinding = GetBindingExt<BCBindingCustom>();
            ProductData data = obj.Extern;
            StockItem local = obj.Local = new StockItem();
            StockItem existingItem = existing?.Local as StockItem;

        }


        public override void SaveBucketImport(CCStockItemEntityBucket bucket, IMappedEntity existing, string operation)
        {
            MappedStockItem obj = bucket.Product;
            StockItem local = obj.Local;
            StockItem presented = existing?.Local as StockItem;

            UpdateStatus(obj, operation);
        }
        #endregion Import

    }
}
