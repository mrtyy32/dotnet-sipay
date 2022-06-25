using Newtonsoft.Json;
using Sipay.Base;
using Sipay.Settings;

namespace Sipay.Helpers
{
    public static class JsonSerializer
    {
        public static string ToJson(this BaseRequest self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
        
        public static BaseResponse FromJson(string json) =>
            JsonConvert.DeserializeObject<BaseResponse>(json, Converter.Settings);
    }
}