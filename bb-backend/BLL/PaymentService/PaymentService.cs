using BLL.Models.Configs;
using Infrastructure.Models.Enum;
using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using BLL.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BLL.PaymentService
{
    internal class PaymentService : IPaymentService
    {
        private readonly IRepository<Course> _courseRepository;
        
        private readonly PaymentConfig _paymentConfig;

        public PaymentService(IRepository<Course> courseRepository, IOptions<PaymentConfig> paymentConfig)
        {
            _courseRepository = courseRepository;
            _paymentConfig = paymentConfig.Value;
        }
        public async Task<string> GetUrlForPurchase(long userId)
        {
            var coursePrices = await _courseRepository.GetAll()
                .Where(c => c.CourseProgresses.Any(p => p.State == CourseProgressState.InCart && p.UserId == userId))
                .Select(c => c.Price)
                .ToListAsync();

            if (!coursePrices.Any())
            {
                throw new BusinessException("Корзина пуста!");
            }

            var amount = coursePrices.Sum();

            var paymentUrl = "https://my.click.uz/services/pay";

            paymentUrl = QueryHelpers.AddQueryString(paymentUrl, "service_id", _paymentConfig.ServiceId.ToString());
            paymentUrl = QueryHelpers.AddQueryString(paymentUrl, "merchant_id", _paymentConfig.MerchantId.ToString());
            paymentUrl = QueryHelpers.AddQueryString(paymentUrl, "amount", amount.ToString());
            paymentUrl = QueryHelpers.AddQueryString(paymentUrl, "merchant_user_id", userId.ToString());
            paymentUrl = QueryHelpers.AddQueryString(paymentUrl, "return_url", _paymentConfig.ReturnUrl);

            if (_paymentConfig.CardType is not null)
            {
                paymentUrl = QueryHelpers.AddQueryString(paymentUrl, "card_type", _paymentConfig.CardType);
            }

            return paymentUrl;
        }
    }
}
