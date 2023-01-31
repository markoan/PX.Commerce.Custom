using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    public class PaymentRestDataProvider : RestDataProviderBase, IParentRestDataProvider<InvoiceData>
    {
        protected override string GetListUrl { get; } = "orders/payments";

        protected override string GetSingleUrl { get; } = "orders/payments/{id}";

        protected override string GetCountUrl => throw new NotImplementedException();

        protected override string GetSearchUrl => throw new NotImplementedException();

        protected override string PostSingleUrl => throw new NotImplementedException();

        protected override string PutSingleUrl => throw new NotImplementedException();

        public PaymentRestDataProvider(ICustomRestClient restClient) : base()
        {
            _client = restClient;
        }

        public ItemCount Count()
        {
            throw new NotImplementedException();
        }

        public ItemCount Count(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public InvoiceData Create(InvoiceData entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(InvoiceData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceData> Get(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoiceData> GetAll(IFilter filter = null)
        {
            var request = BuildRequest(GetListUrl, nameof(this.Get), null, filter);
            PaymentsResponse response = _client.GetList<InvoiceData, PaymentsResponse>(request);
            return response;
        }

        public InvoiceData GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public InvoiceData Update(InvoiceData entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
