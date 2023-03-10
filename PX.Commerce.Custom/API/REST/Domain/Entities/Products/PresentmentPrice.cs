using System.ComponentModel;
using Newtonsoft.Json;

namespace PX.Commerce.Zoey.API.REST
{
	public class PresentmentPrice
	{
		/// <summary>
		/// The three-letter code (ISO 4217 format) for one of the shop's enabled presentment currencies.
		/// </summary>
		[JsonProperty("currency_code")]
		[Description(ZoeyCaptions.CurrencyCode)]
		public string CurrencyCode { get; set; }

		/// <summary>
		/// The variant's price or compare-at price in the presentment currency.
		/// </summary>
		[JsonProperty("amount")]
		[Description(ZoeyCaptions.Amount)]
		public decimal Amount { get; set; }
	}
}
