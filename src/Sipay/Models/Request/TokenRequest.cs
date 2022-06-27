using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Request
{
    public class TokenRequest : BaseRequest
    {
		[JsonProperty("app_id")]
		public string AppId { get; set; }

		[JsonProperty("app_secret")]
		public string AppSecret { get; set; }

        public TokenRequest()
        {
        }

        public TokenRequest(string appId, string appSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
        }
    }
}