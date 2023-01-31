using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Whether customer is admin or restricted user.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CustomerRole
	{
		/// <summary>
		/// configurable: Template product.
		/// </summary>
		[EnumMember(Value = "1")]
		Admin = 1,

		/// <summary>
		/// virtual: Virtual non-stock item.
		/// </summary>
		[EnumMember(Value = "2")]
		Restricted = 2
	}
}
