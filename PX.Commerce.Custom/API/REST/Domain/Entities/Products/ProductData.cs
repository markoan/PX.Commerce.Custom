using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PX.Commerce.Core;

namespace PX.Commerce.Custom.API.REST
{
	public class ProductResponse : IEntityResponse<ProductData>
	{
		public ProductData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

	public class ProductsResponse : List<ProductData>
	{

	}

    [JsonConverter(typeof(JsonCustomConverter))]
    public class UpdateResponse : IEntityResponse<ProductData>
    {
		public UpdateResponse (string code)
        {

        }
		public string code { get; set; }
        public ProductData Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UpdateResponse () { }
	}

	[JsonObject(Description = "Product list (total  BigCommerce API v3 response)")]
	public class ProductQtyList : IEntitiesResponse<ProductQtyData>
	{
		private List<ProductQtyData> _data;

		[JsonProperty("data")]
		public List<ProductQtyData> Data
		{
			get
			{
				return _data ?? (_data = new List<ProductQtyData>());
			}
			set
			{
				_data = value;
			}
		}

		[JsonProperty("meta")]
		public Meta Meta { get; set; }

    }

	[JsonConverter(typeof(JsonCustomConverter))]
	public class UpdateQtyResponse : IEntityResponse<ProductQtyData>
	{
		public UpdateQtyResponse(string code)
		{

		}
		public string code { get; set; }
		public ProductQtyData Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public UpdateQtyResponse() { }
	}

	[JsonConverter(typeof(JsonCustomConverter))]
    public class IdPostResponse : IEntityResponse<ProductData>
    {
		public IdPostResponse(string id)
        {
			Id = id;
        }
		public IdPostResponse() { }
        public string Id { get; set; }
		[JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
		ProductData IEntityResponse<ProductData>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
	

	[JsonObject(Description = "Product")]
	[CommerceDescription(CustomCaptions.Product)]
	public class ProductData : BCAPIEntity
	{
		[JsonProperty("entity_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Id", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Id { get; set; }

		[JsonProperty("attribute_set_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Attribute Set Id", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string AttributeSetId { get; set; }

		[JsonProperty("sku", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Sku", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Sku { get; set; }

		[JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("DateCreatedAt", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public DateTime? DateCreatedAt { get; set; }

		[JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("DateUpdatedAt", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public DateTime? DateUpdatedAt { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Name", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Name { get; set; }

		[JsonProperty("type_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("TypeId", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual ProductType TypeId { get; set; }

		[JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Description", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Description { get; set; }

		[JsonProperty("short_description", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("ShortDescription", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string ShortDescription { get; set; }

		[JsonProperty("meta_description", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("MetaDescription", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string MetaDescription { get; set; }

		[JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Price", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Price { get; set; }

		[JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Weight", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Weight { get; set; }

		[JsonProperty("tax_class_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("TaxClassId", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string TaxClassId { get; set; }

		[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Active", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual Status Active { get; set; }


		[JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Visibility", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual Visibility Visibility { get; set; }

		[JsonProperty("category_ids", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Categories", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual IEnumerable<string> Categories { get; set; }

		//[JsonProperty("stock_data", NullValueHandling = NullValueHandling.Ignore)]
		//[CommerceDescription("StockData", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		//public virtual StockData StockData { get; set; }

		/// <summary>
		/// Catch-all property for all other values that should be attributes assigned to the item
		/// </summary>
		[JsonExtensionData]
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();
	}

	[CommerceDescription(CustomCaptions.ProductQuantityData)]
	public class ProductQtyData : BCAPIEntity
	{
		[JsonProperty("entity_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Id", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Id { get; set; }

		[JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("DateUpdatedAt", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public DateTime? DateUpdatedAt { get; set; }

		[JsonProperty("type_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("TypeId", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual ProductType TypeId { get; set; }

		[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Active", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual Status Active { get; set; }

		[JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Visibility", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual Visibility Visibility { get; set; }


		[JsonProperty("stock_data", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("StockData", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual StockData StockData { get; set; }
	}

	public class StockData
	{
		

		[JsonProperty("manage_stock", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Manage Stock", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual Switch ManageStock { get; set; }

		[JsonProperty("is_in_stock", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Is in stock", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual Switch IsInStock { get; set; }

		[JsonProperty("backorders", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Backorders", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual InventoryPolicy Backorders { get; set; }

		[JsonProperty("qty", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Quantity", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string Qty { get; set; }


		[JsonProperty("update_stock_status", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Update Stock Status", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual int UpdateStockStatus { get; set; }

		//[JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
		//[CommerceDescription("Updated At", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		//public virtual string UpdatedAt { get; set; }
	}

	public enum ProductTypes
	{
		physical,
		digital
	}
}
