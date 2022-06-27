using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Request
{
	public class RefundRequest : BaseRequest
	{
		[JsonProperty("invoice_id")]
		public string InvoiceId { get; set; }

		[JsonProperty("amount")]
		public double Amount { get; set; }

		[JsonProperty("app_id")]
		public string AppId { get; set; }

		[JsonProperty("app_secret")]
		public string AppSecret { get; set; }

		[JsonProperty("merchant_key")]
		public string MerchantKey { get; set; }
	}
}
