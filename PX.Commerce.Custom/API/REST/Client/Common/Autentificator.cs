using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Utility class to help add authentication headers for External app API communication
    /// </summary>
    public class Autentificator : IAuthenticator
    {
        private readonly string _oauth_consumer_key;
        private readonly string _oauth_consumer_secret;
        private readonly string _oauth_token;
        private readonly string _oauth_secret;
        private readonly string _oauth_signature_method;

        public Autentificator(string consumerKey, string consumerSecret, string token,
            string tokenSecret, string oauthMethod)
        {
            _oauth_consumer_key = consumerKey;
            _oauth_consumer_secret = consumerSecret;
            _oauth_token = token;
            _oauth_secret = tokenSecret;
            _oauth_signature_method = oauthMethod;

        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("oauth_consumer_key", _oauth_consumer_key);
            request.AddHeader("oauth_consumer_secret", _oauth_consumer_secret);
            request.AddHeader("oauth_token",_oauth_token);
            request.AddHeader("oauth_secret",_oauth_secret);
            request.AddHeader("oauth_signature_method",_oauth_signature_method);
        }
    }
}
