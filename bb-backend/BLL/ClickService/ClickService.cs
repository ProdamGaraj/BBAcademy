using System.Net;
using System.Text;
using BLL.Models.Click;
using BLL.Models.Configs;
using BLL.Services.Interfaces;
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

    private readonly ICourseProgressService _courseProgressService;

    private readonly PaymentConfig _config;

    public ClickService(IOptions<PaymentConfig> config, ILogger<ClickService> logger, IRepository<Order> repository, ICourseProgressService courseProgressService)
    {
        _logger = logger;
        _repository = repository;
        _courseProgressService = courseProgressService;
        _config = config.Value;
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

        if (!long.TryParse(model.MerchantTransId, out _))
        {
            return ClickPrepareBllResponseModel.FromError(-5);
        }

        var order = await _repository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == long.Parse(model.MerchantTransId));

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
        var computeHashOf = $"{model.ClickTransId}{model.ServiceId}{_config.SecretKey}{model.MerchantTransId}{model.MerchantPrepareId}{model.Amount}{model.Action}{model.SignTime}";
        var md5 = computeHashOf.Md5();

        _logger.LogWarning(
            "Computed hash of {compute_hash_of} {hash} {expected}",
            computeHashOf,
            md5,
            model.SignString
        );

        if (!model.SignString.Equals(md5, StringComparison.OrdinalIgnoreCase))
        {
            return ClickCompleteBllResponseModel.FromError(-1);
        }

        if (!long.TryParse(model.MerchantTransId, out _))
        {
            return ClickCompleteBllResponseModel.FromError(-5);
        }

        var order = await _repository.GetAll()
            .Include(x => x.Courses)
            .FirstOrDefaultAsync(x => x.Id == model.MerchantPrepareId);

        if (order is null)
        {
            // заказ или клиент не найден
            return ClickCompleteBllResponseModel.FromError(-6);
        }

        if (order.PaymentStatus == PaymentStatus.Payed)
        {
            // заказ уже оплачен
            return ClickCompleteBllResponseModel.FromError(-4);
        }

        if (model.Error == -5017)
        {
            // отмена заказа
            order.PaymentStatus = PaymentStatus.Cancelled;
            await _repository.Update(order);
            return ClickCompleteBllResponseModel.FromError(-9);
        }

        if (Math.Abs(order.TotalSum - model.Amount) > 0.01)
        {
            // не совпадает сумма платежа
            return ClickCompleteBllResponseModel.FromError(-2);
        }

        if (order.PaymentStatus == PaymentStatus.Cancelled)
        {
            // заказ уже отменён
            return ClickCompleteBllResponseModel.FromError(-9);
        }

        order.PaymentStatus = PaymentStatus.Payed;
        await _repository.Update(order);
        
        await _courseProgressService.TransitionToBought(order.Courses.Select(x => x.Id).ToList(), order.UserId);
        
        // успешно
        return new ClickCompleteBllResponseModel()
        {
            Error = 0,
            ClickTransId = model.ClickTransId,
            MerchantTransId = model.MerchantTransId,
            MerchantConfirmId = int.Parse(model.MerchantTransId)
        };
    }
}