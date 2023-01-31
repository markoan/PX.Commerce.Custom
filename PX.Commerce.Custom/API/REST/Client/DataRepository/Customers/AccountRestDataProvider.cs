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
    /// Map API Accounts endpoint
    /// </summary>
    public class AccountRestDataProvider : RestDataProviderBase, IParentRestDataProvider<AccountData>
    {
        protected override string GetListUrl => "accounts/list";
        protected override string GetSingleUrl => "accounts/{id}";
        protected override string GetCountUrl => "accounts/count";
        protected override string GetSearchUrl => throw new NotImplementedException();
        protected override string PostSingleUrl => "accounts/{id}";
        protected override string PutSingleUrl => "accounts/{id}";

        public AccountRestDataProvider(ICustomRestClient restClient) : base()
        {
            _client = restClient;
        }

        public ItemCount Count()
        {
            throw new NotImplementedException();
        }

        public ItemCount Count(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public AccountData Create(AccountData entity)
        {
            AccountPostRequest account = new AccountPostRequest();
            account.accountData = entity;
            var request = BuildRequest(PostSingleUrl, nameof(Method.POST));

            return _client.Post<AccountPostRequest, AccountPostResponse>(request, account).Data;
        }

        public bool Delete(AccountData entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountData> Get(IFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountData> GetAll(IFilter filter = null)
        {
            var request = BuildRequest(GetListUrl, nameof(this.Get));
            AccountsResponse response = _client.GetList<AccountData, AccountsResponse>(request);

            return response;
        }

        public AccountData GetByID(string id)
        {
            var request = BuildRequest(GetSingleUrl.Replace("{id}",id), nameof(Method.POST));
            AccountData result = _client.Get<AccountData>(request);

            return result;
        }

        public AccountData Update(AccountData entity, string id)
        {
            AccountPutRequest account = new AccountPutRequest ();

            account.CompanyId = id;
            account.CompanyData = entity;
            var request = BuildRequest(PostSingleUrl, nameof(Method.PUT));
            UpdateAccountResponse result = _client.Put<AccountPutRequest, UpdateAccountResponse>(request, account);

            // Get account
            return GetByID(id);
        }

        AccountData IParentRestDataProvider<AccountData>.Create(AccountData entity)
        {
            throw new NotImplementedException();
        }

        public AccountData Update(AccountData entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
