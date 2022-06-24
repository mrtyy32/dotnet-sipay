using Newtonsoft.Json;

namespace Sipay
{
	public partial class SipayRequest
	{
		[JsonProperty("bill_address1")]
		public string BillingAddress1 { get; set; }

		[JsonProperty("bill_address2")]
		public string BillingAddress2 { get; set; }

		[JsonProperty("bill_city")]
		public string BillingCity { get; set; }

		[JsonProperty("bill_postcode")]
		public string BillingZipCode { get; set; }

		[JsonProperty("bill_state")]
		public string BillingState { get; set; }

		[JsonProperty("bill_country")]
		public string BillingCountry { get; set; }

		[JsonProperty("bill_email")]
		public string BillingEmail { get; set; }

		[JsonProperty("bill_phone")]
		public string BillingPhone { get; set; }
	}
}
