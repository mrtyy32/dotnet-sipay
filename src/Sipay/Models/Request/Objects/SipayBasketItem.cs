using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sipay.Models.Request.Objects
{
	public partial class SipayBasketItem
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public double Price { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}

	public static class Serialize
	{
		public static string ToJson(this List<SipayBasketItem> self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}
}

