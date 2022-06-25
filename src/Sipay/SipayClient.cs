using System.Collections.Generic;
using Sipay.Base;
using Sipay.Models.Request;
using Sipay.Models.Response;
using Sipay.Settings;

namespace Sipay
{
    public class SipayClient : ISipayBuilder
    {
        private SipayRequest request;
        private SipayClientSettings clientSettings;
        private RestHttpClient restHttpClient;

        private string TEST_MERCHANT_KEY = "$2y$10$HmRgYosneqcwHj.UH7upGuyCZqpQ1ITgSMj9Vvxn.t6f.Vdf2SQFO";
        private string TEST_APP_KEY = "6d4a7e9374a76c15260fcc75e315b0b9";
        private string TEST_SECRET_KEY = "b46a67571aa1e7ef5641dc3fa6f1712a";
        private string TEST_URL = "https://provisioning.sipay.com.tr/ccpayment/api";
        private string PROD_URL = "https://app.sipay.com.tr/ccpayment/api/";
        private string _baseUrl;
        private string _tokenUrl = "/token";
        private string _paymentUrl = "/paySmart3D";

        public SipayClient(bool test = false)
        {
            request = new SipayRequest();
            clientSettings = new SipayClientSettings();
            restHttpClient = new RestHttpClient();
            Test(test);
        }

        public ISipayBuilder CreditCard(string cardholderName, string creditCardNumber, string expiryMonth,
            string expiryYear, string cvv)
        {
            request.CardholderName = cardholderName;
            request.CreditCardNumber = creditCardNumber;
            request.ExpiryMonth = expiryMonth;
            request.ExpiryYear = expiryYear;
            request.CVV = cvv;

            return this;
        }

        public ISipayBuilder Customer(string firstName, string lastName, string ipAddress)
        {
            request.FirstName = firstName;
            request.LastName = lastName;

            return this;
        }

        public ISipayBuilder BillingAddress(string address1, string address2, string state, string city, string country,
            string zipCode, string email, string phone)
        {
            request.BillingAddress1 = address1;
            request.BillingAddress2 = address2;
            request.BillingState = state;
            request.BillingCity = city;
            request.BillingCountry = country;
            request.BillingEmail = email;
            request.BillingPhone = phone;
            request.BillingZipCode = zipCode;

            return this;
        }

        public ISipayBuilder Order(string currencyCode, int installments, string orderGuid, double orderTotal)
        {
            request.CurrencyCode = currencyCode;
            request.Installments = installments;
            request.OrderGuid = orderGuid;
            request.Description = $"{orderGuid} nolu sipariş ödemesi";
            request.OrderTotal = orderTotal;

            return this;
        }

        public ISipayBuilder BasketItems(List<SipayBasketItem> basketItems)
        {
            var itemJsonText = basketItems.ToJson();

            request.Items = itemJsonText;
            return this;
        }

        public ISipayBuilder Callbacks(string returlUrl, string cancelUrl)
        {
            request.ReturnUrl = returlUrl;
            request.CancelUrl = cancelUrl;

            clientSettings.ReturnUrl = request.ReturnUrl;
            clientSettings.CancelUrl = request.CancelUrl;

            return this;
        }

        public ISipayBuilder Execute(string token = "")
        {
            var uri = $"{_baseUrl}{_paymentUrl}";
            if (string.IsNullOrEmpty(token)) token = GetToken();
            
            var response = restHttpClient.PostData<ThreeDResponse, SipayRequest>(uri, request);
            //TODO
            return this;
        }

        public ISipayBuilder Test(bool isTest = true)
        {
            _baseUrl = isTest ? TEST_URL : PROD_URL;

            if (!isTest) return this;

            clientSettings.AppId = TEST_APP_KEY;
            clientSettings.AppSecret = TEST_SECRET_KEY;
            clientSettings.MerchantKey = TEST_MERCHANT_KEY;
            clientSettings.BaseUrl = _baseUrl;

            return this;
        }

        private string GetToken()
        {
            var tokenRequest = new TokenRequest
            {
                AppSecret = clientSettings.AppSecret,
                AppId = clientSettings.AppId
            };
            
            var uri = $"{_baseUrl}{_tokenUrl}";
            var response = restHttpClient.PostData<TokenResponse, TokenRequest>(uri, tokenRequest);

            return response.Data.StatusCode;
        }
    }
}