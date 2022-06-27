using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Sipay.Base;
using Sipay.Helpers;
using Sipay.Models.Request;

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
            Dictionary<string, string> headers = null) where TRequest : BaseRequest
        {
			var jsonString = model.ToJson();

			var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endPoint),
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json"),
            };

			requestMessage.Headers.Add("Accept", "application/json");

			if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

            var httpResponse = HttpClient.SendAsync(requestMessage).Result;
			var httpResponseString = httpResponse.Content.ReadAsStringAsync().Result;

			return !httpResponse.IsSuccessStatusCode ? default
                : JsonConvert.DeserializeObject<TResponse>(httpResponseString);
        }

		public string PostData<TResponse, TRequest>(string endPoint, TRequest model,
			Dictionary<string, string> headers = null, bool isForm = true) where TRequest : SipayRequest
		{
			var requestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(endPoint),
			};

			if (isForm)
				requestMessage.Content = new FormUrlEncodedContent(FormHelper.FormKeyValues(model));

			requestMessage.Headers.Add("Accept", "application/json");

			if (headers != null)
				foreach (var header in headers)
					requestMessage.Headers.Add(header.Key, header.Value);

			var httpResponse = HttpClient.SendAsync(requestMessage).Result;
			var httpResponseString = httpResponse.Content.ReadAsStringAsync().Result;

			return !httpResponse.IsSuccessStatusCode ? default
				: httpResponseString;
		}
	}
}