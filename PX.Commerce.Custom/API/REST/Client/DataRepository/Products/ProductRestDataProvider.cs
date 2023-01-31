using PX.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static PX.Common.Async;

namespace PX.Commerce.Custom.API.REST
{

	public class ProductRestDataProvider : RestDataProvider, IParentRestDataProvider<ProductData>
    {
        protected override string GetListUrl { get; } = "products";
        protected override string GetSingleUrl { get; } = "products/{id}";
        protected override string GetCountUrl => throw new NotImplementedException();
        protected override string GetSearchUrl => throw new NotImplementedException();
        protected override string PostSingleUrl { get; } = "products";
        protected override string PutSingleUrl { get; } = "products/{id}";
        protected string GetCategoriesUrl { get; } = "products/{id}/categories";
        protected string SetCategoriesUrl { get; } = "products/{id}/categories";

        public ProductRestDataProvider(ICustomRestClient restClient) : base ()
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

        #region Create 
        public string Create(ProductData entity)
        {
          
            IdPostResponse postResponse = Create<ProductData,IdPostResponse>(entity);
            return postResponse.Id;
        }
        #endregion


        public bool Delete(ProductData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductData> Get(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductData> GetAll(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public ProductData GetByID(string id)
        {
            string url = GetSingleUrl.Replace("{id}", id);
            var request = BuildRequest(url, nameof(this.Get));
            ProductData response = _client.Get<ProductData>(request);
            return response;
        }

        public string Update(ProductData entity, int id)
        {
            string url = PutSingleUrl.Replace("{id}",id.ToString());
            var request = BuildRequest(url, nameof(Method.PUT));

            UpdateResponse response = _client.Put<ProductData,UpdateResponse>(request, entity);
            if(response == null)
            {
                response = new UpdateResponse();
                response.code = HttpStatusCode.OK.ToString();
            }
            return response.code;
        }

        public string UpdateQty(ProductQtyData entity, string id)
        {
            string url = PutSingleUrl.Replace("{id}", id);
            var request = BuildRequest(url, nameof(Method.PUT));

            UpdateQtyResponse response = _client.Put<ProductQtyData, UpdateQtyResponse>(request, entity);
            if (response == null)
            {
                response = new UpdateQtyResponse();
                response.code = HttpStatusCode.OK.ToString();
            }
            return response.code;
        }

        public virtual void UpdateAllQty(List<ProductQtyData> productDatas, Action<ItemProcessCallback<ProductQtyData>> callback)
        {
            var product = new ProductQtyList { Data = productDatas };
            UpdateAll<ProductQtyData, ProductQtyList>(product, new UrlSegments(), callback);
        }

        public GETCategoriesResponse GetProductCategories(string id)
        {
            string url = GetCategoriesUrl.Replace("{id}", id.ToString());
            var request = BuildRequest(url, nameof(Method.GET));

            GETCategoriesResponse categories = _client.Get<GETCategoriesResponse>(request);

            return categories;
        }

        public string SetProductCategories(string productId, SETCategoryToProduct body)
        {
            string url = SetCategoriesUrl.Replace("{id}", productId);
            var request = BuildRequest(url, nameof(Method.POST));
            SETCategoryToProduct response = _client.Post<SETCategoryToProduct>(request, body);
            return response.category_id;
        }

        ProductData IParentRestDataProvider<ProductData>.Create(ProductData entity)
        {
            throw new NotImplementedException();
        }

        ProductData IParentRestDataProvider<ProductData>.Update(ProductData entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
