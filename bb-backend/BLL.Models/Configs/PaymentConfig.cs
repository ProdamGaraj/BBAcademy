using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models.Configs
{
    public class PaymentConfig
    {
        public int MerchantId { get; set; }
        public int ServiceId { get; set; }
        
        [JsonPropertyName("Merchant_User_Id")]
        public string MerchantUserId { get; set; }
        public string CardType { get; set; }
        public string ReturnUrl { get; set; }
    }
}
