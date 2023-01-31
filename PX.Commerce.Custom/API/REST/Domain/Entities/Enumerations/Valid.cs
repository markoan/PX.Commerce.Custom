using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Valid state of entity.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Valid
	{
		/// <summary>
		/// Valid
		/// </summary>
		[EnumMember(Value = "0")]
		Yes = 0,

		/// <summary>
		/// Invalid
		/// </summary>
		[EnumMember(Value = "1")]
		No = 1

	}
}
