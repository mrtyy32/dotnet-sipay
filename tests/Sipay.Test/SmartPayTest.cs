using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sipay.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sipay.Test
{
	[TestClass]
	public class SmartPayTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			try
			{
				var client = new SipayClient(true);

				var basketItems = new List<SipayBasketItem>();

				for (int i = 1; i <= 3; i++)
				{
					basketItems.Add(new SipayBasketItem { Description = $"Product {i}", Name = $"Product {i}", Price = 25, Quantity = new Random().Next(1, 5) });
				}

				var orderTotal = basketItems.Sum(w=> (w.Quantity * w.Price));

				var response = client
					.CreditCard("Murat Savran", "4508034508034509", "12", "26", "000")
					.Customer("Murat", "Savran", "127.0.0.1")
					.BillingAddress("Mimar Sinan Mh.", "Şükran Sk. No:41", "İstanbul", "Sultanbeyli", "Türkiye", "34395", "murat.savran@lemooncreative.com", "5413348334")
					.Order("TRY", 1, Guid.NewGuid().ToString(), orderTotal)
					.BasketItems(basketItems)
					.Callbacks("http://localhost:62340/SuccessUrl", "http://localhost:62340/FailUrl")
					.Execute();

				Assert.IsNotNull(response);
				Console.WriteLine(response);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
