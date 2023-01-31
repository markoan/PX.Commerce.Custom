using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PX.Commerce.Core;
using System.ComponentModel;
using PX.Commerce.Core.API;

namespace PX.Commerce.Custom.API.REST
{
    #region Entity

    /// <summary>
    /// Declare entity as defined in the external API
    /// </summary>
    [JsonObject(Description = "customerData")]
	[CommerceDescription("ContactData")]
	public class ContactData : BCAPIEntity
	{
		/// <summary>
		/// A unique identifier for the customer
		/// </summary>
		[JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.CustomerId, FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
		public virtual long? Id { get; set; }

		/// <summary>
		/// The unique email address of the customer. Attempting to assign the same email address to multiple customers returns an error.
		/// </summary>
		[JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.EmailAddress, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired()]
		public virtual string Email { get; set; }

		[JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.PhoneNumber, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired()]
		public virtual string Phone { get; set; }

		/// <summary>
		/// The date and time (ISO 8601 format) when the customer was created.
		/// </summary>
		[JsonProperty("created_at")]
		[CommerceDescription(CustomCaptions.DateCreated, FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
		[ShouldNotSerialize]
		public virtual string DateCreatedAt { get; set; }

		/// <summary>
		/// The date and time (ISO 8601 format) when the customer was last updated.
		/// </summary>
		[JsonProperty("updated_at")]
		[ShouldNotSerialize]
		[CommerceDescription(CustomCaptions.DateModified, FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
		public virtual string DateModifiedAt { get; set; }

		/// <summary>
		/// The customer's first name.
		/// </summary>
		[JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.FirstName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired]
		public virtual string FirstName { get; set; }

		/// <summary>
		/// The customer's last name.
		/// </summary>
		[JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.LastName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired()]
		public virtual string LastName { get; set; }

		//...
	}
	#endregion Entity

    #region Entity Response
    public class CustomerResponse : IEntityResponse<ContactData>
	{
		[JsonProperty("customerData")]
		public ContactData Data { get; set; }
		[JsonIgnore()]
        public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

	public class CustomersResponse : List<ContactData> { }

	public class CustomerResponseJson
    {
		[JsonProperty("customerData")]
		public ContactData customerData { get; set; }
	}
    #endregion Entity Response

    #region Request Json
    public class CustomerPutJson
    {
		[JsonProperty("customerId")]
		public int Id { get; set; }

		[JsonProperty("customerData")]
		public ContactData Customer { get; set; }
    }

	[JsonConverter(typeof(JsonCustomConverter))]
	public class CustomerPostResponse : IEntityResponse<CustomerResponseJson>
	{
		public CustomerPostResponse(string id)
		{
			Id = id;
		}
		public CustomerPostResponse() { }
		public string Id { get; set; }
		[JsonIgnore]
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
		CustomerResponseJson IEntityResponse<CustomerResponseJson>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}

	[JsonConverter(typeof(JsonCustomConverter))]
	public class UpdateCustomerResponse : IEntityResponse<CustomerPutJson>
	{
		public UpdateCustomerResponse(string code)
		{

		}
		public string code { get; set; }
		public CustomerPutJson Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public UpdateCustomerResponse() { }
	}
    #endregion Request Json
}
