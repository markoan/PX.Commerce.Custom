using Newtonsoft.Json;
using PX.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    #region Zoey API Shipment responses
    //Get a single shipment
    public class ShipmentResponse : IEntityResponse<ShipmentData>
    {
        [JsonProperty("shipment")]
        public ShipmentData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    //Get all shipments from Zoey
    public class ShipmentsResponse : List<ShipmentData> { }
    #endregion

    [JsonConverter(typeof(JsonCustomConverter))]
    public class ShipmentPostResponse : IEntityResponse<ShipmentPost>
    {
        public ShipmentPostResponse(string id)
        {
            Id = id;
        }
        public ShipmentPostResponse() { }
        public string Id { get; set; }
        public ShipmentPost Data { get; set; }
        [JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //[JsonIgnore]
        //ShipmentPost IEntityResponse<ShipmentPost>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [JsonObject(Description = "Post Shipment")]
    public class ShipmentPost : BCAPIEntity
    {
        [JsonProperty("orderIncrementId", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string OrderIncrementId { get; set; }

        [JsonProperty("itemsQty", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> itemsQty { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Comment { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Email { get; set; }

        [JsonProperty("includeComment", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string IncludeComment { get; set; }

    }


    [JsonObject(Description = "Shipment")]
    [CommerceDescription(CustomCaptions.ShipmentData)]
    public class ShipmentData :BCAPIEntity
    {
        [JsonProperty("store_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("StoreId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string StoreId { get; set; }

        [JsonProperty("total_weight", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("TotalWeight", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TotalWeight { get; set; }

        [JsonProperty("total_qty", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("TotalQty", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TotalQty { get; set; }

        [JsonProperty("email_sent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("EmailSent", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EmailSent { get; set; }

        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("OrderId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OrderId { get; set; }

        [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CustomerId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CustomerId { get; set; }

        [JsonProperty("shipping_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ShippingAddressId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingAddressId { get; set; }

        [JsonProperty("billing_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("BillingAddressId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BillingAddressId { get; set; }

        [JsonProperty("shipment_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ShippingStatus", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingStatus { get; set; }

        [JsonProperty("increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("IncrementId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string IncrementId { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CreatedAt", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CreatedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("UpdatedAt", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string UpdatedAt { get; set; }

        [JsonProperty("packages", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Packages", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Packages { get; set; }

        [JsonProperty("shipping_label", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ShippingLabel", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingLabel { get; set; }

        [JsonProperty("old_shipment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Old shipment Id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OldShipmentId { get; set; }

        [JsonProperty("old_shipment_increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Old Shipment Increment Id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OldShipmentIncrementId { get; set; }

        [JsonProperty("split_shipped_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Split shipped status", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string SplitShippedStatus { get; set; }

        [JsonProperty("carriergroup", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Carrier group", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Carriergroup { get; set; }

        [JsonProperty("shipping_description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ShippingDescription", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingDescription { get; set; }

        [JsonProperty("is_archived", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is Archived", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string IsArchived { get; set; }

        [JsonProperty("shipment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ShipmentId", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShipmentId { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Items", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual List<Item> ShippingItems { get; set; }
    }

}
