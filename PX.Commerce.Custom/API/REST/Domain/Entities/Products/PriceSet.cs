using Newtonsoft.Json;
using PX.Commerce.Core;

namespace PX.Commerce.Zoey.API.REST
{
	[CommerceDescription(ZoeyCaptions.PriceSet)]
	public class PriceSet
	{
		[JsonProperty("shop_money", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(ZoeyCaptions.ShopMoney, FieldFilterStatus.Skipped, FieldMappingStatus.Import)]
		public PresentmentPrice ShopMoney { get; set; }

		[JsonProperty("presentment_money", NullValueHandling = NullValueHandling.Ignore)]
		[CommerceDescription(ZoeyCaptions.PresentmentMoney, FieldFilterStatus.Skipped, FieldMappingStatus.Import)]
		public PresentmentPrice PresentmentMoney { get; set; }
	}
}
