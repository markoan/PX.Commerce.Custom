using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PX.Commerce.Core;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
	public class BCStoreMaint : Objects.BCStoreMaint
	{
		//Getting current store 
		public new PXSelect<BCBindingCustom, Where<BCBindingCustom.bindingID, 
			Equal<Current<BCBinding.bindingID>>>> CurrentBinding;

		public PXSelect<INCategory> Categories;

		public BCStoreMaint()
		{
			base.Bindings.WhereAnd<Where<BCBinding.connectorType, Equal<CCConnector.ccConnectorType>>>();
		}

		#region Actions
		//Testing connection to the REST API 
		public PXAction<BCBinding> TestConnection;
		[PXButton]
		[PXUIField(DisplayName = "Test Connection", Enabled = false)]
		protected virtual IEnumerable testConnection(PXAdapter adapter)
		{
			Actions.PressSave();

			//Getting current store connection data 
			BCBinding binding = Bindings.Current;
			BCBindingCustom currentBinging = CurrentBinding.Current ?? CurrentBinding.Select();

			if (binding.ConnectorType != CCConnector.TYPE) return adapter.Get();

			//Validate if all required connection parameters are null or empty 
			if (binding == null || currentBinging == null || currentBinging.ApiBaseUrl == null
				|| string.IsNullOrEmpty(currentBinging.ApiKey) || string.IsNullOrEmpty(currentBinging.ApiPassword))
			{
				throw new PXException(BCMessages.TestConnectionFailedParameters);
			}

			PXLongOperation.StartOperation(this, delegate
			{
                BCStoreMaint graph = PXGraph.CreateInstance<BCStoreMaint>();
				graph.Bindings.Current = binding;
				graph.CurrentBinding.Current = currentBinging;
				
				//Creating the client
				StoreRestDataProvider restClient = new StoreRestDataProvider(CCConnector.GetRestClient(currentBinging));
				
				try
				{
					//Making request to connect to 
					StoreData data = restClient.Get();

					if (data == null)
						throw new Exception(ConnectorMessages.StoreNotFound);

					//Assign response store data to graph
					currentBinging.StoreName = data?.StoreAddressInfo.Name;
					currentBinging.DefaultCurrency = data?.BaseCurrency;
					currentBinging.StoreTimeZone = data?.Timezone;

					graph.CurrentBinding.Cache.SetValueExt(binding, nameof(BCBindingCustom.storeName),data?.StoreAddressInfo.Name);
					graph.CurrentBinding.Cache.SetValueExt(binding, nameof(BCBindingCustom.DefaultCurrency), data?.BaseCurrency);
					graph.CurrentBinding.Cache.SetValueExt(binding, nameof(BCBindingCustom.StoreTimeZone), data?.Timezone);
					graph.CurrentBinding.Update(currentBinging);

					graph.Persist();
				}
				catch (Exception ex)
				{
					//throw new PXException(ex, BCMessages.TestConnectionFailedGeneral, ex.Message);
					throw;
				}
			});

			return adapter.Get();
		}
		#endregion

		#region BCBinding Events


		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXCustomizeBaseAttribute(typeof(BCConnectorsAttribute), "DefaultConnector", CCConnector.TYPE)]
		public virtual void _(Events.CacheAttached<BCBinding.connectorType> e) { }

		public override void _(Events.RowSelected<BCBinding> e)
		{
			base._(e);

			BCBinding row = e.Row as BCBinding;
			if (row == null) return;

			//Actions
			TestConnection.SetEnabled(row.BindingID > 0 && row.ConnectorType == CCConnector.TYPE);
		}
		public override void _(Events.RowInserted<BCBinding> e)
		{
			base._(e);

			bool dirty = CurrentBinding.Cache.IsDirty;
			CurrentBinding.Insert();
			CurrentBinding.Cache.IsDirty = dirty;
		}

		public virtual void _(Events.FieldVerifying<BCBindingCustom, BCBindingCustom.apiBaseUrl> e)
		{
			string val = e.NewValue?.ToString();
			if (val != null)
			{
				val = val.TrimEnd('/');
				for (int i = 0; i < 10; i++)
				{
					string pattern = "/v" + i;
					if (val.EndsWith(pattern)) val = val.Substring(0, val.LastIndexOf(pattern) + 1);
				}
				if (!val.EndsWith("/")) val += "/";

				e.NewValue = val;
			}
		}

	
		public override void _(Events.FieldUpdated<BCEntity, BCEntity.isActive> e)
		{
			base._(e);

			BCEntity row = e.Row;
			if (row == null || row.CreatedDateTime == null) return;

			if (row.IsActive == true)
			{
				if (row.EntityType == BCEntitiesAttribute.ProductWithVariant)
					if (PXAccess.FeatureInstalled<FeaturesSet.matrixItem>() == false)
					{
						EntityReturn(row.EntityType).IsActive = false;
						e.Cache.Update(EntityReturn(row.EntityType));
						throw new PXSetPropertyException(BCMessages.MatrixFeatureRequired);
					}
			}
		}
		#endregion
	}
}
