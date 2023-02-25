using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Configs
{
    public class PaymentConfig
    {
        public int MerchantId { get; set; }
        public int ServiceId { get; set; }
        public string Merchant_User_Id { get; set; }
        public string CardType { get; set; }
        public string ReturnUrl { get; set; }
    }
}
