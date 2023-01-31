using System;
using System.Collections.Generic;
using System.Linq;
using PX.Commerce.Core;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using Newtonsoft.Json;

namespace PX.Commerce.Custom.API.REST
{
    //Parent class for REST client
    public abstract class CCRestClientBase : RestClient
    {
        protected ISerializer _serializer;
        protected IDeserializer _deserializer;
        public Serilog.ILogger Logger { get; set; } = null;

        //Initialize REST client
        protected CCRestClientBase(IDeserializer deserializer, ISerializer serializer, IRestOptions options, Serilog.ILogger logger)
        {
            _serializer = serializer;
            _deserializer = deserializer;
            AddHandler("application/json", () => { return deserializer; });
            AddHandler("text/json", () => { return deserializer; });
            AddHandler("text/x-json", () => { return deserializer; });

            Authenticator = OAuth1Authenticator.ForAccessToken(options.XAuthClient, options.XAuthClientSecret,
                options.XAuthToken,options.XAuthTokenSecret, options.XAuthMethod);

            try
            {
                BaseUrl = new Uri(options.BaseUri);
            }
            catch (UriFormatException e)
            {
                throw new UriFormatException("Invalid URL: The format of the URL could not be determined.", e);
            }
            Logger = logger;
        }

        //Create RestRequest based on the specific url
        public RestRequest MakeRequest(string url, Dictionary<string, string> urlSegments = null)
        {
            var request = new RestRequest(url) { JsonSerializer = _serializer, RequestFormat = DataFormat.Json };

            if (urlSegments != null)
            {
                foreach (var urlSegment in urlSegments)
                {
                    request.AddUrlSegment(urlSegment.Key, urlSegment.Value);
                }
            }
            return request;
        }


        //Write an error to log
        protected void LogError(Uri baseUrl, IRestRequest request, IRestResponse response)
        {
            //Get the values of the parameters passed to the API
            var parameters = string.Join(", ", request.Parameters.Select(x => (x.Name ?? "(empty)") + "=" + (x.Value ?? "NULL")).ToArray());
           
            //Set up the information message with the URL, the status code, and the parameters.
            var info = "Request to " + baseUrl.AbsoluteUri + request.Resource + " (" + response.ResponseUri.ToString() + ") with parameters: " + parameters + " failed with status code " + response.StatusCode + "\n Body: " +request?.Body?.Value?.ToString() ?? "(empty)";
            //Get the response body
            var description = "Response content: " + response.Content;

            //Acquire the actual exception
            var ex = (response?.ErrorException?.Message) ?? info;

            //Log the exception and info message
            Logger.ForContext("Scope", new BCLogTypeScope(GetType()))
                .ForContext("Exception", response.ErrorException?.Message)
                .Error("{CommerceCaption}: {ex}, Status Code: {StatusCode}.\n {description}", BCCaptions.CommerceLogCaption, ex, response.StatusCode, description);
        }

    }
}
