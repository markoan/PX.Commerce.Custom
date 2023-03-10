using PX.Commerce.Custom.API.REST;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
	[PXLocalizable()]
	public static class ConnectorMessages
	{

        public const string DataProviderNotSupportMethod = "Data provider {0} does not support the {1} method.";
		public const string DiscountAppliedToShippingItem = "Discount applied to shipping item";
		public const string DiscountAppliedToLineItem = "Discount applied to line item";
		public const string DiscountCombined = "Aggregated order discount";
		public const string RefundDiscount = "Refund Discount";
		public const string FeatureRequired = "The {0} feature must be enabled for the Custom connector to work properly. Please enable the feature on the Enable/Disable Features (CS100000) form.";
		public const string FeatureNotSupported = "The {0} feature must be disabled for the Custom connector to work properly. Please disable the feature on the Enable/Disable Features (CS100000) form.";
		public const string InventoryLocationNotFound = "The inventory location data could not be obtained from App. Please check your network connection and the store settings on the Custom Stores form.";
		public const string ExternalLocationNotFound = "The Product Availability entity cannot be synchronized because the Custom location could not be found. Please check the warehouse and location mapping on the Inventory Settings tab of the Custom Stores (BC201010) form.";
		public const string NoGuestCustomer = "The customer record to be used for synchronization cannot be found. Please make sure that the customer exists and the guest customer account is specified in the store settings on the Custom Stores (BC201010) form.";
		public const string NoRequiredField = "{0} is a required field. Please provide {0} in the {1} record.";
		public const string NoCustomerNumbering = "The Custom integration requires autonumbering of customers. Please configure a numbering sequence in the store settings on the Custom Stores (BC201010) form or on the Segmented Keys (CS202000) form.";
		public const string NoLocationNumbering = "The Custom integration requires autonumbering of customer locations. Please configure a numbering sequence in the store settings on the Custom Stores (BC201010) form or on the Segmented Keys (CS202000) form.";
		public const string NoCustomerClass = "The Custom integration requires the default customer class to be configured. Please configure the default customer class on the Custom Stores (BC201010) form.";
		public const string NoBranch = "The Custom integration requires the default branch to be configured. Please configure the default branch on the Custom Stores (BC201010) form.";
		public const string NoSalesOrderType = "The Custom integration requires an active sales order type to be specified as the default. Please configure the default sales order type on the Custom Stores (BC201010) form.";
		public const string NoReturnOrderType = "The Custom integration requires the default return order type to be configured. Please configure the default return order type on the Custom Stores (BC201010) form.";
		public const string NoSubstituteValues = "The Custom integration requires substitute values for the {0} mapping. Please provide substitute values on the Substitute Lists (BC105000) form.";
		public const string NoDefaultCashAccount = "The payment cannot be saved because the Custom integration requires the default cash account for the {0} payment method. Please specify the default cash account on the Payment Methods (CA204000) form.";
		public const string InvalidStoreUrl = "Invalid store URL. Please make sure the URL is specified in the following format: https://yourstorename.com/admin/";
		public const string TestConnectionStoreNotFound = "Cannot retrieve the store through REST API. Please check if the store URL is correct.";
		public const string TooManyApiCalls = "The API call failed because too many requests were sent in a short period of time. Please try again later.";
		public const string StoreNotFound = "Store data cannot be found. Please check request";
		public const string LogInvoiceSkippedNoOrder = "The {0} invoice has been skipped because no externally synchronized orders have been found in invoice details.";
		public const string SONotFound = "Sales order {0} cannot be found in the system. Please synchronize it.";

		public const string ImportError = "Importing {0} error with code: {1}";
		public const string UpdateError = "Updating {0} error with code: {1}";


	}

	[PXLocalizable()]
	public static class CustomCaptions
	{
		//API Object Descriptions
		public const string AcceptsMarketing = "Accepts Marketing";
		public const string AddressLine1 = "Address Line 1";
		public const string AddressLine2 = "Address Line 2";
		public const string Amount = "Amount";
		public const string AmountSet = "Amount Set";
		public const string Authorization = "Authorization";
		public const string AvsResultCode = "AVS Result Code";
		public const string AvalaraTaxCode = "Avalara Tax Code";
		public const string Barcode = "Barcode";
		public const string BillingAddress = "Billing Address";
		public const string BodyHTML = "Body HTML Description";
		public const string CancelReason = "Cancel Reason";
		public const string CarrierIdentifier = "Carrier Identifier";
		public const string City = "City";
		public const string ClientDetails = "Client Details";
		public const string Code = "Code";
		public const string CompanyName = "Company Name";
		public const string Country = "Country";
		public const string CountryName = "Normalized Country Name";
		public const string CountryISOCode = "Country ISO Code";
		public const string CreditCard = "Credit Card";
		public const string CreditCardBin = "Credit Card BIN";
		public const string CreditCardCompany = "Credit Card Company";
		public const string CreditCardNumber = "Credit Card Number";
		public const string Currency = "Currency";
		public const string CurrencyCode = "Currency Code";
		public const string CurrencyExchangeRate = "Currency Exchange Rate";
		public const string Customer = "Customer";
		public const string CustomerAddress = "Customer Address";
		public const string CustomerAddressData = "Customer Address Data";
		public const string CustomerId = "Customer ID";
		public const string CvvResult = "CVV Result";
		public const string CvvResultCode = "CVV Result Code";
		public const string DateCanceled = "Date Canceled";
		public const string DateCreated = "Date Created";
		public const string DateModified = "Date Modified";
		public const string DateShipped = "Date Shipped";
		public const string DefaultAddress = "Default Address";
		public const string Depth = "Depth";
		public const string DeviceId = "Device ID";
		public const string Discount = "Discount";
		public const string DiscountedPrice = "Discounted Price";
		public const string DiscountedPriceSet = "Discounted Price Set";
		public const string DiscountAllocation = "Discount Allocation";
		public const string DiscountAmount = "Discount Amount";
		public const string DiscountRule = "Discount Rule";
		public const string Duties = "Duties";
		public const string ErrorCode = "Error Code";
		public const string Email = "Email";
		public const string EmailAddress = "Email Address";
		public const string EventName = "Event Name";
		public const string FinancialStatus = "Financial Status";
		public const string FirstName = "First Name";
		public const string Fulfillment = "Fulfillment";
		public const string FulfillableQuantity = "Fulfillable Quantity";
		public const string FulfillmentService = "Fulfillment Service";
		public const string FulfillmentStatus = "Fulfillment Status";
		public const string Gateway = "Gateway";
		public const string GiftCard = "Gift Card";
		public const string GlobalDescriptionTag = "Global Description Tag";
		public const string GlobalTitleTage = "Global Title Tag";
		public const string GlobalTradeNumber = "Global Trade Number";
		public const string Height = "Height";
		public const string Id = "ID";
		public const string ImageUrl = "Image Url";
		public const string InventoryBehaviour = "Inventory Behavior";
		public const string InventoryItem = "Inventory Item";
		public const string InventoryLevel = "Inventory Level";
		public const string InventoryLocation = "Inventory Location";
		public const string InventoryManagement = "Inventory Management";
		public const string InventoryPolicy = "Inventory Policy";
		public const string InventoryTracking = "Inventory Tracking";
		public const string Invoice = "Invoice";
		public const string IsDefault = "Is Default";
		public const string ItemShipped = "Item Shipped";
		public const string ItemsTotal = "Items Total";
		public const string ItemsTotalSet = "Items Total Set";
		public const string Kind = "Kind";
		public const string LandingSite = "Landing Site";
		public const string LastName = "Last Name";
		public const string Latitude = "Latitude";
		public const string LineItem = "Line Item";
		public const string LineItemId = "Line Item ID";
		public const string LocationId = "Location ID";
		public const string Longitude = "Longitude";
		public const string Message = "Message";
		public const string MetaDescription = "Meta Description";
		public const string Metafields = "Metafields";
		public const string MetaKeywords = "Meta Keywords";
		public const string MetaNamespace = "Meta Namespace";
		public const string MetaValueType = "Meta Value Type";
		public const string Method = "Method";
		public const string MinimumOrderQuantity = "Minimum Order Quantity";
		public const string Name = "Name";
		public const string NameLabel = "Name Label";
		public const string Note = "Note";
		public const string NoteAttribute = "Note Attribute";
		public const string NotifyCustomer = "Notify Customer";
		public const string Option1 = "Option1";
		public const string Option2 = "Option2";
		public const string Option3 = "Option3";
		public const string OrderAdjustment = "Order Adjustment";
		public const string OrderAddress = "Order Address";
		public const string OrderData = "Order Data";
		public const string OrderDate = "Order Date";
		public const string OrderId = "Order ID";
		public const string OrderItemLocation = "Order Item Location";
		public const string OrderNumber = "Order Number";
		public const string OrderPaymentStatus = "Order Payment Status";
		public const string OrderRisk = "Order Risk";
		public const string OrderTotal = "Order Total";
		public const string OrderTotalSet = "Order Total Set";
		public const string OrdersProduct = "Orders Product";
		public const string OrdersProductsOption = "Orders Products Option";
		public const string OrdersProductsType = "Orders Products Type";
		public const string OrdersShipment = "Orders Shipment";
		public const string OrdersShippingAddress = "Orders Shipping Address";
		public const string OrdersTax = "Orders Tax";
		public const string OrderStatusURL = "Order Status URL";
		public const string OrdersTransaction = "Orders Transaction";
		public const string OrdersTransactionData = "Orders Transaction Data";
		public const string PageTitle = "Page Title";
		public const string ParentId = "Parent ID";
		public const string Password = "Password";
		public const string PaymentDetail = "Payment Detail";
		public const string PaymentMethod = "Payment Method";
		public const string Phone = "Phone";
		public const string PhoneNumber = "Phone Number";
		public const string PostalCode = "Postal Code";
		public const string PresentmentCurrency = "Presentment Currency";
		public const string PresentmentMoney = "Presentment Money";
		public const string Price = "Price";
		public const string PriceExcludingTax = "Price Excluding Tax";
		public const string PriceIncludingTax = "Price Including Tax";
		public const string PriceListId = "Price List Id";
		public const string PriceSet = "Price Set";
		public const string PriceTax = "Price Tax";
		public const string ProcessedAt = "Date Processed";
		public const string ProcessingMethod = "Processing Method";
		public const string Product = "Product";
		public const string ProductQuantityData = "Product Quantity"; 
		public const string ProductDescription = "Product Description";
		public const string ProductExists = "Product Exists";
		public const string ProductId = "Product Id";
		public const string ProductName = "Product Name";
		public const string ProductOptions = "Product Options";
		public const string ProductTaxCode = "Product Tax Code";
		public const string ProductType = "Product Type";
		public const string ProductVariants = "Product Variants";
		public const string Properties = "Properties";
		public const string Province = "Province";
		public const string ProvinceCode = "Province Code";
		public const string Published = "Published";
		public const string PublishedScope = "Published Scope";
		public const string Quantity = "Quantity";
		public const string QuantityRefund = "Quantity Refund";
		public const string QuantityShipped = "Quantity Shipped";
		public const string Reason = "Reason";
		public const string Receipt = "Receipt";
		public const string Recommendation = "Recommendation";
		public const string ReferringSite = "Referring Site";
		public const string Refund = "Refund";
		public const string RefundAmount = "Refund Amount";
		public const string RefundId = "Refund ID";
		public const string RefundItem = "Refund Item";
		public const string RemainingBalance = "Remaining Balance";
		public const string RequiresShipping = "Requires Shipping";
		public const string RestockType = "Restock Type";
		public const string RetailPrice = "Retail Price";
		public const string SalePrice = "Sale Price";
		public const string SearchKeywords = "Search Keywords";
		public const string SendReceipt = "Send Receipt";
		public const string SendFulfillmentReceipt = "Send Fulfillment Receipt";
		public const string Service = "Service";
		public const string Score = "Score";
		public const string ShipmentData = "Shipment Data";
		public const string ShipmentID = "Shipment ID";
		public const string ShipmentItems = "Shipment Items";
		public const string ShipmentStatus = "Shipment Status";
		public const string ShippingAddress = "Shipping Address";
		public const string ShippingCostExcludingTax = "Shipping Cost Excluding Tax";
		public const string ShippingCostIncludingTax = "Shipping Cost Including Tax";
		public const string ShippingCostTax = "Shipping Cost Tax";
		public const string ShippingLine = "Shipping Line";
		public const string ShippingMethod = "Shipping Method";
		public const string ShippingMethodId = "Shipping Method Id";
		public const string ShippingMethodName = "Shipping Method Name";
		public const string ShippingMethodType = "Shipping Method Type";
		public const string ShippingProvider = "Shipping Provider";
		public const string ShippingTo = "Shipping To";
		public const string ShippingZone = "Shipping Zone";
		public const string ShippingZoneId = "Shipping Zone Id";
		public const string ShippingZoneName = "Shipping Zone Name";
		public const string ShippingZoneType = "Shipping Zone Type";
		public const string ShopMoney = "Shop Money";
		public const string SKU = "SKU";
		public const string SortOrder = "Sort Order";
		public const string SourceName = "Source Name";
		public const string State = "State";
		public const string Status = "Status";
		public const string Street1 = "Street 1";
		public const string Street2 = "Street 2";
		public const string StreetMatch = "Street Match";
		public const string Subtotal = "Subtotal";
		public const string SubtotalExcludingTax = "Subtotal Excluding Tax";
		public const string SubtotalIncludingTax = "Subtotal Including Tax";
		public const string SubtotalSet = "Subtotal Set";
		public const string SubtotalTax = "Subtotal Tax";
		public const string Tags = "Tags";
		public const string Taxable = "Taxable";
		public const string TaxAmount = "Tax Amount";
		public const string TaxAmountSet = "Tax Amount Set";
		public const string TaxesIncluded = "Taxes Included";
		public const string TaxExempt = "Tax Exempt";
		public const string TaxExemptions = "Tax Exemptions";
		public const string TaxLine = "Tax Line";
		public const string TaxLineAmount = "Tax Line Amount";
		public const string TaxLineItemType = "Tax Line Item Type";
		public const string TaxName = "Tax Name";
		public const string TaxRate = "Tax Rate";
		public const string TestCase = "Test Case";
		public const string TestTransaction = "Test Transaction";
		public const string Token = "Token";
		public const string TotalExcludingTax = "Total Excluding Tax";
		public const string TotalIncludingTax = "Total Including Tax";
		public const string TotalDiscount = "Total Discount";
		public const string TotalDiscountSet = "Total Discount Set";
		public const string TotalTax = "Total Tax";
		public const string TotalTaxSet = "Total Tax Set";
		public const string TotalTips = "Total Tips";
		public const string TotalWeight = "Total Weight";
		public const string TipPaymentGateway = "Tip Payment Gateway";
		public const string TipPaymentMethod = "Tip Payment Method";
		public const string Title = "Title";
		public const string TrackingCarrier = "Tracking Carrier";
		public const string TrackingCompany = "Tracking Company";
		public const string TrackingID = "Tracking ID";
		public const string TrackingInfo = "Tracking Info";
		public const string TrackingNumber = "Tracking Number";
		public const string TrackingNumbers = "Tracking Numbers";
		public const string TrackingUrl = "Tracking URL";
		public const string TrackingUrls = "Tracking URLs";
		public const string Transactions = "Transactions";
		public const string Type = "Type";
		public const string UPCCode = "UPC Code";
		public const string UserId = "User ID";
		public const string Value = "Value";

		public const string VariantId = "Variant ID";
		public const string VariantTitle = "Variant Title";
		public const string Vendor = "Vendor";
		public const string Weight = "Weight";
		public const string WeightUnit = "Weight Unit";
		public const string Zipcode = "Zipcode";
	}

	[PXLocalizable()]
	public static class CustomApiStatusCodes
	{
		public const string Code_200 = "Status Code 200/OK : The request was successfully processed.";
		public const string Code_201 = "Status Code 201/Created : The request has been fulfilled and a new resource has been created.";
		public const string Code_202 = "Status Code 202/Accepted : The request has been accepted, but not yet processed.";
		public const string Code_303 = "Status Code 303/See Other : The response to the request can be found under a different URL in the Location header and can be retrieved using a GET method on that resource.";
		public const string Code_400 = "Status Code 400/Bad Request : The request was not understood by the server, generally due to bad syntax or because the Content-Type header was not correctly set to application/json. This status is also returned when the request provides an invalid code parameter during the OAuth token exchange process.";
		public const string Code_401 = "Status Code 401/Unauthorized : The necessary authentication credentials are not present in the request or are incorrect.";
		public const string Code_402 = "Status Code 402/Payment Required : The requested shop is currently frozen. The shop owner needs to log in to the shop's admin and pay the outstanding balance to unfreeze the shop.";
		public const string Code_403 = "Status Code 403/Forbidden : The server is refusing to respond to the request. This is generally because you have not requested the appropriate scope for this action.";
		public const string Code_404 = "Status Code 404/Not Found : The requested resource was not found but could be available again in the future.";
		public const string Code_406 = "Status Code 406/Not Acceptable : The requested resource is only capable of generating content not acceptable according to the Accept headers sent in the request.";
		public const string Code_422 = "Status Code 422/Unprocessable Entity : The request body was well-formed but contains semantic errors. The response body will provide more details in the errors or error parameters.";
		public const string Code_423 = "Status Code 423/Locked : The requested shop is currently locked. Shops are locked if they repeatedly exceed their API request limit, or if there is an issue with the account, such as a detected compromise or fraud risk. Contact support if your shop is locked.";
		public const string Code_429 = "Status Code 429/Too Many Requests : The request was not accepted because the application has exceeded the rate limit. See the API Call Limit documentation for a breakdown of rate-limiting mechanism.";
		public const string Code_500 = "Status Code 500/Internal Server Error : An internal error occurred.";
		public const string Code_501 = "Status Code 501/Not Implemented : The requested endpoint is not available on that particular shop, e.g. requesting access to a Plus-specific API on a non-Plus shop. This response may also indicate that this endpoint is reserved for future use.";
		public const string Code_503 = "Status Code 503/Service Unavailable : The server is currently unavailable. Check the status page for reported service outages.";
		public const string Code_504 = "Status Code 504/Gateway Timeout : The request could not complete in time. Try breaking it down in multiple smaller requests.";
		public const string Code_Unknown = "Code {0} : Unknown code";

		public static string GetCodeMessage(string code)
		{
			if (string.IsNullOrEmpty(code))
				return string.Empty;
			string codeDesc = int.TryParse(code, out var intCode) ? $"Code_{intCode}" : code;
			var codeItem = typeof(CustomApiStatusCodes).GetField(codeDesc, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Static);
			return codeItem != null ? codeItem.GetRawConstantValue()?.ToString() : string.Format(Code_Unknown, code);
		}
	}

	[PXLocalizable()]
	public static class CustomConstants
    {
		#region Account 
		
		#endregion

		#region Products
		public const string WEIGHT_DEFAULT = "1";
		public const string TAXABLE_ITEM = "2"; //Taxable Item
		public const string IS_IN_STOCK = "1";
		public const string ATTRIBUTE_ID_DEFAULT = "4";
		public const string PRICE_DEFAULT = "1";
		public const Status STATUS_DEFAULT = Status.Active;
		public const Visibility VISIBILITY_DEFAULT =  Visibility.Visible;
		#endregion

		#region Orders
		public enum OrderStatus { complete, open, canceled, pending, pending_payment, processing, closed, holded, payment_review, fraud}
        #endregion
    }

	[PXLocalizable()]
	public class AccountAddressTypes
    {
		public const string BILLING = "billing";
		public const string SHIPPING = "shipping";
		public const string BILLING_SHIPPING = "billing, shipping";
    }
}
