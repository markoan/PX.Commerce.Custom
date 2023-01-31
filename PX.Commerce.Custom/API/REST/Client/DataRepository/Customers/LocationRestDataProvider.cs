using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	/// <summary>
	///Class to CRUD account addresses between Acumatica and Zoey
	/// </summary>
	public class LocationRestDataProvider : RestDataProviderBase, IChildRestDataProvider<LocationData>
	{
		protected override string GetListUrl { get; } = "locations/";
		protected override string GetSingleUrl { get; } = "locations/{id}";
		protected override string GetCountUrl => throw new NotImplementedException();
		protected override string GetSearchUrl => throw new NotImplementedException();
		protected override string PostSingleUrl => "locations/{id}";
		protected override string PutSingleUrl => "locations/{id}";

		public LocationRestDataProvider(ICustomRestClient restClient) : base()
		{
			_client = restClient;
		}

        #region CREATE
        public virtual LocationData Create(LocationData entity, string accountId)
		{
			AccountPutRequest account = new AccountPutRequest();
			AccountData data = new AccountData();

			var createRequest = BuildRequest(PostSingleUrl, nameof(Method.PUT));

			UpdateAccountResponse updateResult = _client.Put<AccountPutRequest, UpdateAccountResponse>(createRequest, account);

			return entity;
		}
        #endregion CREATE
		
        #region READ
		public virtual int Count(string accountId)
		{
            throw new NotImplementedException();
        }

		public virtual IEnumerable<LocationData> GetCurrentList(string customerId, out string previousList, out string nextList, IFilter filter = null)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<LocationData> GetAll(string accountId, IFilter filter = null)
		{
            throw new NotImplementedException();
        }

        public virtual LocationData GetByID(string accountId, string locationId)
		{
            throw new NotImplementedException();

        }

        public virtual IEnumerable<LocationData> GetAllWithoutParent(IFilter filter = null)
		{
			throw new NotImplementedException();
		}
        #endregion READ

        #region UPDATE
        public virtual LocationData Update(LocationData entity, string accountId, string locationId)
		{
            throw new NotImplementedException();

        }
        #endregion UPDATE

        #region DELETE
        public virtual bool Delete(string accountId, string locationId)
		{
            throw new NotImplementedException();
        }

        LocationData IChildRestDataProvider<LocationData>.Create(LocationData entity, string parentId)
        {
			return this.Create(entity, parentId);
		}

		LocationData IChildRestDataProvider<LocationData>.Update(LocationData entity, string parentId, string id)
        {
			return this.Update(entity, parentId, id);
		}

		IEnumerable<LocationData> IChildReadOnlyRestDataProvider<LocationData>.GetAll(string parentId, IFilter filter)
        {
			return this.GetAll(parentId, filter);

        }
        #endregion DELETE
    }
}
