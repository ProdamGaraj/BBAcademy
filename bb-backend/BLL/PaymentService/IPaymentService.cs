using Infrastructure.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.PaymentService
{
    public interface IPaymentService
    {
        Task CreatePayment(long userId);
    }
}
