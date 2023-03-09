using System.Text.Json.Serialization;

namespace WebApi.Models;

public class ClickCompleteResponseDto
{
    /// ID платежа в системе CLICK
    [JsonPropertyName("click_trans_id")]
    public long ClickTransId { get; set; }

    /// ID заказа(для Интернет магазинов)/лицевого счета/логина в биллинге поставщика
    [JsonPropertyName("merchant_trans_id")]
    public string MerchantTransId { get; set; }

    /// ID транзакции завершения платежа в биллинг системе (может быть NULL)
    [JsonPropertyName("merchant_confirm_id")]
    public int MerchantConfirmId { get; set; }

    /// Код статуса запроса. 0 – успешно. В случае ошибки возвращается код ошибки.
    [JsonPropertyName("error")]
    public int Error { get; set; }

    /// Описание кода завершения платежа.
    [JsonPropertyName("error_note")]
    public string ErrorNote { get; set; }
}