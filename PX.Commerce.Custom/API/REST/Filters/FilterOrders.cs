using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

    /// <summary>
    /// Example filter with common fields for orders
    /// </summary>
    public class FilterOrders : Filter
	{

		/// <summary>
		/// Restrict results to those after the specified ID.
		/// </summary>
		[Description("since_id")]
		public string SinceID { get; set; }

		/// <summary>
		/// Show only certain fields, specified by a comma-separated list of field names.
		/// </summary>
		[Description("fields")]
		public string Fields { get; set; }

		/// <summary>
		/// Filter results by status.
		/// </summary>
		[Description("status")]
		public OrderStatus? Status { get; set; }

		/// <summary>
		/// Filter results by name.
		/// </summary>
		[Description("name")]
		public string Name { get; set; }

		/// <summary>
		/// Filter results by financial_status.
		/// </summary>
		[Description("financial_status")]
		public string FinancialStatus { get; set; }

		/// <summary>
		/// Filter results by fulfillment_status.
		/// </summary>
		[Description("fulfillment_status")]
		public string FulfillmentStatus { get; set; }

		/// <summary>
		/// Show orders imported at or after date (format: 2014-04-25T16:15:47-04:00).
		/// </summary>
		[Description("processed_at_min")]
		public string ProcessedAtMin { get; set; }

		/// <summary>
		/// Show orders imported at or before date (format: 2014-04-25T16:15:47-04:00).
		/// </summary>
		[Description("processed_at_max")]
		public string ProcessedAtMax { get; set; }

		/// <summary>
		/// Show orders imported at or after date (format: 2014-04-25T16:15:47-04:00).
		/// </summary>
		[Description("created_at[gte]")]
		public string CreatedAtMin { get; set; }

		/// <summary>
		/// Show orders imported at or before date (format: 2014-04-25T16:15:47-04:00).
		/// </summary>
		[Description("created_at[lt]")]
		public string CreatedAtMax { get; set; }

		/// <summary>
		/// sort by field
		/// </summary>
		[Description("order")]
		public string Order { get; set; }
	}
}
