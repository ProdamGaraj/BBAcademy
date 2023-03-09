using System.Text.Json.Serialization;

namespace BLL.Models.Click;

public class ClickPrepareBllResponseModel
{
    [JsonPropertyName("click_trans_id")]
    public long ClickTransId { get; set; }

    [JsonPropertyName("merchant_trans_id")]
    public string MerchantTransId { get; set; }

    [JsonPropertyName("merchant_prepare_id")]
    public int MerchantPrepareId { get; set; }

    [JsonPropertyName("error")]
    public int Error { get; set; }

    [JsonPropertyName("error_note")]
    public string ErrorNote { get; set; }

    public static ClickPrepareBllResponseModel FromError(int error)
    {
        return new ClickPrepareBllResponseModel()
        {
            Error = error
        };
    }
}