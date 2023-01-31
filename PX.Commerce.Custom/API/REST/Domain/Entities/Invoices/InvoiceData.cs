using Newtonsoft.Json;
using PX.Commerce.Core;
using PX.Commerce.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    #region Zoey API Responses for Invoices
    //Get invoice by id response model
    public class PaymentResponse : IEntityResponse<InvoiceData>
    {
        [JsonProperty("payment")]
        public InvoiceData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    //Get all invoices from Zoey response model
    public class PaymentsResponse : List<InvoiceData> { }
    #endregion

    #region Zoey API Invoices Entities 

    //Invoice entity
    [JsonObject(Description = "Payment")]
    [CommerceDescription("Payments")]
    public class InvoiceData : BCAPIEntity
    {
        [JsonProperty("store_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Store Id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string StoreId { get; set; }

        [JsonProperty("base_grand_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base grand total", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseGrandTotal { get; set; }

        [JsonProperty("shipping_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingTaxAmt { get; set; }

        [JsonProperty("tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TaxAmt { get; set; }

        [JsonProperty("base_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseTaxAmt { get; set; }

        [JsonProperty("store_to_order_rate", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Store to order rate", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string  StoreToOrderRate { get; set; }

        [JsonProperty("base_shipping_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string BaseShippingTaxAmt { get; set; }

        [JsonProperty("base_discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base discount amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseDiscountAmt { get; set; }

        [JsonProperty("base_to_order_rate", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base to order rate", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseToOrderRate { get; set; }

        [JsonProperty("grand_total", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Grand total", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string GrandTotal { get; set; }

        [JsonProperty("shipping_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping Amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingAmt{ get; set; }

        [JsonProperty("subtotal_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Subtotal incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string SubtotalInclTax { get; set; }

        [JsonProperty("base_subtotal_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base subtotal incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseSubtotalInclTax { get; set; }

        [JsonProperty("store_to_base_rate", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Store to base rate", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string StoreToBaseRate { get; set; }

        [JsonProperty("base_shipping_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base Shipping Amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseShippingAmt { get; set; }

        [JsonProperty("total_qty", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Total Qty", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TotalQty { get; set; }

        [JsonProperty("base_to_global_rate", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base to global rate", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseToGlobalRate { get; set; }

        [JsonProperty("subtotal", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Subtotal", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string Subtotal { get; set; }

        [JsonProperty("base_subtotal", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base subtotal", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseSubtotal { get; set; }

        [JsonProperty("discount_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string DiscountAmt { get; set; }

        [JsonProperty("billing_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Billing address id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BillingAddressId { get; set; }

        [JsonProperty("is_used_for_refund", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is used for refund", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string IsUsedForRefund { get; set; }

        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Order id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OrderId { get; set; }

        [JsonProperty("email_sent", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Email sent", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string EmailSent { get; set; }

        [JsonProperty("can_void_flag", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Can void flag", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CanVoidFlag { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("State", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string State { get; set; }

        [JsonProperty("shipping_address_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping address id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingAddressId { get; set; }

        [JsonProperty("store_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Store currency code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string StoreCurrencyCode { get; set; }

        [JsonProperty("transaction_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Transaction id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string TransactionId { get; set; }

        [JsonProperty("order_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Order currency code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OrderCurrencyCode { get; set; }

        [JsonProperty("base_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base currency code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseCurrencyCode { get; set; }

        [JsonProperty("global_currency_code", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Global currency code", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string GlobalCurrencyCode { get; set; }

        [JsonProperty("increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Increment id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string IncrementId { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Created at", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string CreatedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Updated at", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string UpdatedAt { get; set; }

        [JsonProperty("hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Hidden tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string HiddenTaxAmt { get; set; }

        [JsonProperty("base_hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base hidden tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseHiddenTaxAmt { get; set; }

        [JsonProperty("shipping_hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping hidden tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingHiddenTaxAmt { get; set; }

        [JsonProperty("base_shipping_hidden_tax_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping hidden tax amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseShippingHiddenTaxAmt { get; set; }

        [JsonProperty("shipping_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Shipping incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ShippingInclTax { get; set; }

        [JsonProperty("base_shipping_incl_tax", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base shipping incl tax", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseShippingInclTax { get; set; }

        [JsonProperty("base_total_refunded", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base total refunded", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseTotalRefunded { get; set; }

        [JsonProperty("discount_description", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Discount description", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string DiscountDescription { get; set; }

        [JsonProperty("old_invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Old invoice id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OldInvoiceId { get; set; }

        [JsonProperty("old_invoice_increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Old invoice increment id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OldInvoiceIncrementId { get; set; }

        [JsonProperty("base_giftcard_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base giftcard discount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseGiftcardDisc { get; set; }

        [JsonProperty("giftcard_discount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Giftcard discount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string GiftcardDisc { get; set; }

        [JsonProperty("payment_fee_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment fee amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PaymentFeeAmt { get; set; }

        [JsonProperty("base_payment_fee_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base payment fee amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BasePaymentFeeAmt { get; set; }

        [JsonProperty("payment_installment_fee_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment installment fee amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PaymentInstallmentFeeAmt { get; set; }

        [JsonProperty("zoey_custom_options_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Zoey custom options price", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ZoeyCustomOptionsPrice { get; set; }

        [JsonProperty("base_zoey_custom_options_price", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base zoey custom options price", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseZoeyCustomOptionsPrice { get; set; }

        [JsonProperty("is_archived", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is archived", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string IsArchived { get; set; }

        [JsonProperty("payment_surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Payment surcharge amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string PaymentSurchargeAmt { get; set; }

        [JsonProperty("base_payment_surcharge_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base payment surcharge amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BasePaymentSurchargeAmt { get; set; }

        [JsonProperty("apply_store_credit", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Apply store credit", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string ApplyStoreCredit { get; set; }

        [JsonProperty("base_store_credit_amount", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Base store credit amount", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string BaseStoreCreditAmt { get; set; }

        [JsonProperty("invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Invoice id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string InvoiceId { get; set; }

        [JsonProperty("order_increment_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Order increment id", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual string OrderIncrementId { get; set; }

        [JsonProperty("comments", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Comments", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual object Comments { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Items", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        public virtual List<Item> Items { get; set; }


    }

    public class PmtMethods : List<PmtMethod> { }
    public class PmtMethod
    {
        [JsonProperty("cc_types", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CCType { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Code { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Tilte { get; set; }
    }

    #endregion Zoey API Invoices Entities
}
