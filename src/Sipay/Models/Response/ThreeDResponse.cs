﻿using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Response
{
	public class ThreeDResponse : BaseResponse
	{
		[JsonProperty("data")] public ThreeDResponseData Data { get; set; }
	}

	public class ThreeDResponseData
	{
		[JsonProperty("sipay_status")] public int SipayStatus { get; set; }

		[JsonProperty("order_no")] public long OrderNo { get; set; }

		[JsonProperty("order_id")] public long OrderId { get; set; }

		[JsonProperty("invoice_id")] public string InvoiceId { get; set; }

		[JsonProperty("sipay_payment_method")] public int SipayPaymentMethod { get; set; }

		[JsonProperty("credit_card_no")] public string CreditCardNo { get; set; }

		[JsonProperty("transaction_type")] public string TransactionType { get; set; }

		[JsonProperty("payment_status")] public int PaymentStatus { get; set; }

		[JsonProperty("payment_method")] public int PaymentMethod { get; set; }

		[JsonProperty("error_code")] public int ErrorCode { get; set; }

		[JsonProperty("error")] public string Error { get; set; }

		[JsonProperty("hash_key")] public string HashKey { get; set; }
	}
}
