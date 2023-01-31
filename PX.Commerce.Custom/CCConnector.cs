using CommonServiceLocator;
using Newtonsoft.Json;
using PX.Commerce.Core;
using PX.Commerce.Custom.API;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
	#region CCConnectorFactory
	public class CCConnectorFactory : BaseConnectorFactory<CCConnector>, IConnectorFactory
	{
		public override string Type => CCConnector.TYPE;
		public override string Description => CCConnector.NAME;
		public override bool Enabled => true;	

		public CCConnectorFactory(IEnumerable<IProcessorsFactory> processors)
			: base(processors)
		{
			
		}

		public IConnectorDescriptor GetDescriptor()
		{
			return new ZYConnectorDescriptor(_processors.Values.ToList());
		}
	}
	#endregion

	#region CCProcessorsFactory
	public class CCProcessorsFactory : IProcessorsFactory
	{
		public string ConnectorType => CCConnector.TYPE;

		public IEnumerable<KeyValuePair<Type, Int32>> GetProcessorTypes()
		{

			yield return new KeyValuePair<Type, int>(typeof(CCCustomerProcessor), 10);
			yield return new KeyValuePair<Type, int>(typeof(CCAccountProcessor), 20);
			yield return new KeyValuePair<Type, int>(typeof(CCLocationProcessor), 30);
			yield return new KeyValuePair<Type, int>(typeof(CCProductCategoryProcessor), 40);
			yield return new KeyValuePair<Type, int>(typeof(CCStockItemProcessor), 50);
			yield return new KeyValuePair<Type, int>(typeof(CCTemplateItemProcessor), 60);
			yield return new KeyValuePair<Type, int>(typeof(CCImageProcessor), 70);
			yield return new KeyValuePair<Type, int>(typeof(CCAvailabilityProcessor), 80);
			yield return new KeyValuePair<Type, int>(typeof(CCPriceListProcessor), 90);
			yield return new KeyValuePair<Type, int>(typeof(CCOrderProcessor), 100);
			yield return new KeyValuePair<Type, int>(typeof(CCPaymentProcessor), 110);
			yield return new KeyValuePair<Type, int>(typeof(CCInvoiceProcessor), 120);
			yield return new KeyValuePair<Type, int>(typeof(CCShipmentProcessor), 130);

			yield break;
		}
	}
	#endregion

	public class CCConnector : BCConnectorBase<CCConnector>, IConnector
	{
		#region IConnector
		public const string TYPE = "CC";
		public const string NAME = "Custom";

		public override string ConnectorType { get => TYPE; }
		public override string ConnectorName { get => NAME; }
		public class ccConnectorType : PX.Data.BQL.BqlString.Constant<ccConnectorType>
		{
			public ccConnectorType() : base(TYPE) { }
		}

		public override void Initialise(List<EntityInfo> entities)
		{
			base.Initialise(entities);
		}

		public virtual IEnumerable<TInfo> GetExternalInfo<TInfo>(string infoType, int? bindingID)
			where TInfo : class
		{
			if (string.IsNullOrEmpty(infoType) || bindingID == null) return null;
			BCBindingCustom binding = BCBindingCustom.PK.Find(this, bindingID);
			if (binding == null) return null;

			try
			{
				List<TInfo> result = new List<TInfo>();
				return result;
			}
			catch (Exception ex)
			{
				LogError(new BCLogTypeScope(typeof(CCConnector)), ex);
			}

			return null;
		}

		#endregion

		#region Navigation
		public void NavigateExtern(ISyncStatus status)
		{
			if (status?.ExternID == null) return;

			EntityInfo info = GetEntities().FirstOrDefault(e => e.EntityType == status.EntityType);
			BCBindingCustom ccBinding = BCBindingCustom.PK.Find(this, status.BindingID);

			if (string.IsNullOrEmpty(ccBinding?.StoreUrl) || string.IsNullOrEmpty(info.URL)) return;

			string[] parts = status.ExternID.Split(new char[] { ';' });
			string url = string.Format(info.URL, parts.Length > 2 ? parts.Take(2).ToArray() : parts);
			string redirectUrl = ccBinding.StoreUrl.TrimEnd('/') + "/" + url;

			throw new PXRedirectToUrlException(redirectUrl, PXBaseRedirectException.WindowMode.New, string.Empty);
			
		}
		#endregion

		#region Process
		
		public virtual ConnectorOperationResult Process(ConnectorOperation operation, int?[] syncIDs = null)
		{
			LogInfo(operation.LogScope(), BCMessages.LogConnectorStarted, PXMessages.LocalizeNoPrefix(NAME));
			EntityInfo info = GetEntities().FirstOrDefault(e => e.EntityType == operation.EntityType);
			using (IProcessor graph = (IProcessor)PXGraph.CreateInstance(info.ProcessorType))
			{
				graph.Initialise(this, operation);
				return graph.Process(syncIDs);
			}
		}

		public DateTime GetSyncTime(ConnectorOperation operation)
		{
			BCBindingCustom binding = BCBindingCustom.PK.Find(this, operation.Binding);

			//Shopify Server Response Time
			bool up = new StoreRestDataProvider(GetRestClient(binding)).Check();
	
			DateTime syncTime = default(DateTime);
			syncTime = syncTime.ToUniversalTime();

			//Acumatica Time
			PXDatabase.SelectDate(out DateTime dtLocal, out DateTime dtUtc);
			syncTime = PX.Common.PXTimeZoneInfo.ConvertTimeFromUtc(dtUtc, PX.Common.LocaleInfo.GetTimeZone());

			return syncTime;
		}
        #endregion

        /// <summary>
        /// Creating client with connection parameters
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public static CCRestClient GetRestClient(BCBindingCustom binding)
		{
			return GetRestClient(binding.ApiBaseUrl, binding.ApiKey, 
				binding.ApiPassword, binding.ApiToken, binding.ApiTokenSecret, 
				RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1.ToString());
		}

		public static CCRestClient GetRestClient(String url, String key, String password, String token,
			String secret, String method)
		{
			RestOptions options = new RestOptions
			{
				BaseUri = url,
				XAuthClient = key,
				XAuthClientSecret = password,
				XAuthToken = token,
				XAuthTokenSecret = secret,
				XAuthMethod = method
			};

			JsonSerializer serializer = new JsonSerializer
			{
				MissingMemberHandling = MissingMemberHandling.Ignore,
				DateFormatHandling = DateFormatHandling.IsoDateFormat,
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				Formatting = Formatting.Indented,
				ContractResolver = new Core.REST.GetOnlyContractResolver(),
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};
			RestJsonSerializer restSerializer = new RestJsonSerializer(serializer);
			CCRestClient client = new CCRestClient(restSerializer, restSerializer, options,
				ServiceLocator.Current.GetInstance<Serilog.ILogger>()
				);

			return client;
		}

        public void ProcessHook(IEnumerable<BCExternQueueMessage> messages)
        {
            throw new NotImplementedException();
        }

        public override void StartWebHook(string baseUrl, BCWebHook hook)
        {
            throw new NotImplementedException();
        }

        public override void StopWebHook(string baseUrl, BCWebHook hook)
        {
            throw new NotImplementedException();
        }

        
    }

	#region ZYConnectorDescriptor
	public class ZYConnectorDescriptor : IConnectorDescriptor
	{
		protected IList<EntityInfo> _entities;

		public ZYConnectorDescriptor(IList<EntityInfo> entities)
		{
			_entities = entities;
		}

		/// <summary>
		/// Method to generate ID based on external API request
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
        public Guid? GenerateExternID(BCExternNotification message)
        {
			string scope = message.AdditionalInfo["X-Custom-Topic"]?.ToString();
			string bindingId = message.AdditionalInfo["bindingId"]?.ToString();

			EntityInfo info = _entities.FirstOrDefault(e => e.ExternRealtime.Supported && e.ExternRealtime.WebHookType != null && e.ExternRealtime.WebHooks.Contains(scope));

			Byte[] bytes = new Byte[16];
			BitConverter.GetBytes(CCConnector.TYPE.GetHashCode()).CopyTo(bytes, 0); //Connector
			BitConverter.GetBytes(info.EntityType.GetHashCode()).CopyTo(bytes, 4); //EntityType
			BitConverter.GetBytes(bindingId.GetHashCode()).CopyTo(bytes, 8); //Store

			return new Guid(bytes);
		}

        public virtual Guid? GenerateLocalID(BCLocalNotification message)
		{
			Guid? noteId = message.Fields.First(v => v.Key.EndsWith("NoteID", StringComparison.InvariantCultureIgnoreCase) && v.Value != null).Value.ToGuid();
			Byte[] bytes = new Byte[16];
			BitConverter.GetBytes(CCConnector.TYPE.GetHashCode()).CopyTo(bytes, 0); //Connector
			BitConverter.GetBytes(message.Entity.GetHashCode()).CopyTo(bytes, 4); //EntityType
			BitConverter.GetBytes(message.Binding.GetHashCode()).CopyTo(bytes, 8); //Store
			BitConverter.GetBytes(noteId.GetHashCode()).CopyTo(bytes, 12); //ID
			return new Guid(bytes);

		}

		public List<Tuple<String, String, String>> GetExternalFields(String type, Int32? binding, String entity)
		{
			
			List<Tuple<String, String, String>> fieldsList = new List<Tuple<string, string, string>>();
			if (entity != BCEntitiesAttribute.Customer || entity != BCEntitiesAttribute.Address) return fieldsList;

			CustomerRestDataProvider provider = new CustomerRestDataProvider(GetCCRestClient(binding));
			var fields = provider.GetAll(null);
			if (fields == null || fields.Count() <= 0) return fieldsList;
			foreach (var item in fields)
			{
				fieldsList.Add(new Tuple<string, string, string>(entity, item.Id?.ToString(), item.FirstName));
			}
			return fieldsList;
		}

		protected CCRestClient GetCCRestClient(Int32? binding)
		{
			String url = string.Empty;
			String client = string.Empty;
			String clientSecret = string.Empty;
			String token = string.Empty;
			String tokenSecret = string.Empty;

			PXGraph graph = PXGraph.CreateInstance<PXGraph>();

			var bindingsQuery = new PXSelect<BCBindingCustom,
				Where<BCBindingCustom.bindingID, Equal<Required<BCBindingCustom.bindingID>>>>(new PXGraph());
			var result = bindingsQuery.Select(binding);

			foreach (BCBindingCustom bcb in result)
			{
				url = bcb.ApiBaseUrl;
				client = bcb.ApiKey;
				clientSecret = bcb.ApiPassword;
				token = bcb.ApiToken;
				tokenSecret = bcb.ApiTokenSecret;
			}

			return CCConnector.GetRestClient(url, client, clientSecret, token, tokenSecret, RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1.ToString());
		}

	}
	#endregion


}
