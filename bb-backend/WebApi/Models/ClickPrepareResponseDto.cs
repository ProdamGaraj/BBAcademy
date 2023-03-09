using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models;

public class ClickPrepareResponseDto
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
}