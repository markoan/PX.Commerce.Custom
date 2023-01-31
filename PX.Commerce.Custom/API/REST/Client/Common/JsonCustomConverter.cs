using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Custom json converter to de/serialize API responses
    /// 
    /// Add any special handling here
    /// </summary>
    public class JsonCustomConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        /// <summary>
        /// Change to true and customize WriteJson
        /// </summary>
        public override bool CanWrite { get { return false; } }

        /// <summary>
        /// Change to true and customize ReadJson
        /// </summary>
        public override bool CanRead { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }

}
