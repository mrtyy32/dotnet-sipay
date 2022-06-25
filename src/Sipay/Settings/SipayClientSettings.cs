using Newtonsoft.Json;

namespace Sipay.Settings
{
    public class SipayClientSettings
    {
        [JsonProperty("app_id")] public string AppId { get; set; }

        [JsonProperty("app_secret")] public string AppSecret { get; set; }

        [JsonProperty("merchant_key")] public string MerchantKey { get; set; }

        [JsonProperty("base_url")] public string BaseUrl { get; set; }
        
        [JsonProperty("return_url")] public string ReturnUrl { get; set; }
        
        [JsonProperty("cancel_url")] public string CancelUrl { get; set; }
    }
}