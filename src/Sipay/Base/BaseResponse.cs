using Newtonsoft.Json;

namespace Sipay.Base
{
    public class BaseResponse
    {
        [JsonProperty("status_code")] public int StatusCode { get; set; }

        [JsonProperty("status_description")] public string StatusDescription { get; set; }
        
        [JsonProperty("message")] public string Message { get; set; }
    }
}