using Newtonsoft.Json;

namespace Sipay.Models.Response
{
	public partial class ThreeDResponse
	{
		[JsonProperty("status_code")]
		public int StatusCode { get; set; }

		[JsonProperty("status_description")]
		public string StatusDescription { get; set; }

		[JsonProperty("data")]
		public ThreeDResponseData Data { get; set; }
	}

	public partial class ThreeDResponseData
	{
		[JsonProperty("sipay_status")]
		public int SipayStatus { get; set; }

		[JsonProperty("order_no")]
		public int OrderNo { get; set; }

		[JsonProperty("order_id")]
		public int OrderId { get; set; }

		[JsonProperty("invoice_id")]
		public string InvoiceId { get; set; }

		[JsonProperty("sipay_payment_method")]
		public int SipayPaymentMethod { get; set; }

		[JsonProperty("credit_card_no")]
		public string CreditCardNo { get; set; }

		[JsonProperty("transaction_type")]
		public string TransactionType { get; set; }

		[JsonProperty("payment_status")]
		public int PaymentStatus { get; set; }

		[JsonProperty("payment_method")]
		public int PaymentMethod { get; set; }

		[JsonProperty("error_code")]
		public int ErrorCode { get; set; }

		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("hash_key")]
		public string HashKey { get; set; }
	}

	public partial class ThreeDResponse
	{
		public static ThreeDResponse FromJson(string json) => JsonConvert.DeserializeObject<ThreeDResponse>(json, Converter.Settings);
	}
}
