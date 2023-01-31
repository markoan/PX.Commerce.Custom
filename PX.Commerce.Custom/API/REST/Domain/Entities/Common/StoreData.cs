using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PX.Commerce.Custom.API.REST
{
	//Json response for store info
	public class StoreResponse : IEntityResponse<StoreData>
	{
		public StoreData Data { get; set; }
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

	public class StoreData 
	{
		/// <summary>
		/// Store allowed currencies
		/// </summary>
		[JsonProperty("allowed_currency")]
		public string[] AllowedCurrencies { get; set; }

		/// <summary>
		/// The base currency for the store 
		/// </summary>
		[JsonProperty("base_currency")]
		public string BaseCurrency { get; set; }

		/// <summary>
		/// Countries available for the store
		/// </summary>
		[JsonProperty("countries")]
		public List<Country> Countries { get; set; }

		/// <summary>
		/// The default id for the website 
		/// </summary>
		[JsonProperty("default_website_id")]
		public int DefaultId { get; set; }

		/// <summary>
		/// Store version
		/// </summary>
		[JsonProperty("version")]
		public string Version { get; set; }

		/// <summary>
		/// Timezone of the store 
		/// </summary>
		[JsonProperty("timezone")]
		public string Timezone { get; set; }

		[JsonProperty("store_address")]
		public StoreAddress StoreAddressInfo { get; set; }

		[JsonProperty("available", NullValueHandling = NullValueHandling.Ignore)]
		public bool Available { get; set; }

	}

	public class StoreAddress
    {
		[JsonProperty("default_website_name")]
		public string DefaultWebsiteName { get; set; }

		[JsonProperty("default_store_name")]
		public string DefaultStoreName { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("merchant_country")]
		public string Country { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("region_id")]
		public string RegionId { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("postal_code")]
		public string PostalCode { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }
      
	}

	public class Country
    {
		[JsonProperty("country_id")]
		public string CountryId;

		[JsonProperty("name")]
		public string Name;
    }

}
