using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	public interface ICustomRestClient
	{
		RestRequest MakeRequest(string url, Dictionary<string, string> urlSegments = null);


		ILogger Logger { get; set; }
		T Post<T>(IRestRequest request, T obj) where T : class, new();
		T Put<T>(IRestRequest request, T obj) where T : class, new();
		bool Delete(IRestRequest request);

		TE Get<T, TE>(IRestRequest request)
			where T : class, new()
			where TE : IEntityResponse<T>, new();

		T GetById<T, TR>(IRestRequest request)
		where T : class, new()
		where TR : IEntityResponse<T>, new();
		TE GetList<T, TE>(IRestRequest request)
			where T : class, new()
			where TE : IEnumerable<T>, new();

		TE Post<T, TE>(IRestRequest request, T entity) where T : class, new() where TE : IEntityResponse<T>, new();
        T Get<T>(RestRequest request);
		IEnumerable<T> GetAll<T, TR>(IRestRequest request) where T : class, new() where TR : class, IEntitiesResponse<T>, new();
		TE Post<T, TE>(IRestRequest request, TE entity) where T : class, new() where TE : IEntityResponse<T>, new();
		TE Post<T, TE>(IRestRequest request, List<T> entities) where T : class, new() where TE : IEntitiesResponse<T>, new();
		TE Put<T, TE>(IRestRequest request, T entity) where T : class, new() where TE : IEntityResponse<T>, new();
		TE Put<T, TE>(IRestRequest request, TE entity) where T : class, new() where TE : IEntityResponse<T>, new();
		TE Put<T, TE>(IRestRequest request, List<T> entities) where T : class, new() where TE : IEntitiesResponse<T>, new();
		TE PutList<T, TE>(IRestRequest request, TE entity) where T : class, new() where TE : IEntitiesResponse<T>, new();
		

	}
}
