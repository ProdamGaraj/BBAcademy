using Newtonsoft.Json;

namespace BLL.Models.Click;

public class ClickCreateInvoiceResponse
{
    [JsonProperty("error_code")]
    public string ErrorCode { get; set; }
    [JsonProperty("error_note")]
    public string ErrorNote { get; set; }
    [JsonProperty("invoice_id")]
    public int InvoiceId { get; set; }
}