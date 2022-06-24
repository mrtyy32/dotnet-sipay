using Newtonsoft.Json;

namespace Sipay
{
	public partial class SipayRequest
	{
		[JsonProperty("cc_holder_name")]
		public string CardholderName { get; set; }

		[JsonProperty("cc_no")]
		public string CreditCardNumber { get; set; }

		[JsonProperty("expiry_month")]
		public string ExpiryMonth { get; set; }

		[JsonProperty("expiry_year")]
		public string ExpiryYear { get; set; }

		[JsonProperty("cvv")]
		public string CVV { get; set; }
	}
}
