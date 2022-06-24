using Newtonsoft.Json;

namespace Sipay
{
	public partial class SipayRequest
	{
		[JsonProperty("name")]
		public string FirstName { get; set; }

		[JsonProperty("surname")]
		public string LastName { get; set; }
	}
}
