using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Sipay.Base;
using Sipay.Helpers;

namespace Sipay
{
    public class RestHttpClient
    {
        private static readonly HttpClient HttpClient;

        static RestHttpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient = new HttpClient();
        }

        public static RestHttpClient Create()
        {
            return new RestHttpClient();
        }

        public T Get<T>(string url)
        {
            var httpResponseMessage = HttpClient.GetAsync(url).Result;
            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }

        public T Get<T>(string url, Dictionary<string, string> headers)
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            foreach (var header in headers) requestMessage.Headers.Add(header.Key, header.Value);

            var httpResponseMessage = HttpClient.SendAsync(requestMessage).Result;
            return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        }

        public TResponse PostData<TResponse, TRequest>(string endPoint, TRequest model,
            Dictionary<string, string> headers = null, bool isForm = false) where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endPoint),
                Content = new StringContent(model.ToJson()),
            };
            requestMessage.Headers.Add("Accept", isForm ? "application/x-www-form-urlencoded" : "application/json");

            if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

            var httpResponse = HttpClient.SendAsync(requestMessage).Result;

            return !httpResponse.IsSuccessStatusCode
                ? default
                : JsonConvert.DeserializeObject<TResponse>(httpResponse.Content.ReadAsStringAsync().Result);
        }
    }
}