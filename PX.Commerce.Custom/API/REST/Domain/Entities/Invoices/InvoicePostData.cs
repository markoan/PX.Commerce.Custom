using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Api.ContractBased.Models;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace PX.Commerce.Custom.API.REST
{
    #region Zoey API Invoices Post Entities 

    //InvoicePost  entity
    [JsonObject(Description = "Invoice")]
    [CommerceDescription(CustomCaptions.Invoice)]
    public class InvoicePostData : BCAPIEntity
    {
        [JsonProperty("orderIncrementId", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("OrderNbr", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string OrderNbr { get; set; }

        [JsonProperty("itemsQty", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ItemsList", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public Dictionary<string, string> ItemsList { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Comment", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string Comment { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("IsEmail", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual int? IsEmail { get; set; }

        [JsonProperty("includeComment", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("IsIncludeComment", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual int? IsIncludeComment { get; set; }

    }

    [JsonConverter(typeof(JsonCustomConverter))]
    public class InvoicePostResponse : IEntityResponse<InvoicePostData>
    {
        public InvoicePostResponse(string id)
        {
            Id = id;
        }
        public InvoicePostResponse() { }

        public string Id { get; set; }
       
        public InvoicePostData Data { get; set; }
        
        [JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class InvoicePostsResponse : List<InvoicePostData>
    {

    }


    #endregion Zoey API Invoices Post Entities

    #region Invoice data
    [CommerceDescription("Invoice Detail", FieldFilterStatus.Skipped, FieldMappingStatus.Skipped, null)]
    [ExcludeFromCodeCoverage]
    public class InvoiceDetail : CBAPIEntity
    {
        public InvoiceDetail() { }

        [CommerceDescription("External Ref", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public StringValue ExternalRef { get; set; }

        [CommerceDescription("Inventory ID", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public StringValue InventoryID { get; set; }

        [CommerceDescription("Order Type", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public StringValue SOOrderType { get; set; }

        [CommerceDescription("Order Nbr.", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public StringValue SOOrderNbr { get; set; }

        [CommerceDescription("Order Line Nbr", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public IntValue SOOrderLineNbr { get; set; }

        [CommerceDescription("Open Qty", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public DecimalValue Qty { get; set; }

        public GuidValue NoteID { get; set; }
       
    }

    [CommerceDescription("Invoice", FieldFilterStatus.Skipped, FieldMappingStatus.Skipped, null)]
    [ExcludeFromCodeCoverage]
    public class Invoice : CBAPIEntity
    {
        public Invoice() { }

        [CommerceDescription("Reference Nbr.", FieldFilterStatus.Skipped, FieldMappingStatus.ImportAndExport, null)]
        public StringValue RefNbr { get; set; }

        [IgnoreDataMember]
        public List<Guid?> OrderNoteIds { get; set; }

        [CommerceDescription("Order Type", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport, null)]
        public StringValue SOOrderType { get; set; }

        [CommerceDescription("order Nbr.", FieldFilterStatus.Skipped, FieldMappingStatus.ImportAndExport, null)]
        public StringValue SOOrderNbr { get; set; }

        [CommerceDescription("Description", FieldFilterStatus.Filterable, FieldMappingStatus.Export, null)]
        public StringValue DocDesc { get; set; }

        [CommerceDescription("Customer RefNbr", FieldFilterStatus.Skipped, FieldMappingStatus.Export, null)]
        public StringValue CustomerRefNbr { get; set; }

        [CommerceDescription("Open Qty", FieldFilterStatus.Skipped, FieldMappingStatus.ImportAndExport, null)]
        public DecimalValue Qty { get; set; }

        [CommerceDescription("Details", FieldFilterStatus.Skipped, FieldMappingStatus.Skipped, null)]
        public List<InvoiceDetail> Details { get; set; }

        public GuidValue NoteID { get; set; }
    }
    #endregion
}
