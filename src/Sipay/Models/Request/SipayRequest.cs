using System.Collections.Generic;
using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Request
{
    public partial class SipayRequest : BaseRequest
    {
        [JsonProperty("hash_key")] public string Hash { get; set; }

        [JsonProperty("cancel_url")] public string CancelUrl { get; set; }

        [JsonProperty("return_url")] public string ReturnUrl { get; set; }

		[JsonProperty("merchant_key")] public string MerchantKey { get; set; }

		[JsonProperty("items")] public string Items { get; set; }

		[JsonIgnore]
        public List<SipayBasketItem> BasketItems { get; set; }
    }
}