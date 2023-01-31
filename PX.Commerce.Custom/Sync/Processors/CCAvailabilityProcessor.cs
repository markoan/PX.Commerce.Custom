using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.Common;
using Serilog.Context;
using System.Reflection;
using PX.Commerce.Custom.API.REST;
using System.Net;
using PX.Api.ContractBased.Models;

namespace PX.Commerce.Custom
{
	public class CCAvailabilityEntityBucket : EntityBucketBase, IEntityBucket
	{
		public IMappedEntity Primary => Product;
		public IMappedEntity[] Entities => new IMappedEntity[] { Primary, StockItem };


		public override IMappedEntity[] PreProcessors =>
			new IMappedEntity[] { StockItem };

		public MappedAvailability Product;
		public MappedStockItem StockItem;
	}

	[BCProcessor(typeof(CCConnector), BCEntitiesAttribute.ProductAvailability, BCCaptions.ProductAvailability,
		IsInternal = false,
		Direction = SyncDirection.Export,
		PrimaryDirection = SyncDirection.Export,
		PrimarySystem = PrimarySystem.Local,
		PrimaryGraph = typeof(PX.Objects.IN.InventorySummaryEnq),
		ExternTypes = new Type[] { },
		LocalTypes = new Type[] { },
		GIScreenID = BCConstants.GenericInquiryAvailability,
		GIResult = typeof(StorageDetails),
		AcumaticaPrimaryType = typeof(InventoryItem),
		URL = "products/{0}",
		Requires = new string[] { },
		RequiresOneOf = new string[] { BCEntitiesAttribute.StockItem + "." + BCEntitiesAttribute.ProductWithVariant }
	)]
	[BCProcessorRealtime(PushSupported = true, HookSupported = false,
		PushSources = new String[] { "BC-PUSH-AvailabilityStockItem", "BC-PUSH-AvailabilityTemplates" }, PushDestination = BCConstants.PushNotificationDestination)]
	public class CCAvailabilityProcessor : AvailabilityProcessorBase<CCStockItemProcessor, CCAvailabilityEntityBucket, MappedAvailability>, IProcessor
	{
		protected ProductRestDataProvider productDataProvider;

		#region Constructor
		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);

			productDataProvider = new ProductRestDataProvider(CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>()));
		}
		#endregion

		#region Common
		public override void NavigateLocal(IConnector connector, ISyncStatus status)
		{
			PX.Objects.IN.InventorySummaryEnq extGraph = PXGraph.CreateInstance<PX.Objects.IN.InventorySummaryEnq>();
			InventorySummaryEnqFilter filter = extGraph.Filter.Current;
			InventoryItem item = PXSelect<InventoryItem, Where<InventoryItem.noteID, Equal<Required<InventoryItem.noteID>>>>.Select(this, status.LocalID);
			filter.InventoryID = item.InventoryID;

			if (filter.InventoryID != null)
				throw new PXRedirectRequiredException(extGraph, "Navigation") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
		}
		public override MappedAvailability PullEntity(Guid? localID, Dictionary<string, object> fields)
		{
			if (localID == null) return null;
			DateTime? timeStamp = fields.Where(f => f.Key.EndsWith(nameof(BCEntity.LastModifiedDateTime), StringComparison.InvariantCultureIgnoreCase)).Select(f => f.Value).LastOrDefault()?.ToDate();
			int? parentID = fields.Where(f => f.Key.EndsWith(nameof(BCSyncStatus.SyncID), StringComparison.InvariantCultureIgnoreCase)).Select(f => f.Value).LastOrDefault()?.ToInt();
			localID = fields.Where(f => f.Key.EndsWith("TemplateItem_noteID", StringComparison.InvariantCultureIgnoreCase)).Select(f => f.Value).LastOrDefault()?.ToGuid() ?? localID;
			return new MappedAvailability(new StorageDetailsResult(), localID, timeStamp, parentID);
		}
		#endregion

		#region Import
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public override void FetchBucketsImport()
		{

		}
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public override List<CCAvailabilityEntityBucket> GetBucketsImport(List<BCSyncStatus> ids)
		{
			return null;
		}
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public override void MapBucketImport(CCAvailabilityEntityBucket bucket, IMappedEntity existing)
		{
			throw new NotImplementedException();
		}
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public override void SaveBucketsImport(List<CCAvailabilityEntityBucket> buckets)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Export

		public override void FetchBucketsExport()
		{
			DateTime? startDate = Operation.PrepareMode == PrepareMode.Incremental ? GetEntityStats()?.LastIncrementalExportDateTime : Operation.StartDate;
			IEnumerable<StorageDetailsResult> results = Enumerable.Empty<StorageDetailsResult>();
			if (GetEntity(BCEntitiesAttribute.StockItem)?.IsActive == true)
			{
				results = results.Concat(FetchStorageDetails(GetBindingExt<BCBindingExt>(), startDate, Operation.EndDate, FetchAvailabilityBaseCommandForStockItem));
			}
			if (GetEntity(BCEntitiesAttribute.ProductWithVariant)?.IsActive == true)
			{
				results = results.Concat(FetchStorageDetails(GetBindingExt<BCBindingExt>(), startDate, Operation.EndDate, FetchAvailabilityBaseCommandForTemplateItem));
			}

			foreach (StorageDetailsResult lineItem in results)
			{
				DateTime? lastModified = new DateTime?[] { lineItem.SiteLastModifiedDate?.Value, lineItem.InventoryLastModifiedDate?.Value }.Where(d => d != null).Select(d => d.Value).Max();
				MappedAvailability obj = new MappedAvailability(lineItem, lineItem.InventoryNoteID.Value, lastModified, lineItem.ParentSyncId.Value);
				EntityStatus status = EnsureStatus(obj, SyncDirection.Export);
				if (status == EntityStatus.Deleted) status = EnsureStatus(obj, SyncDirection.Export, resync: true);
			}
		}

		public override List<CCAvailabilityEntityBucket> GetBucketsExport(List<BCSyncStatus> syncIDs)
		{
			BCEntityStats entityStats = GetEntityStats();
			BCBinding binding = GetBinding();
			BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();
			List<CCAvailabilityEntityBucket> buckets = new List<CCAvailabilityEntityBucket>();

			var warehouses = new Dictionary<int, INSite>();
			List<BCLocations> locationMappings = BCLocationSlot.GetBCLocations(bindingExt.BindingID);
			Dictionary<int, Dictionary<int, PX.Objects.IN.INLocation>> siteLocationIDs = BCLocationSlot.GetWarehouseLocations(bindingExt.BindingID);

			if (bindingExt.WarehouseMode == BCWarehouseModeAttribute.SpecificWarehouse)
			{
				warehouses = BCLocationSlot.GetWarehouses(bindingExt.BindingID);
			}
			Boolean anyLocation = locationMappings.Any(x => x.LocationID != null);

			IEnumerable<StorageDetailsResult> response = GetStorageDetailsResults(bindingExt, syncIDs);

			if (response == null || response.Any() == false) return buckets;

			List<StorageDetailsResult> results = new List<StorageDetailsResult>();
			foreach (var detailsGroup in response.GroupBy(r => new { InventoryID = r.InventoryCD?.Value, /*SiteID = r.SiteID?.Value*/ }))
			{
				if (detailsGroup.First().Availability?.Value == BCItemAvailabilities.DoNotUpdate)
					continue;
				StorageDetailsResult result = detailsGroup.First();
				result.SiteLastModifiedDate = detailsGroup.Where(d => d.SiteLastModifiedDate?.Value != null).Select(d => d.SiteLastModifiedDate.Value).Max().ValueField();
				result.LocationLastModifiedDate = detailsGroup.Where(d => d.LocationLastModifiedDate?.Value != null).Select(d => d.LocationLastModifiedDate.Value).Max().ValueField();
				result.SiteOnHand = detailsGroup.Sum(k => k.SiteOnHand?.Value ?? 0m).ValueField();
				result.SiteAvailable = detailsGroup.Sum(k => k.SiteAvailable?.Value ?? 0m).ValueField();
				result.SiteAvailableforIssue = detailsGroup.Sum(k => k.SiteAvailableforIssue?.Value ?? 0m).ValueField();
				result.SiteAvailableforShipping = detailsGroup.Sum(k => k.SiteAvailableforShipping?.Value ?? 0m).ValueField();
				if (bindingExt.WarehouseMode == BCWarehouseModeAttribute.SpecificWarehouse && locationMappings.Any() == false)//if warehouse is specific but nothing is configured in table
				{
					result.LocationOnHand = result.LocationAvailable = result.LocationAvailableforIssue = result.LocationAvailableforShipping = 0m.ValueField();
				}
				else
				{
					if (detailsGroup.Any(i => i.SiteID?.Value != null))
					{
						result.LocationOnHand = anyLocation ? detailsGroup.Where
							(k => warehouses.Count <= 0
							|| (siteLocationIDs.ContainsKey(k.SiteID?.Value ?? 0)
								&& (siteLocationIDs[k.SiteID?.Value ?? 0].Count == 0
								|| (k.LocationID?.Value != null
									&& siteLocationIDs[k.SiteID?.Value ?? 0].ContainsKey(k.LocationID.Value.Value)))))
							.Sum(k => k.LocationOnHand?.Value ?? 0m).ValueField() : null;       
						result.LocationAvailable = anyLocation ? detailsGroup.Where(
							k => warehouses.Count <= 0
							|| (siteLocationIDs.ContainsKey(k.SiteID?.Value ?? 0)
								&& (siteLocationIDs[k.SiteID?.Value ?? 0].Count == 0
								|| (k.LocationID?.Value != null
									&& siteLocationIDs[k.SiteID?.Value ?? 0].ContainsKey(k.LocationID.Value.Value)))))
							.Sum(k => k.LocationAvailable?.Value ?? 0m).ValueField() : null;
						result.LocationAvailableforIssue = anyLocation ? detailsGroup.Where(
							k => warehouses.Count <= 0
							|| (siteLocationIDs.ContainsKey(k.SiteID?.Value ?? 0)
								&& (siteLocationIDs[k.SiteID?.Value ?? 0].Count == 0
								|| (k.LocationID?.Value != null
								&& siteLocationIDs[k.SiteID?.Value ?? 0].ContainsKey(k.LocationID.Value.Value)))))
							.Sum(k => k.LocationAvailableforIssue?.Value ?? 0m).ValueField() : null;
						result.LocationAvailableforShipping = anyLocation ? detailsGroup.Where(
							k => warehouses.Count <= 0
							|| (siteLocationIDs.ContainsKey(k.SiteID?.Value ?? 0)
								&& (siteLocationIDs[k.SiteID?.Value ?? 0].Count == 0
								|| (k.LocationID?.Value != null
								&& siteLocationIDs[k.SiteID?.Value ?? 0].ContainsKey(k.LocationID.Value.Value)))))
							.Sum(k => k.LocationAvailableforShipping?.Value ?? 0m).ValueField() : null;
					}
					else
						result.LocationOnHand = result.LocationAvailable = result.LocationAvailableforIssue = result.LocationAvailableforShipping = null;
				}
				results.Add(result);
			}

			var allVariants = results.Where(x => x.TemplateItemID?.Value != null);

			if (results != null)
			{
				var stockItems = results.Where(x => x.TemplateItemID?.Value == null);
				if (stockItems != null)
				{
					foreach (StorageDetailsResult line in stockItems)
					{
						Guid? noteID = line.InventoryNoteID?.Value;
						DateTime? lastModified;
						if (line.IsTemplate?.Value == true)
						{
							line.VariantDetails = new List<StorageDetailsResult>();
							line.VariantDetails.AddRange(allVariants.Where(x => x.TemplateItemID?.Value == line.InventoryID.Value));
							if (line.VariantDetails.Count() == 0) continue;
							lastModified = line.VariantDetails.Select(x => new DateTime?[] { x.LocationLastModifiedDate?.Value, x.SiteLastModifiedDate?.Value, x.InventoryLastModifiedDate.Value }.Where(d => d != null).Select(d => d.Value).Max()).Max();
						}
						else
						{
							lastModified = new DateTime?[] { line.LocationLastModifiedDate?.Value, line.SiteLastModifiedDate?.Value, line.InventoryLastModifiedDate?.Value }.Where(d => d != null).Select(d => d.Value).Max();
						}

						CCAvailabilityEntityBucket bucket = new CCAvailabilityEntityBucket();
						MappedAvailability obj = bucket.Product = new MappedAvailability(line, noteID, lastModified, line.ParentSyncId.Value);

						//Search customer in Acumatica
						Core.API.StockItem product = BCExtensions.GetSharedSlot<Core.API.StockItem>(line.InventoryNoteID.Value.ToString()) ?? cbapi.GetByID<Core.API.StockItem>(line.InventoryNoteID.Value, GetCustomFieldsForExport());

						//if (product == null) return EntityStatus.None;
						//Add to bucket
						MappedStockItem mappedProduct = bucket.StockItem = bucket.StockItem.Set(product, product.SyncID, product.SyncTime);

						EntityStatus status = EnsureStatus(obj, SyncDirection.Export);

						obj.ParentID = line.ParentSyncId.Value;
						if (Operation.PrepareMode != PrepareMode.Reconciliation && Operation.PrepareMode != PrepareMode.Full && status != EntityStatus.Pending && Operation.SyncMethod != SyncMode.Force)
						{
							SynchronizeStatus(bucket.Product, BCSyncOperationAttribute.Reconfiguration);
							Statuses.Cache.Persist(PXDBOperation.Update);
							Statuses.Cache.Persisted(false);
							continue;
						}

						buckets.Add(bucket);
					}
				}
			}

			return buckets;
		}

		public override void MapBucketExport(CCAvailabilityEntityBucket bucket, IMappedEntity existing)
		{
			BCBinding binding = GetBinding();
			BCBindingExt bindingExt = GetBindingExt<BCBindingExt>();

			MappedAvailability obj = bucket.Product;

			StorageDetailsResult local = obj.Local;
			ProductQtyData product = obj.Extern = new ProductQtyData();

			MappedStockItem mapItem = bucket.StockItem;
			StockItem stockItem = mapItem.Local;

			obj.ExternID = product.Id = local.ProductExternID.Value;

			var last = ((ProductQtyData)existing);

			string availability = local.Availability?.Value;
			string notAvailMode = local.NotAvailMode?.Value;
			string visibility = stockItem?.Visibility?.Value;

			if (availability == null || availability == BCCaptions.StoreDefault) availability = BCItemVisibility.Convert(GetBindingExt<BCBindingExt>().Availability);
			if (visibility == null || visibility == BCCaptions.StoreDefault) visibility = BCItemVisibility.Convert(GetBindingExt<BCBindingExt>().Visibility);
			if (notAvailMode == null || notAvailMode == BCCaptions.StoreDefault) notAvailMode = BCItemNotAvailModes.Convert(GetBindingExt<BCBindingExt>().NotAvailMode);

			product.Active = last?.Active ?? Status.Active;
			product.Visibility = last?.Visibility ?? Visibility.Visible;
			StockData stockData = last?.StockData ?? new StockData();

			if (availability == BCItemAvailabilities.AvailableTrack)
			{
				stockData.ManageStock = Switch.On;
				stockData.UpdateStockStatus = 1;


				//Inventory Level
				stockData.Qty = GetInventoryLevel(bindingExt, local).ToString();

				//Not In Stock mode
				if ($"0{stockData.Qty}".ToDecimal() <= 0)
				{
					stockData.Qty = "0.0";
					switch (notAvailMode)
					{
						case BCItemNotAvailModes.DisableItem:
							stockData.Backorders = InventoryPolicy.Deny;
							product.Visibility = Visibility.Hidden;
							product.Active = Status.Inactive;
							break;
						case BCItemNotAvailModes.PreOrderItem:
							stockData.Backorders = InventoryPolicy.Backorder;
							break;
					}
				}

				if (local.IsTemplate?.Value == true)
				{
					product.TypeId = ProductType.Configurable;

					// Do something else for template items
				}
				else
				{
					product.TypeId = ProductType.Simple;

					// Do something else for simple items
				}

			}
			else
			{
				stockData.ManageStock = Switch.Off;
				stockData.UpdateStockStatus = 0;

				switch (availability)
				{
					case BCItemAvailabilities.AvailableSkip: product.Visibility = Visibility.Visible; break;
					case BCItemAvailabilities.Disabled: product.Visibility = Visibility.Hidden; break;
				}
			}

			// Status
			if (local.ItemStatus?.Value == InventoryItemStatus.Inactive || local.ItemStatus?.Value == InventoryItemStatus.MarkedForDeletion || local.ItemStatus?.Value == InventoryItemStatus.NoSales)
			{
				product.Active = Status.Inactive;
			}
			else
			{
				product.Active = Status.Active;
				if (visibility == BCCaptions.Invisible || availability == BCCaptions.Disabled)
				{
					product.Visibility = Visibility.Hidden;
				}
				else if (visibility == BCCaptions.Featured)
				{
					product.Visibility = Visibility.Search;
				}
				else
				{
					product.Visibility = Visibility.Visible;
				}
			}


			product.StockData = stockData;
		}

		public int GetInventoryLevel(BCBindingExt store, StorageDetailsResult detailsResult)
		{
			switch (store.AvailabilityCalcRule)
			{
				case BCAvailabilityLevelsAttribute.Available:
					return (int)(detailsResult.LocationAvailable?.Value ?? detailsResult.SiteAvailable.Value);
				case BCAvailabilityLevelsAttribute.AvailableForShipping:
					return (int)(detailsResult.LocationAvailableforShipping?.Value ?? detailsResult.SiteAvailableforShipping.Value);
				case BCAvailabilityLevelsAttribute.OnHand:
					return (int)(detailsResult.LocationOnHand?.Value ?? detailsResult.SiteOnHand.Value);
				default:
					return 0;
			}
		}

		public override void SaveBucketsExport(List<CCAvailabilityEntityBucket> buckets)
		{

			foreach(CCAvailabilityEntityBucket bucket in buckets)
            {
				Exception Error = null;

				var response = productDataProvider.UpdateQty(bucket.Product.Extern, bucket.Product.ExternID);

				if (response == HttpStatusCode.OK.ToString())
				{
					if (bucket.Product.Extern.TypeId == ProductType.Configurable)
					{
						// Do something extra for template products
					}
					if (Error == null)
					{
						bucket.Product.AddExtern(bucket.Product.Extern, bucket.Product.Extern.Id, DateTime.UtcNow);
						UpdateStatus(bucket.Product, BCSyncOperationAttribute.ExternUpdate);
						Operation.Callback?.Invoke(new SyncInfo(bucket?.Primary?.SyncID ?? 0, SyncDirection.Export, SyncResult.Processed));
					}
					else
					{
						Log(bucket.Product?.SyncID, SyncDirection.Export, Error);
						UpdateStatus(bucket.Product, BCSyncOperationAttribute.ExternFailed, Error.ToString());
						Operation.Callback?.Invoke(new SyncInfo(bucket?.Primary?.SyncID ?? 0, SyncDirection.Export, SyncResult.Error, Error));
					}
				}
				else
				{
					var retryResponse = productDataProvider.UpdateQty(bucket.Product.Extern, bucket.Product.ExternID);


						if (retryResponse == HttpStatusCode.OK.ToString())
						{
							bucket.Product.AddExtern(bucket.Product.Extern, bucket.Product.Extern.Id, DateTime.UtcNow);
							UpdateStatus(bucket.Product, BCSyncOperationAttribute.ExternUpdate);
							Operation.Callback?.Invoke(new SyncInfo(bucket?.Primary?.SyncID ?? 0, SyncDirection.Export, SyncResult.Processed));
						}
						else
						{
							if (retryResponse == "404") //id not found
							{
								DeleteStatus(BCSyncStatus.PK.Find(this, bucket.Product.ParentID), BCSyncOperationAttribute.NotFound);
								DeleteStatus(bucket.Product, BCSyncOperationAttribute.NotFound);
								Operation.Callback?.Invoke(new SyncInfo(bucket?.Primary?.SyncID ?? 0, SyncDirection.Export, SyncResult.Deleted));
							}
							else
							{
								var exception = new Exception($"Availability retry failed with code {retryResponse}");
								Log(bucket.Product?.SyncID, SyncDirection.Export, exception);

								UpdateStatus(bucket.Product, BCSyncOperationAttribute.ExternFailed, exception.Message );
								Operation.Callback?.Invoke(new SyncInfo(bucket?.Primary?.SyncID ?? 0, SyncDirection.Export, SyncResult.Error, exception));
							}
						}
				}
			}


		}
		#endregion
	}
}
