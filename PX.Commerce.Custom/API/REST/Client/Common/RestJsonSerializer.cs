using AutoMapper;
using Newtonsoft.Json;
using PX.Commerce.Core.REST;
using PX.Data;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PX.Data.BQL.BqlPlaceholder;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Json class to customize parsing of request responses
    /// </summary>
    public class RestJsonSerializer : ISerializer, IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        public RestJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        public string DateFormat { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string ContentType { get; set; }

        T IDeserializer.Deserialize<T>(IRestResponse response)
        {
            String content = response.Content;
            JsonSerializerSettings settings = new JsonSerializerSettings { ContractResolver = new GetOnlyContractResolver() };
            
            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    var result = _serializer.Deserialize<T>(jsonTextReader);
                    return result;
                }
            }
        }
    }
}
