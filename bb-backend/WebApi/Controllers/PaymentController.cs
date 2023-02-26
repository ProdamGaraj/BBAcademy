

using BLL.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
[ResponseCache(NoStore = true)]
public class PaymentController:Controller
{
    private readonly IPaymentService _paymentService;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Pay()
    {
        var userId = HttpContext.User.GetId();
        var url = await _paymentService.GetUrlForPurchase(userId);
        return Ok(url);
    }
}