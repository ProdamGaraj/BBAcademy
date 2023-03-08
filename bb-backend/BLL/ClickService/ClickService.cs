using System.Net;
using System.Text;
using BLL.Models.Click;
using BLL.Models.Configs;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BLL.ClickService;

public class ClickService : IClickService
{
    private readonly ILogger<ClickService> _logger;

    private readonly PaymentConfig _config;

    public ClickService(IOptions<PaymentConfig> config, ILogger<ClickService> logger)
    {
        _logger = logger;
        _config = config.Value;
    }

    public async Task<int> CreateInvoice(float amount, string phone, long orderId)
    {
        var httpClient = new HttpClient();

        var requestData = new ClickCreateInvoiceRequest()
        {
            Amount = amount,
            PhoneNumber = phone,
            ServiceId = _config.ServiceId,
            MerchantTransId = orderId.ToString()
        };

        var timestamp = DateTime.Now.ToUnixTime();
        var digest = $"{timestamp}{_config.SecretKey}".Sha1();
        var authHeader = $"{_config.MerchantId}:{digest}:{timestamp}";

        var requestMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://api.click.uz/v2/merchant/invoice/create"),
            Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json"),
            Method = HttpMethod.Post,
            Headers =
            {
                {"Auth", authHeader}, 
                {"Accept", "application/json"}
            }
        };

        var responseMessage = await httpClient.SendAsync(requestMessage);

        var response = await responseMessage.Content.ReadAsStringAsync();
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError(
                "Failed to execute CreateInvoice: {amount} {phone} {order_id}. {status_code} {response}",
                amount,
                phone,
                orderId,
                responseMessage.StatusCode,
                response
            );
            throw new BusinessException("Не удалось выполнить запрос к системе Click");
        }

        var clickInvoice = JsonConvert.DeserializeObject<ClickCreateInvoiceResponse>(response);
        
        _logger.LogWarning("Executed CreateInvoice {response}", JsonConvert.SerializeObject(clickInvoice, Formatting.Indented));

        return clickInvoice.InvoiceId;
    }
}