using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class CartController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Empty()
    {
        return Ok();
    }
}