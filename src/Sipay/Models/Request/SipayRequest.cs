using Newtonsoft.Json;
using Sipay.Models.Request.Objects;
using System.Collections.Generic;

namespace Sipay
{
	public partial class SipayRequest
	{
		[JsonProperty("hash_key")]
		public string Hash { get; set; }

		[JsonProperty("cancel_url")]
		public string CancelUrl { get; set; }

		[JsonProperty("return_url")]
		public string ReturnUrl { get; set; }

		[JsonProperty("items")]
		public string Items { get; set; }

		//public SipayCard Card { get; set; }
		//public SipayCustomer Customer { get; set; }
		//public SipayAddress Address { get; set; }
		//public SipayOrder Order { get; set; }
		public List<SipayBasketItem> BasketItems { get; set; }
	}
}
