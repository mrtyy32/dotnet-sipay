﻿using Newtonsoft.Json;

namespace Sipay.Models.Request
{
    public partial class SipayRequest
    {
        [JsonProperty("currency_code")] public string CurrencyCode { get; set; }

        [JsonProperty("installments_number")] public int Installments { get; set; }

        [JsonProperty("invoice_id")] public string OrderGuid { get; set; }

        [JsonProperty("invoice_description")] public string Description { get; set; }

        [JsonProperty("total")] public double OrderTotal { get; set; }
    }
}