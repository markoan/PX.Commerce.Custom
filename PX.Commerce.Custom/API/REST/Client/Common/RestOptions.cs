using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    //Build the headers for REST client 
    public interface IRestOptions
    {
        string BaseUri { get; set; }
        string XAuthClient { get; set; }
        string XAuthToken { get; set; }
        string XAuthClientSecret { get; set; }
        string XAuthTokenSecret { get; set; }
        string XAuthMethod { get; set; }
    }

    public class RestOptions : IRestOptions
    {
        public string BaseUri { get; set; }
        public string XAuthClient { get; set; }
        public string XAuthToken { get; set; }

        public string XAuthClientSecret { get; set; }
        public string XAuthTokenSecret { get; set; }
        public string SharedSecret { get; set; }
        public string XAuthMethod { get; set; }

        public override string ToString()
        {
            return $"base_url: {BaseUri},{Environment.NewLine}" +
                   $"oauth_consumer_key: {XAuthClient},{Environment.NewLine}" +
                   $"oauth_consumer_secret: {XAuthClientSecret},{Environment.NewLine}" +
                   $"oauth_token: {XAuthToken},{Environment.NewLine}" +
                   $"oauth_secret: {XAuthTokenSecret},{Environment.NewLine}" +
                   $"oauth_signature_method: {XAuthMethod},{Environment.NewLine}";

        }
    }
}
