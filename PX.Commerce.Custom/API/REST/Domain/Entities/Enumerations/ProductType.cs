using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Whether product is a simple or template product.
	/// "simple", "grouped", "configurable", "bundle", "downloadable", "virtual"
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProductType
	{
		/// <summary>
		/// simple: Simple product.
		/// </summary>
		[EnumMember(Value = "simple")]
		Simple = 0,

		/// <summary>
		/// configurable: Template product.
		/// </summary>
		[EnumMember(Value = "configurable")]
		Configurable = 1,

		/// <summary>
		/// virtual: Virtual non-stock item.
		/// </summary>
		[EnumMember(Value = "virtual")]
		Virtual = 2,

		/// <summary>
		/// downloadable: Donwloadable non-stock item.
		/// </summary>
		[EnumMember(Value = "downloadable")]
		Downloadable = 3,

		/// <summary>
		/// downloadable: Donwloadable non-stock item.
		/// </summary>
		[EnumMember(Value = "grouped")]
		Grouped = 4,

		/// <summary>
		/// downloadable: Donwloadable non-stock item.
		/// </summary>
		[EnumMember(Value = "bundle")]
		Bundle = 5
	}
}
