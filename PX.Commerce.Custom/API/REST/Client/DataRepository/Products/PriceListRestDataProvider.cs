using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	public class PriceListRestDataProvider : RestDataProviderBase, IParentRestDataProvider<PriceList>
	{
		protected override string GetListUrl { get; } = "/products/{productid}/prices";

		protected override string GetSingleUrl { get; } = "/products/{productid}/prices";

        protected override string GetCountUrl => throw new NotImplementedException();

        protected override string GetSearchUrl => throw new NotImplementedException();

        protected override string PostSingleUrl { get; } = "/products/{productid}/prices";

		protected override string PutSingleUrl { get; } = "/products/{productid}/prices";

		public PriceListRestDataProvider(ICustomRestClient restClient) : base()
		{
			_client = restClient;
		}

		public PriceList GetByID(string id)
		{
            var segments = new UrlSegments();
            segments.Add("productid", id);

			var request = BuildRequest(GetSingleUrl, nameof(Method.GET), segments);

			PriceList response = _client.Get<PriceList>(request);
			return response;
		}

		public virtual PriceList Create(PriceList priceList)
		{
            var segments = new UrlSegments();
            segments.Add("productid", priceList.EntityId);

            var request = BuildRequest(PutSingleUrl, nameof(Method.PUT), segments);

			PriceList response = _client.Put<PriceList, PriceListResponse>(request, priceList)?.Data;

			return response;
		}

		public virtual PriceList Update(PriceList entity)
		{
			return this.Create(entity);
		}

        public PriceList Update(PriceList entity, int id)
        {
			return this.Create(entity);
		}

        public bool Delete(PriceList entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<PriceList> Get(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PriceList> GetAll(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public ItemCount Count()
        {
			throw new NotImplementedException();
        }

        public ItemCount Count(IFilter filter)
        {
			throw new NotImplementedException();
        }
    }

}
