using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{    public class OrderRestDataProvider : RestDataProviderBase, IParentRestDataProvider<OrderData>
    {
        protected override string GetListUrl { get; } = "orders/list";
        protected override string GetSingleUrl { get; } = "orders/{id}";
        protected override string GetCountUrl { get; } = "orders/count";
        protected override string GetSearchUrl => throw new NotImplementedException();
        protected override string PostSingleUrl => throw new NotImplementedException();
        protected override string PutSingleUrl => throw new NotImplementedException();

        public OrderRestDataProvider(ICustomRestClient restClient) : base ()
        {
            _client = restClient;
        }

        public OrderData Create(OrderData entity)
        {
            throw new NotImplementedException();
        }

        public OrderData Update(OrderData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(OrderData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderData> Get(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderData> GetAll(IFilter filter = null)
        {
            var request = BuildRequest(GetListUrl, nameof(this.Get), null, filter);
            OrdersResponse response = _client.GetList<OrderData, OrdersResponse>(request);
            return response;
        }

        public virtual OrderData GetByID(string id) => GetByID(id, false, false, false, false);

        public virtual OrderData GetByID(string id, bool includedMetafields = false, bool includedTransactions = false, bool includedCustomer = true, bool includedOrderRisk = false)
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
