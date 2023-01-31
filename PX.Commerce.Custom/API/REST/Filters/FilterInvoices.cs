using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Commerce.Custom.API.REST
{

	/// <summary>
	/// Example filter with common fields for invoices
	/// </summary>
	public class FilterInvoices : Filter
	{
		/// <summary>
		/// Restrict results to specific increment Id.
		/// </summary>
		[Description("filters[increment_id][eq]")]
		public string Id { get; set; }

		/// <summary>
		/// Restrict results to specific increment Id.
		/// </summary>
		[Description("filters[order_id][eq]")]
		public string OrderId { get; set; }

		/// <summary>
		/// Restrict results to those after the specified ID.
		/// </summary>
		[Description("filters[increment_id][gt]")]
		public string SinceId { get; set; }

		/// <summary>
		/// Filter results by status.
		/// </summary>
		[Description("filters[state][eq]")]
		public OrderStatus? Status { get; set; }

		/// <summary>
		/// sort by field
		/// </summary>
		[Description("order[created_at]")]
		public string Order { get; set; }
	}
}
