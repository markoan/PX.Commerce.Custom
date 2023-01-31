using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{ 
    public class Filter : IFilter
	{
        protected const string RFC2822_DATE_FORMAT = "{0:ddd, dd MMM yyyy HH:mm:ss} GMT";
        protected const string ISO_DATE_FORMAT = "{0:yyyy-MM-ddTHH:mm:ss}";

        [Description("page")]
        public int? Page { get; set; }

        public virtual void AddFilter(IRestRequest request)
        {
            foreach (var propertyInfo in GetType().GetProperties())
            {
                DescriptionAttribute attr = propertyInfo.GetAttribute<DescriptionAttribute>();
                if (attr == null) continue;
                String key = attr.Description;
                Object value = propertyInfo.GetValue(this);
                if (value != null)
                {
                    if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                    {
                        value = string.Format(ISO_DATE_FORMAT, value);
                    }

                    request.AddParameter(key, value);
                }
            }
        }
    }
	public class FilterWithFields : Filter, IFilterWithFields
	{
		/// <summary>
		/// Show only certain fields, specified by a comma-separated list of field names.
		/// </summary>
		[Description("fields")]
		public string Fields { get; set; }
	}


	public class FilterWithLimit : Filter, IFilterWithLimit
	{
		/// <summary>
		/// The maximum number of results to show.
		/// (default: 50, maximum: 250)
		/// </summary>
		[Description("limit")]
		public int? Limit { get; set; }

	}

	public class FilterWithDateTimeAndLimit : Filter, IFilterWithDateTime, IFilterWithLimit
	{
		/// <summary>
		/// The maximum number of results to show.
		/// (default: 50, maximum: 250)
		/// </summary>
		[Description("limit")]
		public int? Limit { get; set; }

		/// <summary>
		/// Show customers created after a specified date.
		///(format: 2014-04-25T16:15:47-04:00)
		/// </summary>
		[Description("created_at_min")]
		public DateTime? CreatedAtMin { get; set; }

		/// <summary>
		/// Show customers created before a specified date.
		///(format: 2014-04-25T16:15:47-04:00)
		/// </summary>
		[Description("created_at_max")]
		public DateTime? CreatedAtMax { get; set; }

		/// <summary>
		/// Show customers last updated after a specified date.
		///(format: 2014-04-25T16:15:47-04:00)
		/// </summary>
		[Description("updated_at_min")]
		public DateTime? UpdatedAtMin { get; set; }

		/// <summary>
		/// Show customers last updated before a specified date.
		///(format: 2014-04-25T16:15:47-04:00)
		/// </summary>
		[Description("updated_at_max")]
		public DateTime? UpdatedAtMax { get; set; }
	}
}
