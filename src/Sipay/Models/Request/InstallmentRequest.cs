using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Request
{
    public class InstallmentRequest : BaseRequest
    {
        [JsonProperty("credit_card")] public string CreditCard { get; set; }

        [JsonProperty("amount")] public double Amount { get; set; }

        [JsonProperty("currency_code")] public string CurrencyCode { get; set; }

        [JsonProperty("merchant_key")] public string MerchantKey { get; set; }

        [JsonProperty("is_recurring")] public int IsRecurring { get; set; }
    }
}