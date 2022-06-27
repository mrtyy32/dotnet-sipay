using System.Collections.Generic;
using Sipay.Base;
using Sipay.Helpers;
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

		public string Execute(string token = "")
		{
			var uri = $"{_baseUrl}{Endpoints.Payment}";
			if (string.IsNullOrEmpty(token)) token = GetToken();

			var hash = HashHelper.GenerateHashKey(
				request.OrderTotal.ToString(),
				request.Installments.ToString(),
				request.CurrencyCode,
				clientSettings.MerchantKey,
				request.OrderGuid,
				clientSettings.AppSecret);

			request.Hash = hash;

			var response =
				restHttpClient.PostData<string, SipayRequest>(uri, request, AddTokenHeader(token), true);

			return response;
		}

		public RefundResponse Refund(string invoiceId, double refundAmount)
		{
			var uri = $"{_baseUrl}/refund";
			var token = GetToken();

			var refundRequest = new RefundRequest
			{
				Amount = refundAmount,
				AppId = clientSettings.AppId,
				AppSecret = clientSettings.AppSecret,
				InvoiceId = invoiceId,
				MerchantKey = clientSettings.MerchantKey,
			};

			var response =
				restHttpClient.PostData<RefundResponse, RefundRequest>(uri, refundRequest, AddTokenHeader(token));

			return response;
		}

		public ISipayBuilder Test(bool isTest = true)
		{
			_baseUrl = isTest ? TEST_URL : PROD_URL;

			if (!isTest) return this;

			clientSettings.AppId = TEST_APP_KEY;
			clientSettings.AppSecret = TEST_SECRET_KEY;
			clientSettings.MerchantKey = TEST_MERCHANT_KEY;
			clientSettings.BaseUrl = _baseUrl;

			request.MerchantKey = clientSettings.MerchantKey;

			return this;
		}

		public string GetToken()
		{
			var tokenRequest = new TokenRequest
			{
				AppSecret = clientSettings.AppSecret,
				AppId = clientSettings.AppId
			};

			var uri = $"{_baseUrl}{Endpoints.Token}";
			var response = restHttpClient.PostData<TokenResponse, TokenRequest>(uri, tokenRequest);

			return response.Data.Token;
		}

		public List<InstallmentData> GetInstallments(string binNumber, double amount, string currencyCode = "TRY")
		{
			var installmentRequest = new InstallmentRequest
			{
				Amount = amount,
				CreditCard = binNumber,
				CurrencyCode = currencyCode,
				MerchantKey = clientSettings.MerchantKey,
			};

			var uri = $"{_baseUrl}{Endpoints.Installment}";
			var response = restHttpClient.PostData<InstallmentResponse, InstallmentRequest>(uri, installmentRequest, AddTokenHeader(GetToken()));

			return response.Data;
		}

		private Dictionary<string, string> AddTokenHeader(string token)
		{
			var header = new Dictionary<string, string>();
			header.Add("Authorization", $"Bearer {token}");

			return header;
		}
	}
}