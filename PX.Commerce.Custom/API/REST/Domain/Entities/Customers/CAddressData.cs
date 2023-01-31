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
	[JsonObject(Description = "Customer -> Customer Address")]
	[CommerceDescription("Contact Address")]
	public class CAddressDataAll : BCAPIEntity
	{
		[JsonProperty("entity_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.LocationId, FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
		public virtual string Id { get; set; }

		[JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.FirstName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string FirstName { get; set; }

		[JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.LastName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string LastName { get; set; }

		[JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.CompanyName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string Company { get; set; }

		[JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.City, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string City { get; set; }

		[JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.Country, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string CountryId { get; set; }

		[JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Region", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string Region { get; set; }

		[JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.PostalCode, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string PostCode { get; set; }

		[JsonProperty("telephone", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.PhoneNumber, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired(AutoDefault = true)]
		public virtual string Telephone { get; set; }

		[JsonProperty("destination_type", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Destination Type", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired(AutoDefault = true)]
		public virtual string DestinationType { get; set; }

		[JsonProperty("address_valid", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("AddressValid")]
		public virtual string AddressValid { get; set; }

		[JsonProperty("zoey_shipping_type", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("ShippingType")]
		public virtual string ShippingType { get; set; }

		[JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Street")]
		public virtual List<string> StreetArray { get; set; }

		[JsonProperty("is_default_billing", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("IsDefaultBilling")]
		public virtual int? IsDefaultBilling { get; set; }

		[JsonProperty("is_default_shipping", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("AddressLine")]
		public virtual int? IsDefaultShipping { get; set; }
	}


	[JsonObject(Description = "Customer -> Customer Address")]
	[CommerceDescription("Contact Addresses")]
	public class CAddressDataSingle : BCAPIEntity
	{
		[JsonProperty("customer_address_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.LocationId, FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
		public virtual string Id { get; set; }

		[JsonProperty("address_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Shipping Address", FieldFilterStatus.Filterable, FieldMappingStatus.Import)]
		public virtual string ShippingAddressId { get; set; }

		[JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.FirstName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string FirstName { get; set; }

		[JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.LastName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string LastName { get; set; }

		[JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.CompanyName, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public virtual string Company { get; set; }

		[JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.City, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		public string City { get; set; }

		[JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Region", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired(AutoDefault = true)]
		public string Region { get; set; }

		[JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.PostalCode, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired(AutoDefault = true)]
		public virtual string PostalCode { get; set; }

		[JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.Country, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired()]
		public virtual string CountryCode { get; set; }

		[JsonProperty("telephone", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(CustomCaptions.PhoneNumber, FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired(AutoDefault = true)]
		public virtual string Phone { get; set; }

		[JsonProperty("destination_type", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Destination Type", FieldFilterStatus.Filterable, FieldMappingStatus.ImportAndExport)]
		[ValidateRequired(AutoDefault = true)]
		public virtual string DestinationType { get; set; }

		[JsonProperty("address_valid", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("AddressValid")]
		public virtual string AddressValid { get; set; }

		[JsonProperty("zoey_shipping_type", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("ShippingType")]
		public virtual string ShippingType { get; set; }

		[JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Street")]
		public virtual string Street { get; set; }

		[JsonProperty("is_default_billing", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("IsDefaultBilling")]
		public virtual bool? IsDefaultBilling { get; set; }

		[JsonProperty("is_default_shipping", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("AddressLine")]
		public virtual bool? IsDefaultShipping { get; set; }
	}
	#endregion

	#region Entity Response
	public class CustomerAddressResponse : IEntityResponse<CAddressDataSingle>
	{
		[JsonProperty("customer_address")]
		public CAddressDataSingle Data { get; set; }
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
	public class CustomerAddressesResponse : List<CAddressDataAll> { }

	[JsonConverter(typeof(JsonCustomConverter))]
	public class UpdateResponseAddress : IEntityResponse<CustomerAddressPut>
	{
		public UpdateResponseAddress(string code)
		{

		}
		public string code { get; set; }
		public CustomerAddressPut Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public UpdateResponseAddress() { }
	}

	[JsonConverter(typeof(JsonCustomConverter))]
	public class IdPostResponseAddress : IEntityResponse<CustomerAddressPost>
	{
		public IdPostResponseAddress(string id)
		{
			Id = id;
		}
		public IdPostResponseAddress() { }
		public string Id { get; set; }
		[JsonIgnore]
		public Meta Meta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		[JsonIgnore]
		CustomerAddressPost IEntityResponse<CustomerAddressPost>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
	#endregion Entity Response

	#region Request Json
	public class CustomerAddressPut
	{
		[JsonProperty("addressId", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("AddressId", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string addressId { get; set; }

		[JsonProperty("addressData", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Address", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public CAddressDataSingle addressData { get; set; }
	}

	public class CustomerAddressPost
	{
		[JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("CustomerId", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public virtual string customerId { get; set; }

		[JsonProperty("addressData", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription("Address", FieldFilterStatus.Filterable, FieldMappingStatus.Export)]
		public CAddressDataSingle addressData { get; set; }
	}
    #endregion Request Json
}
