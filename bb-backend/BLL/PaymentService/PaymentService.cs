using BLL.Models.Configs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.PaymentService
{
    internal class PaymentService : IPaymentService
    {
        public async Task<string> GetUrlForPurchase(long payment)
        {
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
