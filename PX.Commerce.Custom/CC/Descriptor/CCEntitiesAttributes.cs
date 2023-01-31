using PX.Commerce.Core;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom
{
	public class CCEntitiesAttributes : BCEntitiesAttribute
	{
		public const string Account = "AC";
		public const string Product = "PR";
		public const string Contact = "CN";
		public const string SalesPerson = "SP";


		public CCEntitiesAttributes(Type connectorType, Type bindingType, Modes mode) : base(connectorType, 
			bindingType, mode) {
		}

		
		public sealed class account : PX.Data.BQL.BqlString.Constant<account>
		{
			public account() : base(Account)
			{
			}
		}

		public sealed class product : PX.Data.BQL.BqlString.Constant<product>
		{
			public product() : base(Product)
			{
			}
		}

		public sealed class contact : PX.Data.BQL.BqlString.Constant<contact>
		{
			public contact() : base(Contact)
			{
			}
		}

	}
}
