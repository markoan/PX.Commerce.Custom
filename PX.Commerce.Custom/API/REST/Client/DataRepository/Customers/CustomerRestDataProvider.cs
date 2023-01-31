using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Map API Customer endpoint
    /// </summary>
    public class CustomerRestDataProvider : RestDataProviderBase, IParentRestDataProvider<ContactData>
    {
        protected override string GetListUrl { get; } = "customers/";
        protected override string GetSingleUrl { get; } = "customers/{id}";
        protected override string GetCountUrl { get; } = "customers/count";
        protected override string GetSearchUrl { get; } = "";
		protected override string PostSingleUrl { get; } = "customers/{id}";

        private string GetAccountActivationUrl { get; } = "";
        private string GetSendInviteUrl { get; } = "";
        private string GetMetafieldsUrl { get; } = "";
		protected override string PutSingleUrl { get; } = "customers/{id}";

		public CustomerRestDataProvider(ICustomRestClient restClient) : base()
        {
            _client = restClient;
        }

        #region CREATE
        public virtual string Create(ContactData entity)
		{
			//Build json to sent to Zoey API
			CustomerResponseJson cr = new CustomerResponseJson();
			cr.customerData= entity;
			var request = BuildRequest(PostSingleUrl, nameof(Method.POST));
			CustomerPostResponse result = _client.Post<CustomerResponseJson,CustomerPostResponse> (request, cr);
			
			return result.Id;
		}
        #endregion CREATE

        #region READ
		//not using
        public virtual IEnumerable<ContactData> GetCurrentList(out string previousList, out string nextList, IFilter filter = null)
		{
            previousList = null;
            nextList = null;
            //return GetCurrentList<CustomerData, CustomersResponse>(out previousList, out nextList, filter);
            return new List<ContactData>();
		}

		/*Get list of customers
		 * */
		public virtual IEnumerable<ContactData> GetAll(IFilter filter = null)
		{
			var request = BuildRequest(GetListUrl, nameof(this.Get));
			CustomersResponse response = _client.GetList<ContactData, CustomersResponse>(request);
			return response;
		}

		public virtual ContactData GetByID(string id) => GetByID(id, true, false);

		public ContactData GetByID(string customerId, bool includedMetafields = true, bool includeAllAddresses = false)
		{
			string url = GetSingleUrl.Replace("{id}", customerId);
			var request = BuildRequest(url, nameof(this.Get));
			ContactData response = _client.Get<ContactData>(request);
			return response;
		}

		public virtual ItemCount Count()
		{
			throw new NotImplementedException();
		}

		public virtual ItemCount Count(IFilter filter)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<ContactData> GetByQuery(string fieldName, string value)
		{
			var url = GetListUrl;
	
			var request = BuildRequest(url, nameof(this.GetByQuery), null, null);
			IEnumerable<ContactData> datasFiltered;
			CustomersResponse datas = _client.GetList<ContactData, CustomersResponse>(request);
			if (fieldName.Equals("Email")) 
				datasFiltered= datas.Where(d => (d.Email != null && d.Email.Equals(value)));
			else
				datasFiltered = datas.Where(d => (d.Phone != null && d.Phone.Equals(value)));
			return datasFiltered;
		}

		public List<ContactData> Get(IFilter filter = null)
		{
			throw new NotImplementedException();
		}
		#endregion READ

		#region UPDATE
		public virtual string Update(CustomerPutJson entity) => Update(entity, entity.Id.ToString());
		public virtual string Update(CustomerPutJson entity, string customerId)
		{
			var request = BuildRequest(PutSingleUrl, nameof(Method.PUT));

			UpdateCustomerResponse response = _client.Put<CustomerPutJson, UpdateCustomerResponse>(request, entity);
			if (response == null)
			{
				response = new UpdateCustomerResponse();
				response.code = HttpStatusCode.OK.ToString();
			}
			return response.code;
		}
		public ContactData Update(ContactData entity, int id)
		{
			throw new NotImplementedException();
		}
		#endregion UPDATE

		#region DELETE
		public virtual bool Delete(ContactData entity, string customerId) => Delete(customerId);

		public virtual bool Delete(string customerId)
		{
			var segments = MakeUrlSegments(customerId);
			return Delete(segments);
		}

		public bool Delete(ContactData entity, int id)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}
		#endregion DELETE

		#region HELPERS
		public virtual bool ActivateAccount(string customerId)
		{
			var request = BuildRequest(GetAccountActivationUrl, nameof(this.ActivateAccount), MakeUrlSegments(customerId), null);
			//return ZoeyRestClient.Post(request);
			return true;
		}

        ContactData IParentRestDataProvider<ContactData>.Create(ContactData entity)
        {
            throw new NotImplementedException();
        }

        #endregion HELPERS

    }
}
