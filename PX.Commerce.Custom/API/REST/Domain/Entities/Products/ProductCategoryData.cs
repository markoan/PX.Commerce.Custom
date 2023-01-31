using Newtonsoft.Json;
using PX.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

    public class CategoryResponse : IEntityResponse<ProductCategoryData>
    {
        public CategoryResponse(string id)
        {
            Id = id;
        }

        public CategoryResponse() { }

        public string Id { get; set; }

        public ProductCategoryData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class GETCategoriesResponse : List<CategoriesResponse> {

    }

    public class SETCategoryToProduct
    {
        public string category_id { get; set; }
    }

    [JsonConverter(typeof(JsonCustomConverter))]
    //[JsonConverter(typeof(EmptyArrayConverter<CategoriesResponse>))]
    public class CategoriesResponse : IEntityResponse<ProductCategoryData>
    {
        public CategoriesResponse(string id)
        {
            Id = id;
        }

        public CategoriesResponse() { }

        public string Id { get; set; }

        [JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        ProductCategoryData IEntityResponse<ProductCategoryData>.Data { get; set; }

        public ProductCategoryData Category { get; set; }
    }

    [JsonConverter(typeof(JsonCustomConverter))]
    //[JsonConverter(typeof(EmptyArrayConverter<CategoryPostResponse>))]
    public class CategoryPostResponse : IEntityResponse<ProductCategoryData>
    {
        public CategoryPostResponse(string id)
        {
            Id = id;
        }
        public CategoryPostResponse() { }
        public string Id { get; set; }
        [JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        ProductCategoryData IEntityResponse<ProductCategoryData>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [JsonObject(Description = "Category")]
    [CommerceDescription("Product Category")]
    [JsonConverter(typeof(JsonCustomConverter))]
    public class ProductCategoryData : BCAPIEntity
    {
        [JsonIgnore]
        [CommerceDescription("Id", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string Id { get; set; }

        [JsonProperty("parent_id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Parent Id", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string ParentId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Name", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string Name { get; set; }

        [JsonProperty("url_key", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Url Key", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string UrlKey { get; set; }

        [JsonProperty("is_active", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Is Active", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public Switch IsActive { get; set; }

        [JsonProperty("include_in_menu", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Include In Menu", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public Switch IncludeInMenu { get; set; }

        [JsonProperty("include_in_menu_mobile", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Include In Menu Mobile", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public Switch IncludeInMenuMobile { get; set; }

        [JsonProperty("available_sort_by", NullValueHandling = NullValueHandling.Include)]
        [CommerceDescription("Available Sort By", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public IList<string> AvailableSortBy { get; set; }

        [JsonProperty("default_sort_by", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Default Sort By", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string DefaultSortBy { get; set; }
    }

    public class UpdateCategory
    {

        [JsonProperty("url_key", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Url Key", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string UrlKey { get; set; }

        [JsonProperty("available_sort_by", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Available Sort By", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string AvailableSortBy { get; set; }

        [JsonProperty("default_sort_by", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Default Sort By", FieldFilterStatus.Skipped, FieldMappingStatus.Export)]
        public string DefaultSortBy { get; set; }
    }

    [JsonConverter(typeof(JsonCustomConverter))]
    public class UpdateCategoryResponse : IEntityResponse<ProductCategoryData>
    {
        public UpdateCategoryResponse(string code)
        {

        }
        public string code { get; set; }
        public ProductCategoryData Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UpdateCategoryResponse() { }
    }

}
