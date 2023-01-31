using Newtonsoft.Json;
using PX.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    [JsonObject(Description = "items")]
    [CommerceDescription("Items")]
    public class Item
    {
        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Parent Id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ParentId { get; set; }

        [JsonProperty("base_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base price", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BasePrice { get; set; }

        [JsonProperty("tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TaxAmt { get; set; }

        [JsonProperty("base_row_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base row total", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseRowTotal { get; set; }

        [JsonProperty("discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount Amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string DiscountAmt { get; set; }

        [JsonProperty("row_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Row total", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string RowTotal { get; set; }

        [JsonProperty("base_discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseDiscountAmt { get; set; }

        [JsonProperty("price_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Price incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PriceInclTax { get; set; }

        [JsonProperty("base_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseTaxAmt { get; set; }

        [JsonProperty("base_price_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base price incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BasePriceInclTax { get; set; }

        [JsonProperty("qty", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Qty", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Qty { get; set; }

        [JsonProperty("base_cost", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base cost", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseCost { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Price", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Price { get; set; }

        [JsonProperty("base_row_total_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base row total incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseRowTotalInclTax { get; set; }

        [JsonProperty("row_total_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Row total incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string RowTotalInclTax { get; set; }

        [JsonProperty("product_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Product id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ProductId { get; set; }

        [JsonProperty("order_item_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Order item id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OrderItemId { get; set; }

        [JsonProperty("additional_data", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Additional data", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdditionalData { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Description", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Description { get; set; }

        [JsonProperty("sku", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Sku", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Sku { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Name", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Name { get; set; }

        [JsonProperty("item_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Item id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ItemId { get; set; }

    }

}
