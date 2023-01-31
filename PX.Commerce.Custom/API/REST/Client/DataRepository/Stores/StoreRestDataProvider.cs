using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

    public class StoreRestDataProvider : RestDataProviderBase
    {
  
        public StoreRestDataProvider(ICustomRestClient restClient)
        {
            _client = restClient;
        }

		protected override string GetListUrl => throw new NotImplementedException();
		protected override string GetSingleUrl => "/store/config";
		protected string CheckUrl => "/healthcheck";
		
		protected override string GetCountUrl => throw new NotImplementedException();
		protected override string GetSearchUrl => throw new NotImplementedException();

        protected override string PostSingleUrl => throw new NotImplementedException();

        protected override string PutSingleUrl => throw new NotImplementedException();

		public virtual StoreData Get()
		{
			var request = BuildRequest(GetSingleUrl, nameof(this.Get));
			StoreData response = (_client.Get<StoreData>(request));

			return response;
		}

		public virtual bool Check()
		{
			var request = BuildRequest(CheckUrl, nameof(this.Get));
			bool response = (_client.Get<bool>(request));

			return response;
		}

	}
}
