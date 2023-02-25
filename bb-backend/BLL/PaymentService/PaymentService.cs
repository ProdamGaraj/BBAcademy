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
using Microsoft.EntityFrameworkCore;

namespace BLL.PaymentService
{
    internal class PaymentService : IPaymentService
    {
        private readonly IRepository<Course> _courseRepository;

        public PaymentService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<string> GetUrlForPurchase(int userId)
        {
            var courses = await _courseRepository.GetAll()
                .Where(c => c.CourseProgresses.Any(p => p.State == CourseProgressState.InCart && p.UserId == userId))
                .Select(c => c.Price)
                .ToListAsync();
            decimal payment = courses.Sum();
            PaymentConfig paymentConfig = JsonConvert.DeserializeObject<PaymentConfig>(File.ReadAllText("PaymentDetails.json"));
            string paymentUrl = $"https://my.click.uz/services/pay?service_id={paymentConfig.ServiceId}&merchant_id={paymentConfig.MerchantId}&amount={payment}&return_url={paymentConfig.ReturnUrl}";
            if (paymentConfig.CardType is not null)
            {
                paymentUrl += "&card_type =" + paymentConfig.CardType;
            }
            return paymentUrl;
        }
    }
}
