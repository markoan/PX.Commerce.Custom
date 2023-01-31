using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Item visibility in store.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Visibility
	{
		/// <summary>
		/// NotVisible: Customers are not allowed to place orders for the product variant if it's out of stock.
		/// </summary>
		[EnumMember(Value = "1")]
		Hidden = 1,

		/// <summary>
		/// Catalog: Hidden from search.
		/// </summary>
		[EnumMember(Value = "2")]
		Catalog = 2,

		/// <summary>
		/// Search: Hidden from catalog.
		/// </summary>
		[EnumMember(Value = "3")]
		Search = 3,

		/// <summary>
		/// Visible: Visible in search and catalog.
		/// </summary>
		[EnumMember(Value = "4")]
		Visible = 4

	}
}
