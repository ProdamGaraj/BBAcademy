using System.Text.Json.Serialization;

namespace BLL.Models.PaymentResponse;

public class PaymentResponseDto
{
    [JsonPropertyName("click_trans_id")] public long ClickTransId { get; set; }

    [JsonPropertyName("merchant_trans_id")] public string MerchantTransId { get; set; }

    [JsonPropertyName("merchant_prepare_id")] public int MerchantPrepareId { get; set; }//TODO resolve long/int for merchant prepare id

    [JsonPropertyName("error")] public int Error { get; set; }

    [JsonPropertyName("error_note")] public string ErrorNote { get; set; }
}