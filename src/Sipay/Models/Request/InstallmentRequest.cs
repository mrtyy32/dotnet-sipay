namespace Sipay.Models.Request
{
	public class InstallmentRequest
	{
		public int CreditCard { get; set; }

		public decimal Amount { get; set; }

		public string CurrencyCode { get; set; }

		public string MerchantKey { get; set; }

		public int IsRecurring { get; set; }

		public int Is2D { get; set; }
	}
}
