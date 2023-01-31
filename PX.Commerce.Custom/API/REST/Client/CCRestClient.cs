using AutoMapper;
using Newtonsoft.Json;
using PX.Commerce.Core;
using PX.Common;
using PX.Data;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
	public class CCRestClient : CCRestClientBase, ICustomRestClient
	{
		public CCRestClient(IDeserializer deserializer, ISerializer serializer, IRestOptions options, Serilog.ILogger logger) :
			base(deserializer, serializer, options, logger) { }
		#region CREATE
		public T Post<T>(IRestRequest request, T obj)
			where T : class, new()
		{
			request.Method = Method.POST;
			request.AddJsonBody(obj);
			var response = Execute<T>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
			{
				
				T result = response.Data;

				if (result != null && result is BCAPIEntity) (result as BCAPIEntity).JSON = response.Content;

				return result;
			}

			LogError(BaseUrl, request, response);
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new Exception($"Cannot insert {obj.GetType().Name}");
			}

			throw new RestException(response);
		}

		public TE Post<T, TE>(IRestRequest request, T entity)
	   where T : class, new()
	   where TE : IEntityResponse<T>, new()
		{
			request.Method = Method.POST;
			request.AddJsonBody(entity);

			IRestResponse<TE> response = Execute<TE>(request);

			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
			{
				TE result = response.Data;

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
		public TE Post<T, TE>(IRestRequest request, List<T> entities)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			request.Method = Method.POST;
			request.AddJsonBody(entities);
			IRestResponse<TE> response = Execute<TE>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
			{
				TE result = response.Data;

				if (result?.Data != null && result?.Data is IEnumerable<BCAPIEntity>) (result?.Data as IEnumerable<BCAPIEntity>).ForEach(e => e.JSON = response.Content);

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
		public TE Post<T, TE>(IRestRequest request, TE entity)
			where T : class, new()
			where TE : IEntityResponse<T>, new()
		{
			request.Method = Method.POST;
			request.AddJsonBody(entity.Data);
			var response = Execute<TE>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
			{
				TE result = response.Data;

				if (result?.Data != null && result?.Data is BCAPIEntity) (result?.Data as BCAPIEntity).JSON = response.Content;

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
		#endregion CREATE

		#region READ
		public T Get<T>(IRestRequest request, out object headers)
			where T : class, new()
		{
			request.Method = Method.GET;
			var response = Execute<T>(request);

			if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
			{
				T result = response.Data;

				if (result != null && result is BCAPIEntity) (result as BCAPIEntity).JSON = response.Content;
				headers = null;

				return result;
			}

			LogError(BaseUrl, request, response);

			if (response.StatusCode == HttpStatusCode.InternalServerError && string.IsNullOrEmpty(response.Content))
			{
				throw new Exception(CustomCaptions.ErrorCode);
			}
			throw new RestException(response);
		}

		public TE GetList<T, TE>(IRestRequest request)
			where T : class, new()
			where TE : IEnumerable<T>, new()
		{
			request.Method = Method.GET;
			var response = Execute<TE>(request);

			if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
			{
				TE result = response.Data;

				if (result != null && result is IEnumerable<BCAPIEntity>) (result as IEnumerable<BCAPIEntity>).ForEach(e => e.JSON = response.Content);

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}

		T ICustomRestClient.Get<T>(RestRequest request)
		{
			request.Method = Method.GET;
			var response = Execute<T>(request);

			if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent) && response.ErrorException == null)
			{
				T result = JsonConvert.DeserializeObject<T>(response.Content);
				
				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}


		public T Get<T>(RestRequest request)
		{
			request.Method = Method.GET;
			var response = Execute<T>(request);

			if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
			{
				T result = response.Data;

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}

		public IEnumerable<T> GetAll<T, TR>(IRestRequest request) where T : class, new() where TR : class, IEntitiesResponse<T>, new()
		{
			request.Method = Method.GET;
			bool needGet = true;
			while (needGet)
			{
				var response = Execute<TR>(request);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					var responseHeader = response.Headers;
					var entities = response.Data?.Data;
					if (entities == null && response.ErrorException != null)
					{
						LogError(BaseUrl, request, response);
						throw response.ErrorException;
					}
					if (entities != null && entities.Any())
					{
						foreach (T entity in entities)
						{
							yield return entity;
						}
				
						needGet = false;
					}
					else
						yield break;
				}
				else
				{
					LogError(BaseUrl, request, response);
					throw new RestException(response);
				}
			}
		}

		public TE Get<T, TE>(IRestRequest request)
			   where T : class, new()
			   where TE : IEntityResponse<T>, new()
		{
			throw new NotImplementedException();
		}

		public T GetById<T, TR>(IRestRequest request)
			where T : class, new() where TR : IEntityResponse<T>, new()
		{
			request.Method = Method.GET;
			var response = Execute<TR>(request);
			if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound)
			{
				T result = JsonConvert.DeserializeObject<T>(response.Content);

				if (result == null && response.ErrorException != null)
				{
					LogError(BaseUrl, request, response);
					throw response.ErrorException;
				}
				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}

		#endregion	READ

		#region UPDATE 
		public T Put<T>(IRestRequest request, T obj)
			where T : class, new()
		{
			request.Method = Method.PUT;
			request.AddJsonBody(obj);

			var response = Execute<T>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
			{
				T result = response.Data;

				if (result != null && result is BCAPIEntity) (result as BCAPIEntity).JSON = response.Content;

				return result;
			}

			LogError(BaseUrl, request, response);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				throw new Exception($"Cannot update {obj.GetType().Name}");
			}

			throw new RestException(response);
		}


		public TE Put<T, TE>(IRestRequest request, T entity)
		where T : class, new()
		where TE : IEntityResponse<T>, new()
		{
			request.Method = Method.PUT;
			request.AddJsonBody(entity);

			var response = Execute<TE>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
			{
				TE result = response.Data;

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
		public TE Put<T, TE>(IRestRequest request, List<T> entities)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			request.Method = Method.PUT;
			request.AddJsonBody(entities);

			var response = Execute<TE>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
			{
				TE result = response.Data;

				if (result?.Data != null && result?.Data is IEnumerable<BCAPIEntity>) (result?.Data as IEnumerable<BCAPIEntity>).ForEach(e => e.JSON = response.Content);

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
		public TE Put<T, TE>(IRestRequest request, TE entity)
			where T : class, new()
			where TE : IEntityResponse<T>, new()
		{
			request.Method = Method.PUT;
			request.AddJsonBody(entity);

			var response = Execute<TE>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
			{
				TE result = response.Data;

				if (result?.Data != null && result?.Data is BCAPIEntity) (result?.Data as BCAPIEntity).JSON = response.Content;

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}

		public TE PutList<T, TE>(IRestRequest request, TE entity)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			request.Method = Method.PUT;
			request.AddJsonBody(entity.Data);

			var response = Execute<TE>(request);
			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
			{
				TE result = response.Data;

				return result;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
        #endregion UPDATE

        #region DELETE
        public bool Delete(IRestRequest request)
		{
			request.Method = Method.DELETE;
			var response = Execute(request);
			if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
			{
				return true;
			}

			LogError(BaseUrl, request, response);
			throw new RestException(response);
		}
        #endregion DELETE

    }
}
