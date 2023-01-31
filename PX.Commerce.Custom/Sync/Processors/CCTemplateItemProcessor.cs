using PX.Commerce.Custom.API.REST;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Api.ContractBased.Models;
using Serilog.Context;
using PX.Common;
using System.Net;
using System.Text.RegularExpressions;

namespace PX.Commerce.Custom
{
	public class CCTemplateItemEntityBucket : EntityBucketBase, IEntityBucket
	{
		public IMappedEntity Primary => Product;
		public IMappedEntity[] Entities => new IMappedEntity[] { Primary };
		public MappedTemplateItem Product;
		public Dictionary<string, Tuple<long?, string, InventoryPolicy?>> VariantMappings = new Dictionary<string, Tuple<long?, string, InventoryPolicy?>>();
	}

	public class CCTemplateItemRestrictor : BCBaseRestrictor, IRestrictor
	{
		public virtual FilterResult RestrictExport(IProcessor processor, IMappedEntity mapped)
		{
			#region TemplateItems
			return base.Restrict<MappedTemplateItem>(mapped, delegate (MappedTemplateItem obj)
			{
				if (obj.Local != null && (obj.Local.Matrix == null || obj.Local.Matrix?.Count == 0))
				{
					return new FilterResult(FilterStatus.Invalid,
						PXMessages.LocalizeFormatNoPrefixNLA(BCMessages.LogTemplateSkippedNoMatrix, obj.Local.InventoryID?.Value ?? obj.Local.SyncID.ToString()));
				}

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

	[BCProcessor(typeof(CCConnector), BCEntitiesAttribute.ProductWithVariant, BCCaptions.TemplateItem,
		IsInternal = false,
		Direction = SyncDirection.Export,
		PrimaryDirection = SyncDirection.Export,
		PrimarySystem = PrimarySystem.Local,
		PrimaryGraph = typeof(PX.Objects.IN.InventoryItemMaint),
		ExternTypes = new Type[] { typeof(ProductData) },
		LocalTypes = new Type[] { typeof(TemplateItems) },
		AcumaticaPrimaryType = typeof(PX.Objects.IN.InventoryItem),
		AcumaticaPrimarySelect = typeof(Search<PX.Objects.IN.InventoryItem.inventoryCD, Where<PX.Objects.IN.InventoryItem.isTemplate, Equal<True>>>),
		AcumaticaFeaturesSet = typeof(FeaturesSet.matrixItem),
		URL = "products/{0}",
		Requires = new string[] { }
	)]
	//[BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductImage, EntityName = BCCaptions.ProductImage, AcumaticaType = typeof(BCInventoryFileUrls))]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductImage, EntityName = BCCaptions.ProductImage, AcumaticaType = typeof(BCUploadFileWithIDSelectorExt))]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.RelatedItem, EntityName = BCCaptions.RelatedItem, AcumaticaType = typeof(PX.Objects.IN.InventoryItem))]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductOption, EntityName = BCCaptions.ProductOption, AcumaticaType = typeof(PX.Objects.CS.CSAttribute))]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.ProductOptionValue, EntityName = BCCaptions.ProductOption, AcumaticaType = typeof(PX.Objects.CS.CSAttributeDetail))]
	[BCProcessorDetail(EntityType = BCEntitiesAttribute.Variant, EntityName = BCCaptions.Variant, AcumaticaType = typeof(PX.Objects.IN.InventoryItem))]
	[BCProcessorRealtime(PushSupported = true, HookSupported = false,
		PushSources = new String[] { "BC-PUSH-Variants" }, PushDestination = BCConstants.PushNotificationDestination)]
	[BCProcessorExternCustomField(BCAPICaptions.Matrix, BCAPICaptions.Matrix, nameof(TemplateItems.Matrix), typeof(TemplateItems), readAsCollection: true)]
	public class CCTemplateItemProcessor : CCProductProcessor<CCTemplateItemProcessor, CCTemplateItemEntityBucket, MappedTemplateItem>, IProcessor
	{
		#region Constructor
		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);

			var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());
		}
		#endregion

		#region Common
		public override MappedTemplateItem PullEntity(Guid? localID, Dictionary<string, object> externalInfo)
		{
			TemplateItems impl = cbapi.GetByID(localID,
				new TemplateItems()
				{
					ReturnBehavior = ReturnBehavior.OnlySpecified,
					Attributes = new List<AttributeValue>() { new AttributeValue() },
					Categories = new List<CategoryStockItem>() { new CategoryStockItem() },
					FileURLs = new List<InventoryFileUrls>() { new InventoryFileUrls() },
					Matrix = new List<MatrixItems>() { new MatrixItems() }
				});
			if (impl == null) return null;

			MappedTemplateItem obj = new MappedTemplateItem(impl, impl.SyncID, impl.SyncTime);

			return obj;
		}
		public override MappedTemplateItem PullEntity(String externID, String externalInfo)
		{
			ProductData data = productDataProvider.GetByID(externID);
			if (data == null) return null;

			MappedTemplateItem obj = new MappedTemplateItem(data, data.Id?.ToString(), data.DateUpdatedAt.ToDate(false));

			return obj;
		}
		#endregion

		#region Import
		public override void FetchBucketsForImport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
			//No DateTime filtering for Category
			FilterProducts filter = new FilterProducts
			{
				UpdatedAtMin = minDateTime == null ? (DateTime?)null : minDateTime.Value.ToLocalTime().AddSeconds(-1 ),
				UpdatedAtMax = maxDateTime == null ? (DateTime?)null : maxDateTime.Value.ToLocalTime()
			};

			IEnumerable<ProductData> datas = productDataProvider.GetAll(filter);
			if (datas?.Count() > 0)
			{
				foreach (ProductData data in datas)
				{
					CCTemplateItemEntityBucket bucket = CreateBucket();

					MappedTemplateItem obj = bucket.Product = bucket.Product.Set(data, data.Id?.ToString(), data.DateUpdatedAt.ToDate(false));
					EntityStatus status = EnsureStatus(obj, SyncDirection.Import);
					
				}
			}
		}
		public override EntityStatus GetBucketForImport(CCTemplateItemEntityBucket bucket, BCSyncStatus syncstatus)
		{
			ProductData data = productDataProvider.GetByID(syncstatus.ExternID);
			if (data == null) return EntityStatus.None;

			
			MappedTemplateItem obj = bucket.Product = bucket.Product.Set(data, data.Id?.ToString(), data.DateUpdatedAt.ToDate(false));
			EntityStatus status = EnsureStatus(obj, SyncDirection.Import);

			return status;
		}

		public override void MapBucketImport(CCTemplateItemEntityBucket bucket, IMappedEntity existing)
		{
			BCBinding binding = GetBinding();
			MappedTemplateItem obj = bucket.Product;
			BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();
			BCBindingCustom customBinding = GetBindingExt<BCBindingCustom>();
			ProductData data = obj.Extern;
			TemplateItems local = obj.Local = new TemplateItems();
			TemplateItems existingItem = existing?.Local as TemplateItems;

		}
		public override void SaveBucketImport(CCTemplateItemEntityBucket bucket, IMappedEntity existing, String operation)
		{
			MappedTemplateItem obj = bucket.Product;
			TemplateItems local = obj.Local;
			TemplateItems presented = existing?.Local as TemplateItems;

			UpdateStatus(obj, operation);
		}
		#endregion

		#region Export
		public override IEnumerable<MappedTemplateItem> PullSimilar(ILocalEntity entity, out string uniqueField)
		{
			TemplateItems localEnity = (TemplateItems)entity;
			uniqueField = localEnity?.InventoryID?.Value;
			IEnumerable<ProductData> datas = null;
			List<string> matrixIds = new List<string>();
			if (localEnity?.Matrix?.Count > 0)
			{
				matrixIds = localEnity.Matrix.Select(x => x?.InventoryID?.Value).ToList();
				
			}

			return datas == null ? null : datas.Select(data => new MappedTemplateItem(data, data.Id.ToString(), data.DateUpdatedAt.ToDate(false)));
		}

		public override void FetchBucketsForExport(DateTime? minDateTime, DateTime? maxDateTime, PXFilterRow[] filters)
		{
			TemplateItems item = new TemplateItems()
			{
				InventoryID = new StringReturn(),
				IsStockItem = new BooleanReturn(),
				Matrix = new List<MatrixItems>() { new MatrixItems() { InventoryID = new StringReturn() } },
				Categories = new List<CategoryStockItem>() { new CategoryStockItem() { CategoryID = new IntReturn() } },
				ExportToExternal = new BooleanReturn()
			};
			IEnumerable<TemplateItems> impls = cbapi.GetAll<TemplateItems>(item, minDateTime, maxDateTime, filters);

			if (impls != null)
			{
				int countNum = 0;
				List<IMappedEntity> mappedList = new List<IMappedEntity>();
				foreach (TemplateItems impl in impls)
				{
					IMappedEntity obj = new MappedTemplateItem(impl, impl.SyncID, impl.SyncTime);

					mappedList.Add(obj);
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
		}
		public override EntityStatus GetBucketForExport(CCTemplateItemEntityBucket bucket, BCSyncStatus syncstatus)
		{
			TemplateItems impl = cbapi.GetByID(syncstatus.LocalID,
				new TemplateItems()
				{
					ReturnBehavior = ReturnBehavior.OnlySpecified,
					Attributes = new List<AttributeValue>() { new AttributeValue() },
					Categories = new List<CategoryStockItem>() { new CategoryStockItem() },
					FileURLs = new List<InventoryFileUrls>() { new InventoryFileUrls() },
					Matrix = new List<MatrixItems>() { new MatrixItems() }
				}, GetCustomFieldsForExport());
			if (impl == null || impl.Matrix?.Count == 0) return EntityStatus.None;

			impl.AttributesDef = new List<AttributeDefinition>();
			impl.AttributesValues = new List<AttributeValue>();
			int? inventoryID = null;
			foreach (PXResult<CSAttribute, CSAttributeGroup, INItemClass, InventoryItem> attributeDef in PXSelectJoin<CSAttribute,
			   InnerJoin<CSAttributeGroup, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>,
			   InnerJoin<INItemClass, On<INItemClass.itemClassID, Equal<CSAttributeGroup.entityClassID>>,
			   InnerJoin<InventoryItem, On<InventoryItem.itemClassID, Equal<INItemClass.itemClassID>>>>>,
			  Where<InventoryItem.isTemplate, Equal<True>,
			  And<InventoryItem.noteID, Equal<Required<InventoryItem.noteID>>,
			  And<CSAttribute.controlType, Equal<Required<CSAttribute.controlType>>,
			  And<CSAttributeGroup.isActive, Equal<True>,
			  And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>
			  >>>>>>.Select(this, impl.Id, 2))
			{
				AttributeDefinition def = new AttributeDefinition();
				var inventory = (InventoryItem)attributeDef;
				inventoryID = inventory.InventoryID;
				var attribute = (CSAttribute)attributeDef;
				var attributeGroup = (CSAttributeGroup)attributeDef;
				def.AttributeID = attribute.AttributeID.ValueField();
				def.Description = attribute.Description.ValueField();
				def.NoteID = attribute.NoteID.ValueField();
				def.Order = attributeGroup.SortOrder.ValueField();

				def.Values = new List<AttributeDefinitionValue>();
				var attributedetails = PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>>>.Select(this, def.AttributeID.Value);
				foreach (CSAttributeDetail value in attributedetails)
				{
					AttributeDefinitionValue defValue = new AttributeDefinitionValue();
					defValue.NoteID = value.NoteID.ValueField();
					defValue.ValueID = value.ValueID.ValueField();
					defValue.Description = value.Description.ValueField();
					defValue.SortOrder = (value.SortOrder ?? 0).ToInt().ValueField();
					def.Values.Add(defValue);
				}

				if (def != null)
					impl.AttributesDef.Add(def);
			}

			foreach (PXResult<InventoryItem, CSAnswers> attributeDef in PXSelectJoin<InventoryItem,
			   InnerJoin<CSAnswers, On<InventoryItem.noteID, Equal<CSAnswers.refNoteID>>>,
			  Where<InventoryItem.templateItemID, Equal<Required<InventoryItem.templateItemID>>
			  >>.Select(this, inventoryID))
			{
				var inventory = (InventoryItem)attributeDef;
				var attribute = (CSAnswers)attributeDef;
				AttributeValue def = new AttributeValue();
				def.AttributeID = attribute.AttributeID.ValueField();
				def.NoteID = inventory.NoteID.ValueField();
				def.InventoryID = inventory.InventoryCD.ValueField();
				def.Value = attribute.Value.ValueField();
				impl.AttributesValues.Add(def);
			}
			impl.InventoryItemID = inventoryID;

			MappedTemplateItem obj = bucket.Product = bucket.Product.Set(impl, impl.SyncID, impl.SyncTime);
			EntityStatus status = EnsureStatus(obj, SyncDirection.Export);
			
			if (impl.Matrix?.Count > 0)
			{
				var activeMatrixItems = impl.Matrix.Where(x => x?.ItemStatus?.Value == PX.Objects.IN.Messages.Active);
				if (activeMatrixItems.Count() == 0)
				{
					throw new PXException(BCMessages.NoMatrixCreated);
				}
				
				foreach (var item in activeMatrixItems)
				{
					if (!bucket.VariantMappings.ContainsKey(item.InventoryID?.Value))
						bucket.VariantMappings.Add(item.InventoryID?.Value, null);
				}
			}
			if (obj.Local.Categories != null)
			{
				foreach (CategoryStockItem item in obj.Local.Categories)
				{
					if (!SalesCategories.ContainsKey(item.CategoryID.Value.Value))
					{
						BCItemSalesCategory implCat = cbapi.Get<BCItemSalesCategory>(new BCItemSalesCategory() { CategoryID = new IntSearch() { Value = item.CategoryID.Value } });
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

		public override void MapBucketExport(CCTemplateItemEntityBucket bucket, IMappedEntity existing)
		{
			MappedTemplateItem mappedProduct = bucket.Product;
			
			//Check if the product already exists
			ProductData existingProduct = existing?.Extern as ProductData;

			//Availability
			string storeAvailability = BCItemAvailabilities.Convert(GetBindingExt<BCBindingExt>().Availability);

			bool? currentlyPublished = existingProduct?.Active == Status.Active;

			//Setting Acumatica product to an object
			TemplateItems stockItem = mappedProduct.Local;

			//Create a new External product
			ProductData externalProduct = mappedProduct.Extern = new ProductData();

			//Product properties for External
			externalProduct.Description = helper.ClearHTMLContent(stockItem.Content?.Value);
			externalProduct.Id = mappedProduct.ExternID?.ToString() ?? string.Empty;
			externalProduct.Name = stockItem.InventoryID?.Value ?? string.Empty;
			externalProduct.Price = stockItem.CurySpecificPrice?.Value.ToString() != "0" ? stockItem.CurySpecificPrice.Value.ToString() : CustomConstants.PRICE_DEFAULT;
			externalProduct.ShortDescription = stockItem.Description?.Value ?? string.Empty;
			externalProduct.Sku = stockItem.InventoryID?.Value.ToString().Replace(" ", "-") ?? string.Empty;
			externalProduct.TypeId = ProductType.Configurable;
			externalProduct.Weight = stockItem.DimensionWeight?.Value.ToString() ?? string.Empty;
			externalProduct.TaxClassId = CustomConstants.TAXABLE_ITEM; // TODO: map to 0 or 2 depending on tax category. stockItem.TaxCategory.Value ?? 
			externalProduct.AttributeSetId = CustomConstants.ATTRIBUTE_ID_DEFAULT;
			externalProduct.DateUpdatedAt = stockItem.LastModified?.Value ?? null;

			string availability = stockItem.Availability?.Value;
			string visibility = stockItem?.Visibility?.Value;
			string notAvailable = stockItem.NotAvailable?.Value;
			if (availability == null || availability == BCCaptions.StoreDefault) availability = storeAvailability;
			if (visibility == null || visibility == BCCaptions.StoreDefault) visibility = BCItemVisibility.Convert(GetBindingExt<BCBindingExt>().Visibility);
			if (notAvailable == null || notAvailable == BCCaptions.StoreDefault) notAvailable = BCItemNotAvailModes.Convert(GetBindingExt<BCBindingExt>().NotAvailMode);

			SetProductStatus(externalProduct, stockItem.ItemStatus?.Value, availability, visibility);


		}

		public override object GetAttribute(CCTemplateItemEntityBucket bucket, string attributeID)
		{
			MappedTemplateItem obj = bucket.Product;
			TemplateItems impl = obj.Local;
			return impl.Attributes?.Where(x => string.Equals(x?.AttributeDescription?.Value, attributeID, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

		}

		public override void SaveBucketExport(CCTemplateItemEntityBucket bucket, IMappedEntity existing, String operation)
		{
			//Setting the bucket to an object
			MappedTemplateItem mappedProduct = bucket.Product;

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
						PXTrace.WriteError(ConnectorMessages.UpdateError,"item", response);
						throw new PXException(ConnectorMessages.UpdateError, "item", response);
					}
				}

				mappedProduct.AddExtern(productData, id, DateTime.UtcNow);

				//SaveImages(mappedProduct, mappedProduct?.Local?.FileURLs);
				UpdateStatus(mappedProduct, operation);

			}
			catch (Exception ex)
			{
				PXTrace.WriteError("SAVE BUCKET ERROR: " + Newtonsoft.Json.JsonConvert.SerializeObject(ex));
				throw;
			}
		}
		#endregion

		public virtual bool IsVariantActive(MatrixItems item)
		{
			return !(item.ItemStatus?.Value == PX.Objects.IN.Messages.Inactive || item.ItemStatus?.Value == PX.Objects.IN.Messages.ToDelete || item.ItemStatus?.Value == PX.Objects.IN.Messages.NoSales)
				&& item.ExportToExternal.Value == true;
		}
	}
}
