using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sipay.Test
{
	[TestClass]
	public class InstallmentTest
	{
		[TestMethod]
		public void get_installment_test()
		{
			try
			{
				var client = new SipayClient(true);
				var installmentData = client.GetInstallments("450803", 300.00);

				Assert.IsNotNull(installmentData);
				Console.WriteLine(installmentData);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
