using System.Text.Json.Serialization;

namespace BLL.Models.PaymentCompleteResponse;

public class PaymentCompleteResponseDto
{
    [JsonPropertyName("click_trans_id")] public string ClickTransId { get; set; }
    [JsonPropertyName("merchant_trans_id")] public string MerchantTransId { get; set; }
    [JsonPropertyName("merchant_confirm_id")] public string MerchantConfirmId { get; set; }
    [JsonPropertyName("error")] public string Error { get; set; }
    [JsonPropertyName("error_note")] public string ErrorNote { get; set; }
}