﻿namespace Sipay.Models.Request.Token
{
	public class TokenRequest
	{
		public string AppId { get; set; }
		public string AppSecret { get; set; }

		public TokenRequest()
		{
		}

		public TokenRequest(string appId, string appSecret)
		{
			AppId = appId;
			AppSecret = appSecret;
		}
	}
}