using Newtonsoft.Json;
using Sipay.Base;
using System.Collections.Generic;

namespace Sipay.Models.Response
{
    public class InstallmentResponse : BaseResponse
    {
        [JsonProperty("data")] public List<InstallmentData> Data { get; set; }
    }

    public class InstallmentData
    {
        [JsonProperty("pos_id")] public int PosId { get; set; }

        [JsonProperty("campaign_id")] public int CampaignId { get; set; }

        [JsonProperty("allocation_id")] public int AllocationId { get; set; }

        [JsonProperty("installments_number")] public int InstallmentsNumber { get; set; }

        [JsonProperty("card_type")] public string CardType { get; set; }

        [JsonProperty("card_program")] public string CardProgram { get; set; }

        [JsonProperty("payable_amount")] public double PayableAmount { get; set; }

        [JsonProperty("hash_key")] public string HashKey { get; set; }

        [JsonProperty("amount_to_be_paid")] public double AmountToBePaid { get; set; }

        [JsonProperty("currency_code")] public string CurrencyCode { get; set; }

        [JsonProperty("currency_id")] public int CurrencyId { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("maturity_period")] public string MaturityPeriod { get; set; }

        [JsonProperty("payment_frequency")] public string PaymentFrequency { get; set; }
    }
}