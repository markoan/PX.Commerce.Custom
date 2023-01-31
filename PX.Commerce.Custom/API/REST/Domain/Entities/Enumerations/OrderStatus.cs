using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrderStatus
	{
		[EnumMember(Value = "any")]
		Any,

		[EnumMember(Value = "open")]
		Open,

		[EnumMember(Value = "closed")]
		Closed,

		[EnumMember(Value = "cancelled")]
		Cancelled

	}
}
