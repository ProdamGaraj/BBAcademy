using System.Text.Json.Serialization;

namespace BLL.Models;

public class PaymentRequestDto
{
    [JsonPropertyName("click_trans_id")] public long ClickTtransId { get; set; }

    [JsonPropertyName("service_id")] public int ServiceId { get; set; }

    [JsonPropertyName("click_paydoc_id")] public long ClickPaydocId { get; set; }

    [JsonPropertyName("merchant_trans_id")]public string MerchantTransId { get; set; }

    [JsonPropertyName("amount")] public float Amount { get; set; }

    [JsonPropertyName("action")] public int Action { get; set; }

    [JsonPropertyName("error")] public int Error { get; set; }

    [JsonPropertyName("error_note")] public string ErrorNote { get; set; }

    [JsonPropertyName("error_note")] public string SignTime { get; set; }

    [JsonPropertyName("sign_string")] public string SignString { get; set; }
}