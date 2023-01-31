using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Whether customers are allowed to place an order for the product variant when it's out of stock. Valid values:
	/// deny: Customers are not allowed to place orders for the product variant if it's out of stock.
	/// allow: Customers are allowed to place orders for the product variant if it's out of stock.
	/// backorder: Customers are allowed to place orders for the product variant if it's out of stock and tell them it's on backorder.
	/// Default value: deny.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum InventoryPolicy
	{
		/// <summary>
		/// deny = 0: Customers are not allowed to place orders for the product variant if it's out of stock.
		/// </summary>
		[EnumMember(Value = "0")]
		Deny = 0,

		/// <summary>
		/// allow = 1 : Customers are allowed to place orders for the product variant if it's out of stock but don't tell them it's on backorder.
		/// </summary>
		[EnumMember(Value = "1")]
		Allow = 1,

		/// <summary>
		/// backorder = 2: Customers are allowed to place orders for the product variant if it's out of stock and tell them it's on backorder.
		/// </summary>
		[EnumMember(Value = "2")]
		Backorder = 2
	}
}
