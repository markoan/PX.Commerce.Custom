using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
	public abstract class CCLocationBaseProcessor<TGraph, TEntityBucket, TPrimaryMapped> : BCProcessorSingleBase<TGraph, TEntityBucket, TPrimaryMapped>
		  where TGraph : PXGraph
		  where TEntityBucket : class, IEntityBucket, new()
		  where TPrimaryMapped : class, IMappedEntity, new()
	{
		
		protected CCLocationProcessor locationProcessor;
		protected CustomerRestDataProvider customerDataProvider;
		protected AccountRestDataProvider accountDataProvider;
		protected IChildRestDataProvider<LocationData> locationDataProvider;

		protected List<Tuple<String, String, String>> formFieldsList;

		protected BCBinding currentBinding;
		protected CCRestClient client;
		public PXSelect<State, Where<State.name, Equal<Required<State.name>>,
			Or<State.stateID, Equal<Required<State.stateID>>>>> states;
		public CCHelper helper = PXGraph.CreateInstance<CCHelper>();


		public override void Initialise(IConnector iconnector, ConnectorOperation operation)
		{
			base.Initialise(iconnector, operation);
			currentBinding = GetBinding();
			client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());
			locationDataProvider = new LocationRestDataProvider(client);
			customerDataProvider = new CustomerRestDataProvider(client);
			accountDataProvider = new AccountRestDataProvider(client);
		}

		protected virtual CustomerLocation MapLocationImport(LocationData locationObj, LocationData addressObj, MappedCustomer customerObj)
		{
            throw new NotImplementedException();

        }

		protected virtual LocationData MapLocationExport(MappedLocation locationObj, MappedCustomer customerObj, AccountData account = null)
		{
            throw new NotImplementedException();


        }
    }
}
