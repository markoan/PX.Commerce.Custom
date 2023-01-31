using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /*
     Class to CRUD operations with product images
    */
    public class ProductImageRestDataProvider : RestDataProviderBase, IChildRestDataProvider<ProductImageData>
    {
        protected override string GetListUrl { get; } = "products/{id}/images";

        protected override string GetSingleUrl { get; } = "products/{id}/images/{imageId}";

        protected override string GetCountUrl => throw new NotImplementedException();

        protected override string GetSearchUrl => throw new NotImplementedException();

        protected override string PostSingleUrl { get; } = "products/{id}/images";

        protected override string PutSingleUrl { get; } = "products/{id}/images/{imageId}";

        public ProductImageRestDataProvider(ICustomRestClient restClient) : base()
        {
            _client = restClient;
        }

        public int Count(string parentId)
        {
            throw new NotImplementedException();
        }

        public ProductImageData Create(ProductImageData entity, string parentId)
        {
            var segments = new UrlSegments();
            segments.Add("id", parentId);
            var request = BuildRequest(PostSingleUrl, nameof(Method.POST), segments);
            ProductImageData response = _client.Post<ProductImageData>(request, entity);
            return response;
        }
        public ProductImageData Update(ProductImageData entity, string parentId, string id)
        {
            var segments = new UrlSegments();
            segments.Add("id", parentId);
            segments.Add("imageId", id);

            var request = BuildRequest(PutSingleUrl, nameof(Method.PUT), segments);
            ProductImageData response = _client.Put<ProductImageData>(request, entity);
            return response;
        }

        public bool Delete(string parentId, string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductImageData> GetAll(string parentId, IFilter filter = null)
        {
            var segments = new UrlSegments();
            segments.Add("id", parentId);

            var request = BuildRequest(GetListUrl, nameof(Method.GET), segments, filter);
            IEnumerable<ProductImageData> response = _client.GetAll<ProductImageData, ProductImagesResponse>(request);
            return response;
        }

        public IEnumerable<ProductImageData> GetAllWithoutParent(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public ProductImageData GetByID(string parentId, string id)
        {
            var segments = new UrlSegments();
            segments.Add("id", parentId);
            segments.Add("imageId", id);

            var request = BuildRequest(GetSingleUrl, nameof(Method.GET), segments);
            ProductImageData response = _client.Get<ProductImageData>(request);
            return response;
        }

        public IEnumerable<ProductImageData> GetCurrentList(string parentId, out string previousList, out string nextList, IFilter filter = null)
        {
            throw new NotImplementedException();
        }


    }
}
