using System.Net;
using System.Text;
using BLL.Models.Click;
using BLL.Models.Configs;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BLL.ClickService;

public class ClickService : IClickService
{
    private readonly ILogger<ClickService> _logger;

    private readonly IRepository<Order> _repository;

    private readonly PaymentConfig _config;

    public ClickService(IOptions<PaymentConfig> config, ILogger<ClickService> logger, IRepository<Order> repository)
    {
        _logger = logger;
        _repository = repository;
        _config = config.Value;
    }

    public async Task<int> CreateInvoice(float amount, string phone, long orderId)
    {
        return -1;
        // var httpClient = new HttpClient();
        //
        // var requestData = new ClickCreateInvoiceRequest()
        // {
        //     Amount = amount,
        //     PhoneNumber = phone,
        //     ServiceId = _config.ServiceId,
        //     MerchantTransId = orderId.ToString()
        // };
        //
        // var timestamp = DateTime.Now.ToUnixTime();
        // var digest = $"{timestamp}{_config.SecretKey}".Sha1();
        // var authHeader = $"{_config.MerchantId}:{digest}:{timestamp}";
        //
        // var requestMessage = new HttpRequestMessage()
        // {
        //     RequestUri = new Uri("https://api.click.uz/v2/merchant/invoice/create"),
        //     Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json"),
        //     Method = HttpMethod.Post,
        //     Headers =
        //     {
        //         {"Auth", authHeader}, 
        //         {"Accept", "application/json"}
        //     }
        // };
        //
        // var responseMessage = await httpClient.SendAsync(requestMessage);
        //
        // var response = await responseMessage.Content.ReadAsStringAsync();
        // if (responseMessage.StatusCode != HttpStatusCode.OK)
        // {
        //     _logger.LogError(
        //         "Failed to execute CreateInvoice: {amount} {phone} {order_id}. {status_code} {response}",
        //         amount,
        //         phone,
        //         orderId,
        //         responseMessage.StatusCode,
        //         response
        //     );
        //     throw new BusinessException("Не удалось выполнить запрос к системе Click");
        // }
        //
        // var clickInvoice = JsonConvert.DeserializeObject<ClickCreateInvoiceResponse>(response);
        //
        // _logger.LogWarning("Executed CreateInvoice {response}", JsonConvert.SerializeObject(clickInvoice, Formatting.Indented));
        //
        // return clickInvoice.InvoiceId;
    }

    public async Task<ClickPrepareBllResponseModel> ProcessPrepare(ClickPrepareBllModel model)
    {
        var computeHashOf = $"{model.ClickTransId}{model.ServiceId}{_config.SecretKey}{model.MerchantTransId}{model.Amount}{model.Action}{model.SignTime}";
        var md5 = computeHashOf.Md5();

        _logger.LogWarning(
            "Computed hash of {compute_hash_of} {hash} {expected}",
            computeHashOf,
            md5,
            model.SignString
        );

        if (!model.SignString.Equals(md5, StringComparison.OrdinalIgnoreCase))
        {
            return ClickPrepareBllResponseModel.FromError(-1);
        }

        var order = await _repository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == int.Parse(model.MerchantTransId));

        if (order is null)
        {
            return ClickPrepareBllResponseModel.FromError(-5);
        }

        if (order.PaymentStatus == PaymentStatus.Payed)
        {
            return ClickPrepareBllResponseModel.FromError(-4);
        }

        return new ClickPrepareBllResponseModel()
        {
            Error = 0,
            ClickTransId = model.ClickTransId,
            MerchantTransId = model.MerchantTransId,
            MerchantPrepareId = int.Parse(model.MerchantTransId)
        };
    }

    public async Task<ClickCompleteBllResponseModel> ProcessComplete(ClickCompleteBllModel model)
    {
        return new ClickCompleteBllResponseModel()
        {
            Error = 0,
            ClickTransId = model.ClickTransId,
            MerchantTransId = model.MerchantTransId,
            MerchantConfirmId = int.Parse(model.MerchantTransId)
        };
    }
}