using Newtonsoft.Json;
using PX.Commerce.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PX.Commerce.Custom.API.REST
{
	[JsonObject(Description = "Price List")]
	public class PriceList : BCAPIEntity
	{
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public int? ID { get; set; }
		public bool ShouldSerializeId()
		{
			return false;
		}

		[JsonProperty("entity_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Entity Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string EntityId { get; set; }


		[JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public decimal? Price { get; set; }

		[JsonProperty("special_from_date", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Special From Date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string SpecialFromDate { get; set; }

		[JsonProperty("special_to_date", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Special To Date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string SpecialToDate { get; set; }

		[JsonProperty("special_price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Sales Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public decimal? SalesPrice { get; set; }

		[JsonProperty("group_price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Group Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public IList<GroupPrice> GroupPrice { get; set; }

		[JsonProperty("tier_price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Tier Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public IList<TierPrice> TierPrice { get; set; }

	}

	[JsonObject(Description = "Group Price")]
	public class GroupPrice : BCAPIEntity
	{
		[JsonProperty("website_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Website Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string WebsiteId { get; set; } = "0";

		[JsonProperty("cust_group", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Customer Group", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string CustomerGroup { get; set; } = "0";

		[JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public decimal? Price { get; set; }

		[JsonProperty("special_from_date", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Special From Date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string SpecialFromDate { get; set; }


		[JsonProperty("special_to_date", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Special To Date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string SpecialToDate { get; set; }

		[JsonProperty("special_price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Sales Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public decimal? SalesPrice { get; set; }

		public GroupPrice ShallowCopy()
		{
			return (GroupPrice)this.MemberwiseClone();
		}
	}

	[JsonObject(Description = "Tier Price")]
	public class TierPrice : BCAPIEntity
	{
		[JsonProperty("website_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Website Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string WebsiteId { get; set; } = "0";


		[JsonProperty("cust_group", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Customer Group", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string CustomerGroup { get; set; } = "0";

		[JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public decimal? Price { get; set; }

		[JsonProperty("price_qty", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Price Qty", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public decimal? PriceQty { get; set; }


		public TierPrice ShallowCopy()
		{
			return (TierPrice)this.MemberwiseClone();
		}
	}

	[JsonObject(Description = "Price list")]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PriceListResponse : IEntityResponse<PriceList>
	{
		[JsonProperty("data")]
		public PriceList Data { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }
	}

	[JsonObject(Description = "List of Pricelist")]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PriceListsResponse : IEntitiesResponse<PriceList>
	{
		public PriceListsResponse()
		{
			Data = new List<PriceList>();
		}

		[JsonProperty("data")]
		public List<PriceList> Data { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }
	}

	[JsonObject(Description = "List of Group Price list records")]
	public class GroupPriceResponse : IEntitiesResponse<GroupPrice>
	{
		public GroupPriceResponse()
		{
			Data = new List<GroupPrice>();
		}
		[JsonProperty("data")]
		public List<GroupPrice> Data { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }
	}

	[JsonObject(Description = "List of Tier Price list records")]
	public class TierPriceResponse : IEntitiesResponse<TierPrice>
	{
		public TierPriceResponse()
		{
			Data = new List<TierPrice>();
		}
		[JsonProperty("data")]
		public List<TierPrice> Data { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }
	}

}
