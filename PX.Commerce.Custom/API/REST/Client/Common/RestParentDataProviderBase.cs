using PX.Commerce.Core;
using PX.Common;
using PX.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Parent class for all entities rest providers
    /// </summary>
    public abstract class RestDataProviderBase
    {
		protected const int BATCH_SIZE = 10;
		protected const string ID_STRING = "id";
		protected const string PARENT_ID_STRING = "parent_id";
		protected const string OTHER_PARAM = "other_param";
		protected const string ApiPrefix = "api/rest";
		protected readonly int commerceRetryCount = WebConfig.GetInt(BCConstants.COMMERCE_RETRY_COUNT, 3);
		protected ICustomRestClient _client;

		protected abstract string GetListUrl { get; }
		protected abstract string GetSingleUrl { get; }
		protected abstract string GetCountUrl { get; }
		protected abstract string GetSearchUrl { get; }
		protected abstract string PostSingleUrl { get; }
		protected abstract string PutSingleUrl { get; }

		public RestDataProviderBase() { }

		#region CREATE
		public virtual T Create<T>(T entity, UrlSegments urlSegments = null) where T : class, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", entity)
				.Verbose("{CommerceCaption}: REST API - Creating new {EntityType} entity with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			String.Format("REST API - Creating new {0} entity with parameters {1}", typeof(T).ToString(), urlSegments?.ToString() ?? "none");
			var request = BuildRequest(PostSingleUrl, nameof(Method.POST));

			return _client.Post(request, entity);
		}

		/*
		 * Used for: Products creation
		 * */
		public virtual TE Create<T, TE>(T entity, UrlSegments urlSegments = null)
			where T : class, new()
			where TE : IEntityResponse<T>, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", entity)
				.Verbose("{CommerceCaption}: REST API - Creating new {EntityType} entity with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			int retryCount = 0;
			while (true)
			{
				try
				{
					//Build url and make request
					var request = BuildRequest(PostSingleUrl, nameof(Method.POST));
					PXTrace.WriteInformation("Request: "+Newtonsoft.Json.JsonConvert.SerializeObject(request));

					TE result = _client.Post<T, TE>(request, entity);

					return result;
				}
				catch (RestException ex)
				{
					PXTrace.WriteError("Error occured while creating entity in External App: "+Newtonsoft.Json.JsonConvert.SerializeObject(ex));
					//If error occurs or retry count reached the limit
					if (ex?.ResponseStatusCode == default(HttpStatusCode).ToString() && retryCount < commerceRetryCount)
					{
						_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
							.Error("{CommerceCaption}: Operation failed, RetryCount {RetryCount}, Exception {ExceptionMessage}",
							BCCaptions.CommerceLogCaption, retryCount, ex?.ToString());

						retryCount++;
						Thread.Sleep(1000 * retryCount);
					}
					else throw;
				}
			}
		}
		
		public virtual TE Create<T, TE>(List<T> entities, UrlSegments urlSegments = null)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", entities)
				.Verbose("{CommerceCaption}: REST API - Creating new {EntityType} entity with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			int retryCount = 0;
			while (true)
			{
				try
				{
					var request = _client.MakeRequest(GetListUrl, urlSegments?.GetUrlSegments());

					TE result = _client.Post<T, TE>(request, entities);

					return result;
				}
				catch (RestException ex)
				{
					if (ex?.ResponseStatusCode == default(HttpStatusCode).ToString() && retryCount < commerceRetryCount)
					{
						_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
							.Error("{CommerceCaption}: Operation failed, RetryCount {RetryCount}, Exception {ExceptionMessage}",
							BCCaptions.CommerceLogCaption, retryCount, ex?.ToString());

						retryCount++;
						Thread.Sleep(1000 * retryCount);
					}
					else throw;
				}
			}
		}
		#endregion

		#region READ
		/*Used to retrieve list of entities
		 * */
		public virtual IEnumerable<T> GetAll<T, TR>(IFilter filter = null, UrlSegments urlSegments = null) where T : class, new() where TR : class, IEntitiesResponse<T>, new()
		{
			var request = BuildRequest(GetListUrl, nameof(this.GetAll), urlSegments, filter);
			var entities = _client.GetAll<T, TR>(request);
			return entities;
		}

		/*Used to get entity by id
		 * */
		public virtual T GetByID<T, TR>(UrlSegments urlSegments) where T : class, new() where TR : class, IEntityResponse<T>, new()
		{
			var request = BuildRequest(GetSingleUrl, nameof(this.GetByID), urlSegments, null);
			var entity = _client.GetById<T, TR>(request);
			return entity;
		}

		#endregion READ

		#region UPDATE 
		public virtual T Update<T>(T entity, UrlSegments urlSegments)
			where T : class, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", entity)
				.Verbose("{CommerceCaption}: REST API - Updating {EntityType} entity with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			int retryCount = 0;
			while (true)
			{
				try
				{
					var request = _client.MakeRequest(GetSingleUrl, urlSegments.GetUrlSegments());

					T result = _client.Put(request, entity);

					return result;
				}
				catch (RestException ex)
				{
					if (ex?.ResponseStatusCode == default(HttpStatusCode).ToString() && retryCount < commerceRetryCount)
					{
						_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
							.Error("{CommerceCaption}: Operation failed, RetryCount {RetryCount}, Exception {ExceptionMessage}",
							BCCaptions.CommerceLogCaption, retryCount, ex?.ToString());

						retryCount++;
						Thread.Sleep(1000 * retryCount);
					}
					else throw;
				}
			}
		}
		public virtual TE Update<T, TE>(T entity, UrlSegments urlSegments=null)
			where T : class, new()
			where TE : IEntityResponse<T>, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", entity)
				.Verbose("{CommerceCaption}: REST API - Updating {EntityType} entity with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			int retryCount = 0;
			while (true)
			{
				try
				{

					var request = BuildRequest(PutSingleUrl, nameof(Method.PUT));

					TE result = _client.Put<T, TE>(request, entity);

					return result;
				}
				catch (RestException ex)
				{
					if (ex?.ResponseStatusCode == default(HttpStatusCode).ToString() && retryCount < commerceRetryCount)
					{
						_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
							.Error("{CommerceCaption}: Operation failed, RetryCount {RetryCount}, Exception {ExceptionMessage}",
							BCCaptions.CommerceLogCaption, retryCount, ex?.ToString());

						retryCount++;
						Thread.Sleep(1000 * retryCount);
					}
					else throw;
				}
			}
		}
		public virtual TE Update<T, TE>(List<T> entities, UrlSegments urlSegments = null)
			where T : class, new()
			where TE : class, IEntitiesResponse<T>, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", entities)
				.Verbose("{CommerceCaption}: REST API - Updating {EntityType} entity with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			var request = _client.MakeRequest(GetSingleUrl, urlSegments?.GetUrlSegments());

			return _client.Put<T, TE>(request, entities);
		}
        #endregion UPDATE

        #region DELETE 
        public virtual bool Delete(UrlSegments urlSegments)
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.Verbose("{CommerceCaption}: REST API - Deleting {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, this.GetType().ToString(), urlSegments?.ToString() ?? "none");

			int retryCount = 0;
			while (true)
			{
				try
				{
					var request = _client.MakeRequest(GetSingleUrl, urlSegments.GetUrlSegments());

					var result = _client.Delete(request);

					return result;
				}
				catch (RestException ex)
				{
					if (ex?.ResponseStatusCode == default(HttpStatusCode).ToString() && retryCount < commerceRetryCount)
					{
						_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
							.Error("{CommerceCaption}: Operation failed, RetryCount {RetryCount}, Exception {ExceptionMessage}",
							BCCaptions.CommerceLogCaption, retryCount, ex?.ToString());

						retryCount++;
						Thread.Sleep(1000 * retryCount);
					}
					else throw;
				}
			}
		}
        #endregion DELETE

        #region HELPERS
        protected static UrlSegments MakeUrlSegments(string id)
		{
			var segments = new UrlSegments();
			segments.Add(ID_STRING, id);
			return segments;
		}

		protected static UrlSegments MakeParentUrlSegments(string parentId)
		{
			var segments = new UrlSegments();
			segments.Add(PARENT_ID_STRING, parentId);

			return segments;
		}

		protected static UrlSegments MakeUrlSegments(string id, string parentId)
		{
			var segments = new UrlSegments();
			segments.Add(PARENT_ID_STRING, parentId);
			segments.Add(ID_STRING, id);
			return segments;
		}
		
		protected static UrlSegments MakeUrlSegments(string id, string parentId, string param)
		{
			var segments = new UrlSegments();
			segments.Add(PARENT_ID_STRING, parentId);
			segments.Add(ID_STRING, id);
			segments.Add(OTHER_PARAM, param);
			return segments;
		}
		
		protected virtual string BuildUrl(string url)
		{
			return ApiPrefix + "/" + url.TrimStart('/');
		}

		protected void ValidationUrl(string url, string methodName)
		{
			if (string.IsNullOrWhiteSpace(url))
				throw new PXNotSupportedException(Custom.ConnectorMessages.DataProviderNotSupportMethod, this.GetType().Name, methodName);
		}
		
		protected RestRequest BuildRequest(string url, string methodName, UrlSegments urlSegments = null, IFilter filter = null)
		{
			var builtUrl = BuildUrl(url);
			ValidationUrl(builtUrl, methodName);
			var request = _client.MakeRequest(builtUrl, urlSegments?.GetUrlSegments());
			if (filter != null)
				filter?.AddFilter(request);
			return request;
		}
        #endregion HELPERS

    }

    public abstract class RestDataProvider : RestDataProviderBase
	{
		public RestDataProvider() : base(){}

        #region READ
        public virtual ItemCount GetCount(UrlSegments urlSegments = null)
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.Verbose("{CommerceCaption}: REST API - Getting Count of {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, this.GetType().ToString(), urlSegments?.ToString() ?? "none");

			var request = _client.MakeRequest(BuildUrl(GetCountUrl), urlSegments?.GetUrlSegments());

			var count = _client.Get<ItemCount>(request);
			return count;
		}

		public virtual ItemCount GetCount(IFilter filter, UrlSegments urlSegments = null)
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.Verbose("{CommerceCaption}: REST API - Getting Count of {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, this.GetType().ToString(), urlSegments?.ToString() ?? "none");

			var request = _client.MakeRequest(BuildUrl(GetCountUrl), urlSegments?.GetUrlSegments());
			filter?.AddFilter(request);

			var count = _client.Get<ItemCount>(request);
			return count;
		}

		public virtual List<T> Get<T>(IFilter filter = null, UrlSegments urlSegments = null)
			where T : class, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.Verbose("{CommerceCaption}: REST API - Getting of {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, this.GetType().ToString(), urlSegments?.ToString() ?? "none");

			var request = _client.MakeRequest(BuildUrl(GetListUrl), urlSegments?.GetUrlSegments());
			filter?.AddFilter(request);

			var entity = _client.Get<List<T>>(request);
			return entity;
		}

		public virtual IEnumerable<T> GetAll<T>(IFilterWithLimit filter = null, UrlSegments urlSegments = null)
			where T : class, new()
		{
			var localFilter = filter ?? new FilterWithLimit();
			var needGet = true;

			localFilter.Page = 1;
			localFilter.Limit = 100;

			while (needGet)
			{
				List<T> entities = Get<T>(localFilter, urlSegments);

				if (entities == null) yield break;
				foreach (T entity in entities)
				{
					yield return entity;
				}
				localFilter.Page++;
				needGet = localFilter.Limit == entities.Count;
			}
		}

		public virtual T GetByID<T>(UrlSegments urlSegments) where T : class, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.Verbose("{CommerceCaption}: REST API - Getting by ID {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, this.GetType().ToString(), urlSegments?.ToString() ?? "none");

			var request = _client.MakeRequest(BuildUrl(GetSingleUrl), urlSegments.GetUrlSegments());

			var entity = _client.Get<T>(request);

			return entity;
		}
		#endregion READ


		public virtual TE UpdateAll<T, TE>(TE entities, UrlSegments urlSegments = null)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", APIHelper.DesctructObject(entities), destructureObjects: true)
				.Verbose("{CommerceCaption}: REST API - Updating of {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			var request = _client.MakeRequest(BuildUrl(GetSingleUrl), urlSegments.GetUrlSegments());

			return _client.PutList<T, TE>(request, entities);
		}
		public virtual void UpdateAll<T, TE>(TE entities, UrlSegments urlSegments, Action<ItemProcessCallback<T>> callback)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			TE batch = new TE();
			batch.Meta = entities.Meta;

			int index = 0;
			for (; index < entities.Data.Count(); index++)
			{
				if (index % BATCH_SIZE == 0 && batch.Data.Count() > 0)
				{
					UpdateBatch<T, TE>(batch, urlSegments, index - batch.Data.Count, callback);

					batch.Data.Clear();
				}
				batch.Data.Add(entities.Data[index]);
			}
			if (batch.Data.Count > 0)
			{
				UpdateBatch<T, TE>(batch, urlSegments, index - batch.Data.Count, callback);
			}
		}

		protected void UpdateBatch<T, TE>(TE batch, UrlSegments urlSegments, Int32 startIndex, Action<ItemProcessCallback<T>> callback)
			where T : class, new()
			where TE : IEntitiesResponse<T>, new()
		{
			_client.Logger?.ForContext("Scope", new BCLogTypeScope(GetType()))
				.ForContext("Object", APIHelper.DesctructObject(batch), destructureObjects: true)
				.Verbose("{CommerceCaption}: REST API - Batch Updating of {EntityType} entry with parameters {UrlSegments}", BCCaptions.CommerceLogCaption, typeof(T).ToString(), urlSegments?.ToString() ?? "none");

			while (true)
				try
				{
					RestRequest request = _client.MakeRequest(BuildUrl(GetListUrl), urlSegments.GetUrlSegments());

					TE response = _client.PutList<T, TE>(request, batch);
					if (response == null) return;
					for (int i = 0; i < response.Data.Count; i++)
					{
						T item = response.Data[i];
						callback(new ItemProcessCallback<T>(startIndex + i, item));
					}
					break;
				}
				catch (RestException ex)
				{
					if (ex?.ResponseStatusCode == default(HttpStatusCode).ToString())
					{
						throw;
					}
					else
					{
						for (int i = 0; i < batch.Data.Count; i++)
						{
							T item = batch.Data[i];
							callback(new ItemProcessCallback<T>(startIndex + i, ex, batch.Data));
						}
						break;
					}
				}
		}
	}
}
