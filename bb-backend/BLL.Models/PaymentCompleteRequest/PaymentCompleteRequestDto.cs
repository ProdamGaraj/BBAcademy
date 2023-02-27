using System.Text.Json.Serialization;

namespace BLL.Models.PaymentCompleteRequest;

public class PaymentCompleteRequestDto
{
    [JsonPropertyName("click_trans_id")] public string ClickTransId { get; set; }
    [JsonPropertyName("service_id")] public string ServiceId { get; set; }
    [JsonPropertyName("click_paydoc_id")] public string ClickPaydocId { get; set; }
    [JsonPropertyName("merchant_prepare_id")]public string MerchantPrepareId { get; set; }
    [JsonPropertyName("amount")] public string Amount { get; set; }
    [JsonPropertyName("action")] public string Action { get; set; }
    [JsonPropertyName("error")] public string Error { get; set; }
    [JsonPropertyName("error_note")] public string ErrorNote { get; set; }
    [JsonPropertyName("sign_time")] public string SignTime { get; set; }
    [JsonPropertyName("sign_string")] public string SignString { get; set; }
}