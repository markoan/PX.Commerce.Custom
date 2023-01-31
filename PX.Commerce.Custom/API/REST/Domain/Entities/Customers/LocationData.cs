using Newtonsoft.Json;
using PX.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	#region Entity

	/// <summary>
	/// Declare entity as defined in the external API
	/// </summary>
	[JsonObject(Description = "locations")]
	[CommerceDescription("Location")]
	public class LocationData : BCAPIEntity, IEquatable<LocationData>
	{
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string Id { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string Name { get; set; }

		//...

		public bool Equals(LocationData obj) => obj?.Id == Id;

        public override int GetHashCode() => (Id == null ? 0 : Id.GetHashCode());
	}

    #endregion

    #region Entity Response
    public class LocationResponse : IEntityResponse<ContactData>
    {
        [JsonProperty("LocationData")]
        public ContactData Data { get; set; }
        [JsonIgnore()]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class LocationsResponse : List<ContactData> { }

    public class LocationResponseJson
    {
        [JsonProperty("LocationData")]
        public ContactData locationData { get; set; }
    }
    #endregion Entity Response

    #region Request Json
    public class LocationPutJson
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("locationData")]
        public ContactData Location { get; set; }
    }

    [JsonConverter(typeof(JsonCustomConverter))]
    public class LocationPostResponse : IEntityResponse<LocationResponseJson>
    {
        public LocationPostResponse(string id)
        {
            Id = id;
        }
        public LocationPostResponse() { }
        public string Id { get; set; }
        [JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        LocationResponseJson IEntityResponse<LocationResponseJson>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [JsonConverter(typeof(JsonCustomConverter))]
    public class UpdateLocationResponse : IEntityResponse<LocationPutJson>
    {
        public UpdateLocationResponse(string code)
        {

        }
        public string code { get; set; }
        public LocationPutJson Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UpdateLocationResponse() { }
    }
    #endregion Request Json

}
