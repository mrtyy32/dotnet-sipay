using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sipay.Test
{
	[TestClass]
	public class TokenTest
	{
		[TestMethod]
		public void get_token_test()
		{
			try
			{
				var client = new SipayClient(true);
				var token = client.GetToken();

				Assert.IsNotNull(token);
				Console.WriteLine(token);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
