using Newtonsoft.Json;

namespace BLL.Models.Click;

public class ClickCreateInvoiceRequest
{
    [JsonProperty("service_id")]
    public int ServiceId { get; set; }

    [JsonProperty("amount")]
    public float Amount { get; set; }

    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }

    [JsonProperty("merchant_trans_id")]
    public string MerchantTransId { get; set; }
}