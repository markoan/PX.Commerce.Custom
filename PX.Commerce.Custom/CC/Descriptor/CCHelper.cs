using PX.Api.ContractBased.Models;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PX.Objects.PM.GroupTypes;

namespace PX.Commerce.Custom
{
	public class CCHelper : CommerceHelper
	{
		public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.eMail, Equal<Required<PX.Objects.CR.Contact.eMail>>,
			And<PX.Objects.CR.Contact.contactType, Equal<Required<PX.Objects.CR.Contact.contactType>>>>> ContactsByEmail;
		public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.phone1, Equal<Required<PX.Objects.CR.Contact.phone1>>,
			Or<PX.Objects.CR.Contact.phone2, Equal<Required<PX.Objects.CR.Contact.phone2>>>>> ContactsByPhone;


	}
}
