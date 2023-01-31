using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Exception class to handle REST request errors
    /// </summary>
    public class RestException : Exception
	{
		public string ResponseMessage;
		public string ResponseStatusCode;
		public string ResponseContent;
		public IRestResponse Response;
		protected readonly IList<ErrorMessage> _errors;

		public string ErrorMessage => base.Message;

		public RestException(IRestResponse response) : base(GetErrorMessage(response))
		{
			Response = response;
			ResponseMessage = response.ErrorMessage;
			ResponseStatusCode = response.StatusCode.ToString();
			ResponseContent = response.Content;

			_errors = GetErrorData(response) ?? new List<ErrorMessage>();
		}

		public IEnumerator<ErrorMessage> GetEnumerator()
		{
			foreach (var error in _errors)
			{
				yield return error;
			}
		}

		public override string ToString()
		{
			return GetErrorMessage(Response);
		}

		public static String GetErrorMessage(IRestResponse response)
		{
			StringBuilder sb = new StringBuilder();

			//Returned Errors
			//bool errorsPersist = false;
			var restErrors = GetErrorData(response);


            if (restErrors != null && restErrors.Count > 0)
            {
				sb.Append($"Errors: "+String.Join(". ", restErrors));

			} else 
			{
				//Content
				if (!String.IsNullOrEmpty(response.Content))
				{
					sb.AppendLine($"Response:  {response.Content}");
					sb.AppendLine();
				}
			}

			sb.Append($" Status: {response.StatusCode}");

			return sb.ToString();
		}

		public static IList<ErrorMessage> GetErrorData(IRestResponse response)
		{
			if (response == null ||
				response.StatusCode == HttpStatusCode.OK ||
				response.StatusCode == HttpStatusCode.Created ||
				response.StatusCode == HttpStatusCode.Accepted ||
				response.StatusCode == HttpStatusCode.NoContent)
			{
				return null;
			}

			List<ErrorMessage> errorList = new List<ErrorMessage>();

			String content = response.Content;
			ApiResponse errorMessage = TryDeserialize<ApiResponse>(content);

			if (errorMessage != null)
			{
				return errorMessage?.Messages?.Error;
			}


			RestError1[] result1 = TryDeserialize<RestError1[]>(content);
			if (result1 != null)
			{
				foreach (RestError error in result1)
				{
					errorList.Add(new ErrorMessage() { Message = error.ToString(), Code = response.StatusCode });
				}
			}
			else
			{
				RestError2 result2 = TryDeserialize<RestError2>(content);
				if (result2 != null)
				{
					errorList.Add(new ErrorMessage() { Message = result2.ToString(), Code = response.StatusCode });

				}
				else
				{
					RestError3 result3 = TryDeserialize<RestError3>(content);
					if (result3 != null)
					{
						errorList.Add(new ErrorMessage() { Message = result3.ToString(), Code = response.StatusCode });

					}
				}
			}

			return errorList;
		}

		public static string CustomMessage(IRestResponse response, RestError error)
		{
			RestError2 Error = RestException.TryDeserialize<RestError2>(response.Content);
			if (Error?.Errors != null)
				if (Error?.Errors.ContainsKey("custom_url") == true)
				{
					var request = response.Request.Parameters.First(x => x.Type == ParameterType.RequestBody).Value;
					if (request != null)
						try
						{
							Type myType = request.GetType();
							IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
							PropertyInfo prop = props.FirstOrDefault(i => i.Name == "CustomUrl");
							if (prop != null)
							{
								string customMessage = "";
								if (!string.IsNullOrEmpty(customMessage))
									return customMessage;
							}
						}
						catch { }
				}
				else
				{
					if (!String.IsNullOrEmpty(Error.Title) && Error.Status == 422) //Missing fields		
					{
						string customMessage = String.Join(" ; ", Error.Errors.Select(e => string.Format("{0} {1}", e.Key, (e.Value == "error.path.missing" ? "is missing." : e.Value))).ToArray());
						if (!string.IsNullOrEmpty(customMessage))
							return customMessage;
					}
				}

			return error.ToString();
		}

		public static T TryDeserialize<T>(String content)
			where T : class
		{
			try
			{
				T result = JsonConvert.DeserializeObject<T>(content);
				return result;
			}
			catch (JsonSerializationException)
			{
				return null;
			}
		}
	}
}
