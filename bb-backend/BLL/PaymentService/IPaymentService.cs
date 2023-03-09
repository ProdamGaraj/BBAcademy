using Infrastructure.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.CreatePayment;

namespace BLL.PaymentService
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponse> CreatePayment(long userId);
    }
}
