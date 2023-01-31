using RestSharp;
using RestSharp.Extensions;
using System;
using System.ComponentModel;

namespace PX.Commerce.Custom.API.REST
{
    /// <summary>
    /// Example of a filter for fields that use a format like:
    ///  filter[0][attribute]=email&filter[0][in][0]=test@example.com
    /// </summary>
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class FilterCategories : Filter
	{

        /// <summary>
        /// Filter field name.
        /// </summary>
        [Description("filter[0][attribute]")]
		public string Name { get; set; }

        /// <summary>
        /// Filter field value.
        /// </summary>
        [Description("filter[0][in][0]")]
        public string Value { get; set; }

        public override void AddFilter(IRestRequest request)
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
}
