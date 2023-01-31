using Newtonsoft.Json;
using PX.Commerce.Core;
using PX.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

    public class ImageConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((ProductImageResponse)value).Data);
            return;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                return new ProductImageResponse()
                {
                    Data = serializer.Deserialize<ProductImageData>(reader),
                };
            }
            else
            {
                return new ProductImageResponse();
            }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

    public class ImageArrayConverter : JsonConverter
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((ProductImagesResponse)value).Data);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                return new ProductImagesResponse()
                {
                    Data = serializer.Deserialize<List<ProductImageData>>(reader),
                };
            }
            else
            {
                return new ProductImagesResponse();
            }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

    public class ProductImageDataConverter : JsonConverter
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && reader.ValueType == typeof(string))
            {
                return new ProductImageData()
                {
                    Id = serializer.Deserialize<string>(reader),
                };
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                existingValue = existingValue ?? serializer.ContractResolver.ResolveContract(objectType).DefaultCreator();
                serializer.Populate(reader, existingValue);
                return existingValue;
            }
            else if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else
            {
                throw new JsonSerializationException();
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

    [JsonConverter(typeof(ImageConverter))]
    public class ProductImageResponse : IEntityResponse<ProductImageData>
    {
        [JsonProperty("image")]
        public ProductImageData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [JsonConverter(typeof(ImageArrayConverter))]
    public class ProductImagesResponse : IEntitiesResponse<ProductImageData>
    {
        private List<ProductImageData> _data;

        public List<ProductImageData> Data
        {
            get
            {
                return _data ?? (_data = new List<ProductImageData>());
            }
            set
            {
                _data = value;
            }
        }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [JsonConverter(typeof(ProductImageDataConverter))]
    [JsonObject(Description ="Product -> Product Image")]
    [Description("Product Image")]
    public class ProductImageData : BCAPIEntity
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Id", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string Id { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Label", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string Label { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Position", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string Position { get; set; }

        [JsonProperty("exclude", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Exclude", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string Exclude { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Url", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
        [ShouldNotSerialize]
        public virtual string Url { get; set; }

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("Types", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        public virtual string[] Types { get; set; }


        [JsonProperty("file_name", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("FileName", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        [ShouldNotDeserialize]
        public virtual string FileName { get; set; }


        [JsonProperty("file_content", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("FileContent", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        [ShouldNotDeserialize]
        public virtual string FileContent { get; set; }


        [JsonProperty("file_mime_type", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("FileMimeType", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
        [ShouldNotDeserialize]
        public virtual string FileMimeType { get; set; }



    }
}
