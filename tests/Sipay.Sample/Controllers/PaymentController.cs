using Newtonsoft.Json;
using Sipay.Models.Request;
using Sipay.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Sipay.Sample.Controllers
{
	public class PaymentController : Controller
	{

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string fakeRoute = "")
		{
			var client = new SipayClient(true);
			var token = client.GetToken();
			this.HttpContext.Session.Add("token", token);

			try
			{
				var basketItems = new List<SipayBasketItem>();

				for (int i = 1; i <= 3; i++)
				{
					basketItems.Add(new SipayBasketItem { Description = $"Product {i}", Name = $"Product {i}", Price = 25, Quantity = new Random().Next(1, 5) });
				}

				var orderTotal = basketItems.Sum(w => (w.Quantity * w.Price));

				var response = client
					.CreditCard("Murat Savran", "4508034508034509", "12", "26", "000")
					.Customer("Murat", "Savran", "127.0.0.1")
					.BillingAddress("Mimar Sinan Mh.", "Şükran Sk. No:41", "İstanbul", "Sultanbeyli", "Türkiye", "34395", "murat.savran@lemooncreative.com", "5413348334")
					.Order("TRY", 1, Guid.NewGuid().ToString(), orderTotal)
					.BasketItems(basketItems)
					.Callbacks("http://localhost:54492/Payment/SuccessUrl", "http://localhost:54492/Payment/FailUrl")
					.Execute();

				Response.Write(response);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return View();
		}

		public ActionResult SuccessUrl()
		{
			string query = Request.Url.Query;
			var response = RequestToModel(query);

			if (response.SipayStatus == 1)
			{
				Task.Delay(1500);

				//refund 
				var client = new SipayClient(true);
				var token = this.HttpContext.Session["token"] as string ?? client.GetToken();

				var refundResponse = client.Refund(response.InvoiceId, 25);
			}

			return View();
		}

		public ActionResult FailUrl()
		{
			string query = Request.Url.Query;
			var response = RequestToModel(query);

			ViewBag.ErrorMessage = response.Error;

			return View();
		}

		private ThreeDResponseData RequestToModel(string query)
		{
			var decoded = HttpUtility.ParseQueryString(query);
			var json = new JavaScriptSerializer().Serialize(
													 decoded.Keys.Cast<string>()
														 .ToDictionary(k => k, k => decoded[k]));

			var response = JsonConvert.DeserializeObject<ThreeDResponseData>(json, Sipay.Settings.Converter.Settings);

			return response;
		}
	}
}