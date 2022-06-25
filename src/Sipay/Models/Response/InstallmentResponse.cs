using Newtonsoft.Json;

namespace Sipay.Models.Response
{
    public partial class InstallmentResponse
    {
        [JsonProperty("status_code")] public int StatusCode { get; set; }

        [JsonProperty("status_description")] public string StatusDescription { get; set; }

        [JsonProperty("data")] public InstallmentData Data { get; set; }
    }

    public partial class InstallmentData
    {
        [JsonProperty("pos_id")] public int PosId { get; set; }

        [JsonProperty("campaign_id")] public int CampaignId { get; set; }

        [JsonProperty("allocation_id")] public int AllocationId { get; set; }

        [JsonProperty("installments_number")] public int InstallmentsNumber { get; set; }

        [JsonProperty("card_type")] public string CardType { get; set; }

        [JsonProperty("card_program")] public string CardProgram { get; set; }

        [JsonProperty("payable_amount")] public int PayableAmount { get; set; }

        [JsonProperty("hash_key")] public string HashKey { get; set; }

        [JsonProperty("amount_to_be_paid")] public int AmountToBePaid { get; set; }

        [JsonProperty("currency_code")] public string CurrencyCode { get; set; }

        [JsonProperty("currency_id")] public int CurrencyId { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("maturity_period")] public string MaturityPeriod { get; set; }

        [JsonProperty("payment_frequency")] public string PaymentFrequency { get; set; }
    }

    public partial class InstallmentResponse
    {
        public static InstallmentResponse FromJson(string json) =>
            JsonConvert.DeserializeObject<InstallmentResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this InstallmentResponse self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
    }
}