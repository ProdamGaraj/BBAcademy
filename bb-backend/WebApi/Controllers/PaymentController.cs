using BLL.Models.CreatePayment;
using BLL.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
[ResponseCache(NoStore = true)]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreatePaymentResponse>> CreatePayment()
    {
        var userId = HttpContext.User.GetId();
        var response = await _paymentService.CreatePayment(userId);
        return Ok(response);
    }
}