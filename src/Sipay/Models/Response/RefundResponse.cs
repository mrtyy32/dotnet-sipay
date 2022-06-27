using Newtonsoft.Json;
using Sipay.Base;

namespace Sipay.Models.Response
{
	public partial class RefundResponse : BaseResponse
	{
		[JsonProperty("order_no")]
		public long OrderNo { get; set; }

		[JsonProperty("invoice_id")]
		public string InvoiceId { get; set; }

		[JsonProperty("ref_no")]
		public string RefNo { get; set; }
	}
}
