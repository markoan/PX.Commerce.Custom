using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	[JsonObject(Description = "ApiResponse")]

	public class ApiResponse
	{
		[JsonProperty("messages")]
		public virtual ApiMessages Messages { get; set; }

	}

	[JsonObject(Description = "messages")]
	public class ApiMessages
	{
		[JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<ErrorMessage> Error { get; set; }

		public override string ToString()
		{
			return "Error: " + Error == null ? String.Empty : String.Join(". ", Error);
		}
	}

	[JsonObject(Description = "message")]

	public class ErrorMessage
	{
		[JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
		public virtual HttpStatusCode Code { get; set; }

		[JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Message { get; set; }

		public override string ToString()
		{
			return Message;
		}
	}

	//Class for error 
	public abstract class RestError
	{
		[JsonProperty("status")]
		public int Status { get; set; }

	}
	public class RestError1 : RestError
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("details")]
		public Details Details { get; set; }

		[JsonProperty("errors")]
		public Errors Errors { get; set; }

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(Details?.InvalidReason))
				return Details?.InvalidReason;
			return Message;
		}
	}
	public class RestError2 : RestError
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("errors")]
		public Dictionary<String, String> Errors { get; set; }

		public override string ToString()
		{
			return Errors == null ? Title : String.Join("; ", Errors.Select(e => e.Value).ToArray());
		}
	}
	public class RestError3 : RestError
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("errors")]
		public string[] Errors { get; set; }

		public override string ToString()
		{
			return Errors == null ? Title : String.Join("; ", Errors);
		}
	}
	public class Errors
	{
		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class Details
	{
		[JsonProperty("invalid_reason")]
		public string InvalidReason { get; set; }
	}
}
