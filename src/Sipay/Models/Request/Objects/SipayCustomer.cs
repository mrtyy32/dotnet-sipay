using Newtonsoft.Json;

namespace Sipay
{
	public partial class SipayRequest
	{
		[JsonProperty("name")]
		public string FirstName { get; set; }

		[JsonProperty("surname")]
		public string LastName { get; set; }
		
		[JsonProperty("ip")]
		public string IpAddress { get; set; }
	}
}
