using BLL.Models.Configs;
using Infrastructure.Models.Enum;
using Infrastructure.Models;
using BLL.ClickService;
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

        private readonly IClickService _clickService;

        private readonly PaymentConfig _paymentConfig;

        public PaymentService(IRepository<Course> courseRepository, IRepository<User> userRepository, IRepository<Order> orderRepository, IOptions<PaymentConfig> paymentConfig, IClickService clickService, ILogger<PaymentService> logger)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _clickService = clickService;
            _logger = logger;
            _paymentConfig = paymentConfig.Value;
        }

        public async Task CreatePayment(long userId)
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

            var userPhone = await _userRepository.GetAll()
                .Where(x => x.Id == userId)
                .Select(x => x.Phone)
                .FirstOrDefaultAsync();

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
                CreatedAt = DateTime.Now
            };

            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();

            var amount = (float)coursesInCart.Sum(x => x.Price);

            var invoiceId = await _clickService.CreateInvoice(amount, userPhone, order.Id);

            order.ExternalPaymentId = invoiceId.ToString();
            await _orderRepository.Update(order);
            
            _logger.LogWarning("Created Order {order_id} {invoice_id}", order.Id, invoiceId);
            
            // ON SUCCESS
            // await _courseProgressService.TransitionToBought(courses, userId);
        }
    }
}