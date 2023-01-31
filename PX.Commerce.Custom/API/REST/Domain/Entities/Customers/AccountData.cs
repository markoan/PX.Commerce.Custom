using Newtonsoft.Json;
using PX.Commerce.Core;
using PX.Commerce.Custom.API.REST;
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
    [JsonObject(Description = "Account")]
	[CommerceDescription("Customer")]
	public class AccountData : BCAPIEntity
	{
		[JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("CreatedDate", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string CreatedAt { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        [CommerceDescription("UpdatedDate", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
        public virtual string UpdatedAt { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Id", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string Id { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Name", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string Name { get; set; }

		//...

	}
	#endregion

	#region Get Entity

	[JsonConverter(typeof(JsonCustomConverter))]
	public class AccountsByCustomer : IEntityResponse<AccountData>
    {
        [JsonIgnore]
        public AccountData Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
    //Single result
    public class AccountResponse : IEntityResponse<AccountData>
	{
		[JsonProperty("account")]
		public AccountData Data { get; set; }
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
	//Get all
	public class AccountsResponse : List<AccountData>
	{

	}
    #endregion

    #region Request Formats
	public class AccountPostRequest
    {
		[JsonProperty("companyData")]
		public AccountData accountData { get; set; }

	}

	public class AccountPutRequest
    {
		[JsonProperty("companyId", Order = -2)]
		public string CompanyId { get; set; }

		[JsonProperty("companyData")]
		public AccountData CompanyData { get; set; }
	}
	#endregion Request Formats

	#region Response Formats
	[JsonConverter(typeof(JsonCustomConverter))]
	public class UpdateAccountResponse : IEntityResponse<AccountPutRequest>
	{
		public UpdateAccountResponse(string code)
		{

		}
		public string code { get; set; }
		public AccountPutRequest Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public UpdateAccountResponse() { }
	}

    public class AccountPostResponse : IEntityResponse<AccountPostRequest>
    {
        [JsonIgnore]
		public AccountData Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		AccountPostRequest IEntityResponse<AccountPostRequest>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}

    #endregion Response Formats

    #region Helper classes

	public class AccountItem
    {
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
		public string Status { get; set; }

		[JsonProperty("enable_all_shipping_methods", NullValueHandling = NullValueHandling.Ignore)]
		public string EnableAllShippingMethods { get; set; }

		[JsonProperty("enable_all_payment_methods", NullValueHandling = NullValueHandling.Ignore)]
		public string EnableAllPaymentMethods { get; set; }

		[JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
		public string CreatedAt { get; set; }

		[JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
		public string UpdatedAt { get; set; }

		[JsonProperty("external_id", NullValueHandling = NullValueHandling.Ignore)]
		public string ExternalId { get; set; }

		[JsonProperty("company_location_id", NullValueHandling = NullValueHandling.Ignore)]
		public string CompanyLocationId { get; set; }

	}


	[JsonConverter(typeof(JsonCustomConverter))]
	public class AccountPostResponseObj : IEntityResponse<AccountPostRequest>
    {
		[JsonConstructor]
		public AccountPostResponseObj(string companyId)
		{
			CompanyId = companyId;
		}

		public AccountPostResponseObj() { }

		[JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
		public string CompanyId { get; set; }
		[JsonIgnore]
        public IEnumerable<AccountPostRequest> Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
		AccountPostRequest IEntityResponse<AccountPostRequest>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

	[JsonObject(Description = "netterm")]
	public class NetTerm
    {
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string CustomerId { get; set; }

		[JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
		public Switch Enabled { get; set; }

		[JsonProperty("term", NullValueHandling = NullValueHandling.Ignore)]
		public string Term { get; set; }

		[JsonProperty("credit", NullValueHandling = NullValueHandling.Ignore)]
		public string Credit { get; set; }

		[JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
		public string Detail { get; set; }

		[JsonProperty("company_location_id", NullValueHandling = NullValueHandling.Ignore)]
		public string CompanyLocationId { get; set; }

		[JsonProperty("disable_past_due", NullValueHandling = NullValueHandling.Ignore)]
		public Switch DisablePastDue { get; set; }

		[JsonProperty("disable_past_due_day", NullValueHandling = NullValueHandling.Ignore)]
		public Switch DisablePastDueDay { get; set; }

	}

	[JsonObject(Description = "payment_methods")]
	public class APIMethod
	{
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
		public string Method { get; set; }

		[JsonProperty("company_location_id", NullValueHandling = NullValueHandling.Ignore)]
		public string CompanyLocationId { get; set; }
	}

	#endregion Helper classes
}
