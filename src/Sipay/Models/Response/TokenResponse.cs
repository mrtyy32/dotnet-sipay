using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sipay.Models.Response.Token
{
    public partial class TokenResponse
    {
        [JsonProperty("status_code")] public int StatusCode { get; set; }

        [JsonProperty("status_description")] public string StatusDescription { get; set; }

        [JsonProperty("data")] public TokenData Data { get; set; }
    }

    public partial class TokenData
    {
        [JsonProperty("status_code")] public string StatusCode { get; set; }

        [JsonProperty("is_3d")] public int Is3D { get; set; }

        [JsonProperty("expires_at")] public DateTime ExpiresAt { get; set; }
    }

    public partial class TokenResponse
    {
        public static TokenResponse FromJson(string json) =>
            JsonConvert.DeserializeObject<TokenResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<TokenResponse> self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
    }
}