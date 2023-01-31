using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	/// Turns value on or off.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Switch
	{
		/// <summary>
		/// Off
		/// </summary>
		[EnumMember(Value = "0")]
		Off = 0,

		/// <summary>
		/// On
		/// </summary>
		[EnumMember(Value = "1")]
		On = 1

	}
}
