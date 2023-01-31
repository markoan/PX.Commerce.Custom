using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Whether item shown in the store.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Status
	{
		/// <summary>
		/// Active: Show this product in store.
		/// </summary>
		[EnumMember(Value = "1")]
		Active = 1,

		/// <summary>
		/// Inactive: Hide this product from store.
		/// </summary>
		[EnumMember(Value = "2")]
		Inactive = 2

	}
}
