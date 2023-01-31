using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    public class ProductCategoryRestDataProvider : RestDataProviderBase, IParentRestDataProvider<ProductCategoryData>
    {
        protected override string GetListUrl => "categories/";

        protected override string GetSingleUrl => "categories/{id}";
        protected override string GetCountUrl => throw new NotImplementedException();

        protected override string GetSearchUrl => throw new NotImplementedException();

        protected override string PostSingleUrl => "categories";
        protected override string PutSingleUrl => "categories/{categoryId}";

        public ProductCategoryRestDataProvider(ICustomRestClient restClient) : base()
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

        public string Create(ProductCategoryData entity)
        {
            CategoryPostResponse postResponse = Create<ProductCategoryData, CategoryPostResponse>(entity);
            return postResponse.Id;
        }

        public bool Delete(ProductCategoryData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategoryData> Get(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoriesResponse> GetAll(IFilter filter = null)
        {
            var request = BuildRequest(GetListUrl, nameof(this.Get), null, filter);
            GETCategoriesResponse response = _client.GetList<CategoriesResponse, GETCategoriesResponse>(request);
            return response;
        }

        public ProductCategoryData GetByID(string id)
        {
            string url = GetSingleUrl.Replace("{id}", id);
            var request = BuildRequest(url, nameof(this.Get));
            ProductCategoryData category = _client.Get<ProductCategoryData>(request);

            if (category != null) category.Id = id;

            return category;
        }

       

        ProductCategoryData IParentRestDataProvider<ProductCategoryData>.Create(ProductCategoryData entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ProductCategoryData> IParentRestDataProvider<ProductCategoryData>.GetAll(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public string Update(ProductCategoryData entity, int id)
        {
            string url = PutSingleUrl.Replace("{categoryId}", id.ToString());
            var request = BuildRequest(url, nameof(Method.PUT));

            UpdateCategoryResponse response = _client.Put<ProductCategoryData, UpdateCategoryResponse>(request, entity);
            if (response == null)
            {
                response = new UpdateCategoryResponse();
                response.code = HttpStatusCode.OK.ToString();
            }
            return response.code;
        }

        ProductCategoryData IParentRestDataProvider<ProductCategoryData>.Update(ProductCategoryData entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
