using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    public class ShipmentRestDataProvider : RestDataProviderBase, IParentRestDataProvider<ShipmentData>
    {
        protected override string GetListUrl { get; } = "orders/shipments";
        protected override string GetSingleUrl { get; } = "orders/shipments/{id}";

        protected override string GetCountUrl => throw new NotImplementedException();

        protected override string GetSearchUrl => throw new NotImplementedException();

        protected override string PostSingleUrl { get; } = "orders/shipment/{id}";

        protected override string PutSingleUrl => throw new NotImplementedException();

        public ShipmentRestDataProvider(ICustomRestClient restClient) : base()
        {
            _client = restClient;
        }

        #region CREATE
        public virtual string Create(ShipmentData entity)
        {
            throw new NotImplementedException();
        }

        public string Create(ShipmentPost entity)
        {
            ShipmentPostResponse postResponse = Create<ShipmentPost, ShipmentPostResponse>(entity);
            return postResponse.Id;
        }

        #endregion CREATE

        #region READ
        public virtual IEnumerable<ShipmentData> GetAll(IFilter filter = null)
        {
            var request = BuildRequest(GetListUrl, nameof(this.GetAll));
            ShipmentsResponse response = _client.GetList<ShipmentData, ShipmentsResponse>(request);
            return response;
        }

        public virtual ShipmentData GetByID(string id) => GetByID(id, true, false);

        public ShipmentData GetByID(string shipmentId, bool includedMetafields = true, bool includeAllAddresses = false)
        {
            string url = GetSingleUrl.Replace("{id}", shipmentId);
            var request = BuildRequest(url, nameof(this.GetByID));
            ShipmentData response = _client.Get<ShipmentData>(request);
            return response;
        }
        #endregion

        ShipmentData IParentRestDataProvider<ShipmentData>.Create(ShipmentData entity)
        {
            throw new NotImplementedException();
        }

        public ShipmentData Update(ShipmentData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ShipmentData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ShipmentData> Get(IFilter filter = null)
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
