using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Address type.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AddressTypes
	{
		/// <summary>
		/// billing: Billing address.
		/// </summary>
		[EnumMember(Value = "billing")]
		Billing = 1,

		/// <summary>
		/// shipping: Shipping address.
		/// </summary>
		[EnumMember(Value = "shipping")]
		Shipping = 2,

		/// <summary>
		/// billing,shipping: Both billing and shipping address.
		/// </summary>
		[EnumMember(Value = "billing,shipping")]
		BillingShipping = 3,

	}
}
