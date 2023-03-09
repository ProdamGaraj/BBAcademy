using BLL.Models.Configs;
using Infrastructure.Models.Enum;
using Infrastructure.Models;
using BLL.ClickService;
using BLL.Models.CreatePayment;
using Infrastructure.Repositories;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BLL.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Order> _orderRepository;

        private readonly ILogger<PaymentService> _logger;

        private readonly PaymentConfig _paymentConfig;

        public PaymentService(IRepository<Course> courseRepository, IRepository<User> userRepository, IRepository<Order> orderRepository, IOptions<PaymentConfig> paymentConfig, ILogger<PaymentService> logger)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _logger = logger;
            _paymentConfig = paymentConfig.Value;
        }

        public async Task<CreatePaymentResponse> CreatePayment(long userId)
        {
            var coursesInCart = await _courseRepository.GetAll()
                .Where(c => c.CourseProgresses.Any(p => p.State == CourseProgressState.InCart && p.UserId == userId))
                .Select(
                    c => new
                    {
                        c.Id,
                        c.Price
                    }
                )
                .ToListAsync();

            if (!coursesInCart.Any())
            {
                throw new BusinessException("Корзина пуста!");
            }

            var amount = (float)coursesInCart.Sum(x => x.Price);
            var order = new Order()
            {
                UserId = userId,
                OrderLines = coursesInCart
                    .Select(
                        x => new OrderLine()
                        {
                            CourseId = x.Id
                        }
                    )
                    .ToList(),
                TotalSum = amount,
                CreatedAt = DateTime.Now
            };

            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();

            await _orderRepository.Update(order);
            
            _logger.LogWarning("Created Order {order_id}", order.Id);

            return new CreatePaymentResponse()
            {
                MerchantId = _paymentConfig.MerchantId,
                ServiceId = _paymentConfig.ServiceId,
                TransAmount = amount,
                MerchantUserId = userId,
                TransId = order.Id.ToString(),
                ReturnUrl = "https://birdegop.ru:8080/courses"
            };
        }
    }
}