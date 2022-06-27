using System;
using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Response
{
    public class TokenResponse : BaseResponse
    {
        [JsonProperty("data")] public TokenData Data { get; set; }
    }

    public class TokenData
    {
        [JsonProperty("token")] public string Token { get; set; }

        [JsonProperty("is_3d")] public int Is3D { get; set; }

        [JsonProperty("expires_at")] public DateTime ExpiresAt { get; set; }
    }
}