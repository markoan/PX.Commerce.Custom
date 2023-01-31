using Newtonsoft.Json;
using PX.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

    #region Zoey Order Entity
    [JsonObject(Description = "Order")]
    [CommerceDescription(CustomCaptions.OrderData)]
    public class OrderData : BCAPIEntity
    {
        [JsonProperty("splitorder_key", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("SplitOrder key", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SplitOrderKey { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("State", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string State { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Status", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Status { get; set; }

        [JsonProperty("coupon_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Coupon code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CouponCode { get; set; }

        [JsonProperty("protect_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Protect code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ProtectCode { get; set; }

        [JsonProperty("shipping_description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Description", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingDescription { get; set; }

        [JsonProperty("is_virtual", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is Virtual", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsVirtual { get; set; }

        [JsonProperty("store_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("StoreId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string StoreId { get; set; }

        [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CustomeId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerId { get; set; }

        [JsonProperty("base_discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseDiscountAmt { get; set; }

        [JsonProperty("base_discount_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseDiscountCanceled { get; set; }

        [JsonProperty("base_discount_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseDiscountInvoice { get; set; }

        [JsonProperty("base_discount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseDiscountRefunded { get; set; }

        [JsonProperty("base_grand_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base grand total", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseGrandTotal { get; set; }

        [JsonProperty("base_shipping_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingAmount { get; set; }

        [JsonProperty("base_shipping_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingCanceled { get; set; }

        [JsonProperty("base_shipping_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingInvoiced { get; set; }

        [JsonProperty("base_shipping_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingRefunded { get; set; }

        [JsonProperty("base_shipping_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingTaxAmount { get; set; }

        [JsonProperty("base_shipping_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingTaxRefunded { get; set; }

        [JsonProperty("base_subtotal", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base subtotal", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseSubtotal { get; set; }

        [JsonProperty("base_subtotal_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base subtotal canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseSubtotalCanceled { get; set; }

        [JsonProperty("base_subtotal_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base subtotal invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseSubtotalInvoiced { get; set; }

        [JsonProperty("base_subtotal_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base subtotal refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseSubtotalRefunded { get; set; }

        [JsonProperty("base_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxAmt { get; set; }

        [JsonProperty("base_tax_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxCanceled { get; set; }

        [JsonProperty("base_tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxInvoiced { get; set; }

        [JsonProperty("base_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxRefunded { get; set; }

        [JsonProperty("base_to_global_rate", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base to global rate", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseToGlobalRate { get; set; }

        [JsonProperty("base_to_order_rate", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base to order rate", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseToOrderRate { get; set; }

        [JsonProperty("base_total_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalCanceled { get; set; }

        [JsonProperty("base_total_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalInvoiced { get; set; }

        [JsonProperty("base_total_invoiced_cost", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total invoiced cost", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalInvoicedCost { get; set; }

        [JsonProperty("base_total_offline_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total offline refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalOfflineRefunded { get; set; }

        [JsonProperty("base_total_online_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total online refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalOnlineRefunded { get; set; }

        [JsonProperty("base_total_paid", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total paid", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalPaid { get; set; }

        [JsonProperty("base_total_qty_ordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total qty ordered", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalQtyOrdered { get; set; }

        [JsonProperty("base_total_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base Total refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalRefunded { get; set; }

        [JsonProperty("discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountAmt { get; set; }

        [JsonProperty("discount_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountCanceled { get; set; }

        [JsonProperty("discount_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount Invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountInvoiced { get; set; }

        [JsonProperty("discount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountRefunded { get; set; }

        [JsonProperty("grand_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Total { get; set; }

        [JsonProperty("shipping_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingAmt { get; set; }

        [JsonProperty("shipping_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingCanceled { get; set; }

        [JsonProperty("shipping_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingInvoiced { get; set; }

        [JsonProperty("shipping_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingRefunded { get; set; }

        [JsonProperty("shipping_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingTaxAmt { get; set; }

        [JsonProperty("shipping_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingTaxRefunded { get; set; }

        [JsonProperty("subtotal", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Subtotal", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Subtotal { get; set; }

        [JsonProperty("subtotal_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Subtotal canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SubtotalCanceled { get; set; }

        [JsonProperty("subtotal_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Subtotal invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SubtotalInvoiced { get; set; }

        [JsonProperty("subtotal_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("SubtotalRefunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SubtotalRefunded { get; set; }

        [JsonProperty("tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxAmt { get; set; }

        [JsonProperty("tax_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxCanceled { get; set; }

        [JsonProperty("tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxInvoiced { get; set; }

        [JsonProperty("tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxRefunded { get; set; }

        [JsonProperty("total_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalCanceled { get; set; }

        [JsonProperty("total_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalInvoiced { get; set; }

        [JsonProperty("total_offline_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total offline refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalOfflineRefunded { get; set; }

        [JsonProperty("total_online_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total Online Refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalOnlineRefunded { get; set; }

        [JsonProperty("total_paid", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total Paid", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalPaid { get; set; }

        [JsonProperty("total_qty_ordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total Qty Ordered", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalQtyOrdered { get; set; }

        [JsonProperty("total_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total Refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalRefunded { get; set; }

        [JsonProperty("can_ship_partially", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Can Ship Partially", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CanShipPartially { get; set; }

        [JsonProperty("can_ship_partially_item", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Can Ship Partially Item", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CanShipPartiallyItem { get; set; }

        [JsonProperty("customer_is_guest", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Customer Is Guest", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerIsGuest { get; set; }

        [JsonProperty("customer_note_notify", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Customer Note Notify", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerNoteNotify { get; set; }

        [JsonProperty("billing_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Billing Address Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BillingAddressId { get; set; }

        [JsonProperty("customer_group_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Customer Group Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerGroupId { get; set; }

        [JsonProperty("edit_increment", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Edit Increment", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string EditIncrement { get; set; }

        [JsonProperty("email_sent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Email Sent", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string EmailSent { get; set; }

        [JsonProperty("forced_shipment_with_invoice", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Forced Shipment With Invoice", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ForcedShipmentWithInvoice { get; set; }

        [JsonProperty("payment_auth_expiration", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment Auth Expiration", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PaymentAuthExpiration { get; set; }

        [JsonProperty("quote_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Quote Address Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QuoteAddressId { get; set; }

        [JsonProperty("quote_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Quote Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QuoteId { get; set; }

        [JsonProperty("shipping_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Address Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingAddressId { get; set; }

        [JsonProperty("adjustment_negative", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adjustment Negative", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AdjustmentNegative { get; set; }

        [JsonProperty("adjustment_positive", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adjustment Positive", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AdjustmentPositive { get; set; }

        [JsonProperty("base_total_due", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total due", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTotalDue { get; set; }

        [JsonProperty("payment_authorization_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment Auth Amt", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PaymentAuthAmt { get; set; }

        [JsonProperty("shipping_discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Discount Amt", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingDiscountAmt { get; set; }

        [JsonProperty("subtotal_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Subtotal Incl Tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SubtotalInclTax { get; set; }

        [JsonProperty("total_due", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total Due", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalDue { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("weight", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Weight { get; set; }

        [JsonProperty("customer_dob", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Customer Dob", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerDob { get; set; }

        [JsonProperty("increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("increment_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IncrementId { get; set; }

        [JsonProperty("applied_rule_ids", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("applied_rule_ids", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AppliedRuleIds { get; set; }

        [JsonProperty("base_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_currency_code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseCurrencyCode { get; set; }

        [JsonProperty("customer_email", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_email", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerEmail { get; set; }

        [JsonProperty("customer_firstname", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_firstname", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerFirstname { get; set; }

        [JsonProperty("customer_lastname", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_lastname", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerLastname { get; set; }

        [JsonProperty("customer_middlename", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_middlename", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerMiddlename { get; set; }

        [JsonProperty("customer_prefix", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_prefix", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerPrefix { get; set; }

        [JsonProperty("customer_suffix", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_suffix", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerSuffix { get; set; }

        [JsonProperty("customer_taxvat", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_taxvat", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerTaxvat { get; set; }

        [JsonProperty("discount_description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("discount_description", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountDescription { get; set; }

        [JsonProperty("ext_customer_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ext_customer_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ExtCustomerId { get; set; }

        [JsonProperty("ext_order_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ext_order_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ExtOrderId { get; set; }

        [JsonProperty("global_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("global_currency_code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string GlobalCurrencyCode { get; set; }

        [JsonProperty("hold_before_state", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("hold_before_state", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HoldBeforeState { get; set; }

        [JsonProperty("hold_before_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("hold_before_status", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HoldBeforeStatus { get; set; }

        [JsonProperty("order_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("order_currency_code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OrderCurrencyCode { get; set; }

        [JsonProperty("original_increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("original_increment_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OriginalIncrementId { get; set; }

        [JsonProperty("relation_child_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("relation_child_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RelationChildId { get; set; }

        [JsonProperty("relation_child_real_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("relation_child_real_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RelationChildRealId { get; set; }

        [JsonProperty("relation_parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("relation_parent_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RealParentId { get; set; }

        [JsonProperty("relation_parent_real_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("relation_parent_real_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RelationParentRealId { get; set; }

        [JsonProperty("remote_ip", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("remote_ip", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RemoteIp { get; set; }

        [JsonProperty("shipping_method", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("shipping_method", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingMethod { get; set; }

        [JsonProperty("store_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("store_currency_code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string StoreCurrencyCode { get; set; }

        [JsonProperty("store_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("store_name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string StoreName { get; set; }

        [JsonProperty("customer_note", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_note", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerNote { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("created_at", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CreatedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("updated_at", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string UpdateAt { get; set; }

        [JsonProperty("total_item_count", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("total_item_count", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalItemCount { get; set; }

        [JsonProperty("customer_gender", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_gender", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerGender { get; set; }

        [JsonProperty("hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("hidden_tax_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxAmt { get; set; }

        [JsonProperty("base_hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_hidden_tax_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseHiddenTaxAmt { get; set; }

        [JsonProperty("shipping_hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("shipping_hidden_tax_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingHiddenTaxAmt { get; set; }

        [JsonProperty("base_shipping_hidden_tax_amnt", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_shipping_hidden_tax_amnt", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingHiddenTaxAmt { get; set; }

        [JsonProperty("hidden_tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("hidden_tax_invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxInvoiced { get; set; }

        [JsonProperty("base_hidden_tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_hidden_tax_invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseHiddenTaxInvoiced { get; set; }

        [JsonProperty("hidden_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("hidden_tax_refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxRefunded { get; set; }

        [JsonProperty("base_hidden_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_hidden_tax_refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseHiddenTaxRefunded { get; set; }

        [JsonProperty("shipping_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("shipping_incl_tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ShippingInclTax { get; set; }

        [JsonProperty("base_shipping_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_shipping_incl_tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingInclTax { get; set; }

        [JsonProperty("coupon_rule_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("coupon_rule_name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CouponRuleName { get; set; }

        [JsonProperty("paypal_ipn_customer_notified", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("paypal_ipn_customer_notified", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PaypalIpnCustomerNotified { get; set; }

        [JsonProperty("gift_message_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("gift_message_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string GiftMessageId { get; set; }

        [JsonProperty("ebizmarts_abandonedcart_flag", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ebizmarts_abandonedcart_flag", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string EbizmartsAbandonedCartFlag { get; set; }

        [JsonProperty("ebizmarts_magemonkey_campaign_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ebizmarts_magemonkey_campaign_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string EbizmartaMagemonketCampaignId { get; set; }

        [JsonProperty("auctaneapi_discounts", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("auctaneapi_discounts", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AuctaneapiDiscounts { get; set; }

        [JsonProperty("base_surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_surcharge_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseSurchargeAmt { get; set; }

        [JsonProperty("surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("surcharge_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SurchargeAmt { get; set; }

        [JsonProperty("old_order_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("old_order_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OldOrderId { get; set; }

        [JsonProperty("old_order_increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("old_order_increment_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OldOrderIncrementId { get; set; }

        [JsonProperty("is_giftorder", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is Gift Order", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsGiftOrder { get; set; }

        [JsonProperty("tracking", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("tracking", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Tracking { get; set; }

        [JsonProperty("base_giftcard_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_giftcard_discount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseGiftCardDisc { get; set; }

        [JsonProperty("giftcard_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("giftcard_discount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string GiftcardDisc { get; set; }

        [JsonProperty("giftcard_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("giftcard_code", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string GiftcardCode { get; set; }

        [JsonProperty("carrier_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("carrier_type", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CarrierType { get; set; }

        [JsonProperty("dispatch_date", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("dispatch_date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DispatchDate { get; set; }

        [JsonProperty("delivery_date", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("delivery_date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DeliveryDate { get; set; }

        [JsonProperty("pickup_location", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("pickup_location", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PickupLocation { get; set; }

        [JsonProperty("time_slot", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("time_slot", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TimeSlot { get; set; }

        [JsonProperty("carriergroup", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("carriergroup", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CarrierGroup { get; set; }

        [JsonProperty("carrier_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("carrier_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CarrierId { get; set; }

        [JsonProperty("confirmation_number", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("confirmation_number", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ConfirmationNumber { get; set; }

        [JsonProperty("destination_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("destination_type", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DestinationType { get; set; }

        [JsonProperty("liftgate_required", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("liftgate_required", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string LiftgateRequired { get; set; }

        [JsonProperty("notify_required", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("notify_required", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string NotifyRequired { get; set; }

        [JsonProperty("inside_delivery", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("inside_delivery", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string InsideDelivery { get; set; }

        [JsonProperty("freight_quote_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("freight_quote_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string FreightQuoteId { get; set; }

        [JsonProperty("payment_fee_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("payment_fee_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PaymentFeeAmt { get; set; }

        [JsonProperty("base_payment_fee_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_payment_fee_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BasePaymentFeeAmt { get; set; }

        [JsonProperty("payment_installment_fee_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("payment_installment_fee_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PaymentInstallmentFeeAmt { get; set; }

        [JsonProperty("zoey_order_comment", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_order_comment", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyOrderComment { get; set; }

        [JsonProperty("zoey_admin_order_is_admin", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_admin_order_is_admin", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyAdminOrderIsAdmin { get; set; }

        [JsonProperty("customer_carrier", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_carrier", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerCarrier { get; set; }

        [JsonProperty("customer_carrier_account", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("customer_carrier_account", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerCarrierAccts { get; set; }

        [JsonProperty("limited_delivery", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("limited_delivery", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string LimitedDelivery { get; set; }

        [JsonProperty("pickup_location_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("pickup_location_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PickupLocationId { get; set; }

        [JsonProperty("is_recovered_abandoned_cart", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("is_recovered_abandoned_cart", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsRecoveredAbandonedCart { get; set; }

        [JsonProperty("zoey_custom_options_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_custom_options_price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyCustomOptionsPrice { get; set; }

        [JsonProperty("base_zoey_custom_options_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base Zoey Custom Options Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseZoeyCustomOptionsPrice { get; set; }

        [JsonProperty("is_archived", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("is_archived", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsArchived { get; set; }

        [JsonProperty("zoey_bcc_emails", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_bcc_emails", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyBccEmails { get; set; }

        [JsonProperty("payment_surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("payment_surcharge_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PaymentSurchargeAmt { get; set; }

        [JsonProperty("base_payment_surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_payment_surcharge_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BasePaymentSurchargeAmt { get; set; }

        [JsonProperty("customer_signature", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Customer signature", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerSignature { get; set; }

        [JsonProperty("total_qty_backordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("total_qty_backordered", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TotalQtyBackordered { get; set; }

        [JsonProperty("user_agent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("user_agent", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string UserAgent { get; set; }

        [JsonProperty("geolocation", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("geolocation", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Geolocation { get; set; }

        [JsonProperty("apply_store_credit", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("apply_store_credit", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ApplyStoreCredit { get; set; }

        [JsonProperty("store_credit_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("store_credit_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string StoreCreditAmt { get; set; }

        [JsonProperty("base_store_credit_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("base_store_credit_amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseStoreCreditAmt { get; set; }

        [JsonProperty("tj_salestax_sync_date", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("tj_salestax_sync_date", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TjSalesTaxSyncDate { get; set; }

        [JsonProperty("forced_do_shipment_with_invoice", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("forced_do_shipment_with_invoice", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ForcedDoShipmentWithInvoice { get; set; }

        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Company Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyId { get; set; }

        [JsonProperty("company_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Company name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyName { get; set; }

        [JsonProperty("company_location_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Company location id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyLocationId { get; set; }

        [JsonProperty("company_location_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Company location name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyLocationName { get; set; }

        [JsonProperty("company_external_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Company external id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyExternalId { get; set; }

        [JsonProperty("_sales_reps_loaded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("_sales_reps_loaded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SalesRepsLoades { get; set; }

        [JsonProperty("sales_rep_user_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("sales_rep_user_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SalesRepUserId { get; set; }

        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("order_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OrderId { get; set; }

        [JsonProperty("admin_order_info", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Admin order info", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual AdminOrderInfo AdminOrderInfo { get; set; }

        [JsonProperty("shipping_address", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Address", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual CAddressDataSingle ShippingAddr { get; set; }

        [JsonProperty("billing_address", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("BillingAddress", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual BillingAddress BillingAddress { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Items", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual List<OrderItem> Items { get; set; }

        [JsonProperty("payment", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual PaymentMethodInfo PaymentMethod { get; set; }

        [JsonProperty("status_history", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Status History", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual List<StatusHistory> StatusHistory { get; set; }

        [JsonProperty("order_attributes", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Order attributes", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OrderAttributes { get; set; }

        [JsonProperty("custom_attributes", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Custom attributes", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual List<CustomAtrribute> CustomAttributes { get; set; }

        [JsonProperty("order_invoices", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Invoices", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual List<InvoiceData> Invoices { get; set; }

        [JsonProperty("order_shipments", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("OrderShipments", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual List<ShipmentData> OrderShipments { get; set; }
    }

    #endregion Zoey Order Entity

    public class OrderResponse : IEntityResponse<OrderData>
    {
        [JsonProperty("order")]
        public OrderData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class OrdersResponse : List<OrderData>
    {

    }


    #region Helper classes 

    public class CustomAtrribute
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Name { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("value", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Value { get; set; }
    }
    public class StatusHistory
    {
        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("parent id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ParentId { get; set; }

        [JsonProperty("is_customer_notified", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("is customer notified", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsCustomerNotified { get; set; }

        [JsonProperty("is_visible_on_front", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("is visible on front", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsVisibleOnFront { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("comment", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Comment { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("status", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Status { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("created_at", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CreatedAt { get; set; }

        [JsonProperty("entity_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("entity_name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string EntityName { get; set; }

        [JsonProperty("zoey_commenter_type_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_commenter_type_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyCommenterTypeId { get; set; }

        [JsonProperty("zoey_commenter_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_commenter_id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyCommenterId { get; set; }

        [JsonProperty("zoey_commenter_email", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("zoey_commenter_email", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyCommenterEmail { get; set; }

        [JsonProperty("store_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Store Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string StoreId { get; set; }

        [JsonProperty("comment_updates", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("comment_updates", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual List<string> CommentUpdated { get; set; }
    }
    public class AdminOrderInfo
    {
        [JsonProperty("Created by Team Member", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Created by team member", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CreatedByTeamMember { get; set; }

        [JsonProperty("Created", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Created", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Created { get; set; }

        [JsonProperty("Created from IP", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Created from IP", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CreatedFromIp { get; set; }
    }
    public class OrderItem
    {

        [JsonProperty("item_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ItemId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ItemId { get; set; }

        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("OrderId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OrderId { get; set; }

        [JsonProperty("parent_item_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ParentItemId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ParentItemId { get; set; }

        [JsonProperty("quote_item_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("QuoteItemId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QuoteItemId { get; set; }

        [JsonProperty("store_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("StoreId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string StoreId { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Created At", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CreatedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Updated At", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string UpdatedAt { get; set; }

        [JsonProperty("product_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Product Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ProductId { get; set; }

        [JsonProperty("has_children", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Has Children", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual bool HasChildren { get; set; }

        [JsonProperty("product_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ProductType", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual ProductType ProductType { get; set; }

        [JsonProperty("product_options", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Product Options", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ProductOptions { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Weight", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Weight { get; set; }

        [JsonProperty("is_virtual", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is Virtual", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Isvirtual { get; set; }

        [JsonProperty("sku", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Sku", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Sku { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Name { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Description", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Description { get; set; }

        [JsonProperty("free_shipping", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Free shipping", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string FreeShipping { get; set; }

        [JsonProperty("is_qty_decimal", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("IsQtyDecimal", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsQtyDecimal { get; set; }

        [JsonProperty("no_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("No discount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string NoDiscount { get; set; }

        [JsonProperty("qty_backordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Qty backordered", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QtyBackOrdered { get; set; }

        [JsonProperty("qty_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("QtyCanceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QtyCanceled { get; set; }

        [JsonProperty("qty_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Qty Invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QtyInvoiced { get; set; }

        [JsonProperty("qty_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Qty Refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QtyRefunded { get; set; }

        [JsonProperty("qty_ordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Qty", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Quantity { get; set; }

        [JsonProperty("qty_shipped", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("QtyShipped", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string QtyShipped { get; set; }

        [JsonProperty("base_cost", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base Cost", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseCost { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Price { get; set; }

        [JsonProperty("base_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BasePrice { get; set; }

        [JsonProperty("original_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Original Price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string OriginalPrice { get; set; }

        [JsonProperty("tax_percent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax Percent", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxPercent { get; set; }

        [JsonProperty("tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("TaxAmt", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxAmount { get; set; }

        [JsonProperty("base_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxAmt { get; set; }

        [JsonProperty("tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxInvoiced { get; set; }

        [JsonProperty("base_tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxInvoiced { get; set; }

        [JsonProperty("discount_percent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount Percent", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountPercent { get; set; }

        [JsonProperty("discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount Amt", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountAmt { get; set; }

        [JsonProperty("discount_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount Invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountInvoiced { get; set; }

        [JsonProperty("amount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Amount Refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AmountRefunded { get; set; }

        [JsonProperty("row_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Row total", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RowTotal { get; set; }

        [JsonProperty("row_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Row Invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RowInvoiced { get; set; }

        [JsonProperty("row_weight", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Row weight", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RowWeight { get; set; }

        [JsonProperty("base_tax_before_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax before discount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxBeforeDisc { get; set; }

        [JsonProperty("tax_before_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax before discount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxBeforeDisc { get; set; }

        [JsonProperty("ext_order_item_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Ext Order Item Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ExtOrderItemId { get; set; }

        [JsonProperty("locked_do_invoice", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Locked do invoice", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string LocketDoInvoice { get; set; }

        [JsonProperty("locked_do_ship", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("LockedDoShip", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string LockedDoShip { get; set; }

        [JsonProperty("price_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Price incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PriceInclTax { get; set; }

        [JsonProperty("base_price_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base price incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BasePriceInclTax { get; set; }

        [JsonProperty("row_total_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Row total incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RowTotalInclTax { get; set; }

        [JsonProperty("base_row_total_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base row total incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseRowTotalInclTax { get; set; }

        [JsonProperty("hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Hidden tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxAmt { get; set; }

        [JsonProperty("base_hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base hidden tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseHiddenTaxAmt { get; set; }

        [JsonProperty("hidden_tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Hidden tax invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxInvoiced { get; set; }

        [JsonProperty("base_hidden_tax_invoiced", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base hidden tax invoiced", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseHiddenTaxInvoiced { get; set; }

        [JsonProperty("hidden_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Hidden tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxRefunded { get; set; }

        [JsonProperty("base_hidden_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base hidden tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseHiddenTaxRefunded { get; set; }

        [JsonProperty("is_nominal", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is nominal", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsNominal { get; set; }

        [JsonProperty("tax_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxCanceled { get; set; }

        [JsonProperty("hidden_tax_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Hidden tax canceled", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string HiddenTaxCanceled { get; set; }

        [JsonProperty("tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string TaxRefunded { get; set; }

        [JsonProperty("base_tax_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseTaxRefunded { get; set; }

        [JsonProperty("discount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string DiscountRefunded { get; set; }

        [JsonProperty("base_discount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount refunded", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseDiscountRefunded { get; set; }

        [JsonProperty("gift_message_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Gift message id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string GiftMessageId { get; set; }

        [JsonProperty("gift_message_available", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Gift Message Available", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string GiftMessageAvailable { get; set; }

        [JsonProperty("base_surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base surcharge amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseSurchargeAmt { get; set; }

        [JsonProperty("surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Surcharge amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string SurchardeAmt { get; set; }

        [JsonProperty("pickup_chosen", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Pickup chosen", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PickupChosen { get; set; }

        [JsonProperty("carriergroup", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Carrier group", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CarrierGroup { get; set; }

        [JsonProperty("carriergroup_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Carriergroup id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CarriergroupId { get; set; }

        [JsonProperty("zoey_custom_options_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Zoey custom options price", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyCustomOptionsPrice { get; set; }

        [JsonProperty("is_invoiced_as_percent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is Invoice as percent", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string IsInvoiceAsPercent { get; set; }

        [JsonProperty("zoey_added_by_discount_rule_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Zoey Added by discount rule id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ZoeyAddedByDiscRuleId { get; set; }

        [JsonProperty("product_custom_attributes", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Product Custom attributes", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual object ProductCustomAttributes { get; set; }

    }

    public class BillingAddress
    {
        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ParentId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string ParentId { get; set; }

        [JsonProperty("customer_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CustomerAddressId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerAddressId { get; set; }

        [JsonProperty("region_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("RegionId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RegionId { get; set; }

        [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CustomerId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerId { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Region", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Region { get; set; }

        [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("PostCode", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PostCode { get; set; }

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("LastName", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string LastName { get; set; }

        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Street", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Street { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("City", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string City { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Email", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Email { get; set; }

        [JsonProperty("telephone", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Telephone", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Telephone { get; set; }

        [JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CountryId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CountryId { get; set; }

        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("FirstName", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string FirstName { get; set; }

        [JsonProperty("address_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("AddressType", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AddressType { get; set; }

        [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CompanyName", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyName { get; set; }

        [JsonProperty("address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("AddressId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AddressId { get; set; }
    }
    public class ShippingAddress
    {
        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("ParentId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string  ParentId { get; set; }

        [JsonProperty("customer_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CustomerAddressId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerAddressId { get; set; }

        [JsonProperty("region_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("RegionId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string RegionId { get; set; }

        [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CustomerId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CustomerId { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Region", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Region { get; set; }

        [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("PostCode", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string PostCode { get; set; }

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("LastName", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string LastName { get; set; }

        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Street", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Street { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("City", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string City { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Email", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Email { get; set; }

        [JsonProperty("telephone", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Telephone", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string Telephone { get; set; }

        [JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CountryId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CountryId { get; set; }

        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("FirstName", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string FirstName { get; set; }

        [JsonProperty("address_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("AddressType", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AddressType { get; set; }

        [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CompanyName", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string CompanyName { get; set; }

        [JsonProperty("address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("AddressId", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string AddressId { get; set; }

}

    public class PaymentMethodInfo
    {
        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Parent Id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ParentId { get; set; }

        [JsonProperty("base_shipping_captured", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping captured", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseShippingCaptured { get; set; }

        [JsonProperty("shipping_captured", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Captured", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingCaptured { get; set; }

        [JsonProperty("amount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Amount refunded", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AmountRefunded { get; set; }

        [JsonProperty("base_amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount paid", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtPaid { get; set; }

        [JsonProperty("amount_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Amount canceled", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AmountCanceled { get; set; }

        [JsonProperty("base_amount_authorized", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount authorized", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtAuthorized { get; set; }

        [JsonProperty("base_amount_paid_online", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount paid online", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtPaidOnline { get; set; }

        [JsonProperty("base_amount_refunded_online", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount refunded online", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtRefundedOnline { get; set; }

        [JsonProperty("base_shipping_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseShippingAmt { get; set; }

        [JsonProperty("shipping_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingAmt { get; set; }

        [JsonProperty("amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Amount paid", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AmountPaid { get; set; }

        [JsonProperty("amount_authorized", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Amount authorized", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AmountAuthorized { get; set; }

        [JsonProperty("base_amount_ordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount ordered", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtOrdered { get; set; }

        [JsonProperty("base_shipping_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping refunded", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseShippingRefunded { get; set; }

        [JsonProperty("shipping_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shippinf refunded", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingRefunded { get; set; }

        [JsonProperty("base_amount_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount refunded", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtRefunded { get; set; }

        [JsonProperty("amount_ordered", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Amount ordered", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AmountOrdered { get; set; }

        [JsonProperty("base_amount_canceled", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base amount canceled", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseAmtCanceled { get; set; }

        [JsonProperty("quote_payment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Quote payment id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string QuotePaymentId { get; set; }

        [JsonProperty("additional_data", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Additional data", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdditionalData { get; set; }

        [JsonProperty("cc_exp_month", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC exp month", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCExpMonth { get; set; }

        [JsonProperty("cc_ss_start_year", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC ss start year", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCSSStartYear { get; set; }

        [JsonProperty("echeck_bank_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Echeck bank name", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EcheckBankName { get; set; }

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Method", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Method { get; set; }

        [JsonProperty("cc_debug_request_body", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC debug request body", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCDebugRequestBody { get; set; }

        [JsonProperty("cc_secure_verify", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC secure verify", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCSecureVerify { get; set; }

        [JsonProperty("protection_eligibility", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Protection Eligibility", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ProtectionEligibility { get; set; }

        [JsonProperty("cc_approval", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC approval", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCApproval { get; set; }

        [JsonProperty("cc_last4", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC last 4", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCLast4 { get; set; }

        [JsonProperty("cc_status_description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC status description", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCStatusDescription { get; set; }

        [JsonProperty("echeck_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Echeck type", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EcheckType { get; set; }

        [JsonProperty("cc_debug_response_serialized", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC debug response serialized", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCDebugResponseSerialized { get; set; }

        [JsonProperty("cc_ss_start_month", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC ss start month", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCSSStartMonth { get; set; }

        [JsonProperty("echeck_account_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Echeck account type", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EcheckAccountType { get; set; }

        [JsonProperty("last_trans_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Last trans id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string LastTransId { get; set; }

        [JsonProperty("cc_cid_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC cid status", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCcidStatus { get; set; }

        [JsonProperty("cc_owner", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC owner", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCOwner { get; set; }

        [JsonProperty("cc_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CCType", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCType { get; set; }

        [JsonProperty("po_number", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("PO number", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PONumber { get; set; }

        [JsonProperty("cc_exp_year", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC Exp year", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCExpYear { get; set; }

        [JsonProperty("cc_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC status", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCStatus { get; set; }

        [JsonProperty("echeck_routing_number", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Echeck routing number", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EcheckRoutingNumber { get; set; }

        [JsonProperty("account_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Account status", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AccountStatus { get; set; }

        [JsonProperty("anet_trans_method", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Anet trans method", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AnetTransMethod { get; set; }

        [JsonProperty("cc_debug_response_body", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC debug response body", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCDebugResponseBody { get; set; }

        [JsonProperty("cc_ss_issue", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC ss issue", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCSSIssue { get; set; }

        [JsonProperty("echeck_account_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Echeck account name", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EcheckAccountName { get; set; }

        [JsonProperty("cc_avs_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC avs status", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCAvsStatus { get; set; }

        [JsonProperty("cc_number_enc", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC number enc", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCNumberEnc { get; set; }

        [JsonProperty("cc_trans_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("CC trans id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CCTransId { get; set; }

        [JsonProperty("paybox_request_number", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Paybox request number", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PayboxRequestNumber { get; set; }

        [JsonProperty("address_status", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Address status", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AddressStatus { get; set; }

        [JsonProperty("additional_information", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Additional information", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual object AdditionalInformation { get; set; }

        [JsonProperty("pos_payment_info", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Pos payment info", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PosPaymentInfo { get; set; }

        [JsonProperty("repeat_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Repeat code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string RepeatCode { get; set; }

        [JsonProperty("techinflo_response_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Techinflo response code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TechinfloResponseCode { get; set; }

        [JsonProperty("cybersource_request_token", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Cybersource request token", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CybersourceRequestToken { get; set; }

        [JsonProperty("adyen_psp_reference", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen psp reference", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenPspReference { get; set; }

        [JsonProperty("adyen_event_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen event code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenEventCode { get; set; }

        [JsonProperty("adyen_payment_method", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Ayen payment method", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenPaymentMethod { get; set; }

        [JsonProperty("adyen_klarna_number", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen klarna number", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenKlarnaNbr { get; set; }

        [JsonProperty("adyen_avs_result", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("AdyenAvsResult", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenAvsResult { get; set; }

        [JsonProperty("adyen_cvc_result", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen cvc result", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenCvcResult { get; set; }

        [JsonProperty("adyen_boleto_paid_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Ayen boleto paid amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenBoletoPaidAmt { get; set; }

        [JsonProperty("adyen_total_fraud_score", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen total fraud score", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenTotalFraudScore { get; set; }

        [JsonProperty("adyen_refusal_reason_raw", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen refusal reason raw", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenRefusalReasonRaw { get; set; }

        [JsonProperty("adyen_acquirer_reference", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen acquirer reference", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenAcquirerReference { get; set; }

        [JsonProperty("adyen_auth_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen auth code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenAuthCode { get; set; }

        [JsonProperty("adyen_authorisation_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen authorisation amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenAuthorisationAmt { get; set; }

        [JsonProperty("adyen_card_bin", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Adyen card bin", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string AdyenCardBin { get; set; }

        [JsonProperty("save_card", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Save card", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string SaveCard { get; set; }

        [JsonProperty("fraud_action", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Fraud action", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string FraudAction { get; set; }

        [JsonProperty("fraud_codes", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Fraud codes", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string FraudCodes { get; set; }

        [JsonProperty("transaction_captured", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Transaction captured", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TransactionCaptured { get; set; }

        [JsonProperty("beagle_score", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Beagle score", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BeagleScore { get; set; }

        [JsonProperty("beagle_verification", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Beagle verification", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BeagleVerification { get; set; }

        [JsonProperty("payment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PaymentId { get; set; }
    }
    #endregion Helper classes

  
}
