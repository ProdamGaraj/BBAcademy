using BLL.CartService;
using BLL.CourseService;
using BLL.Models.GetCoursesForCart;
using BLL.Models.GetCoursesForDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
[ResponseCache(NoStore = true)]
public class CartController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ICartService _cartService;

    public CartController(ICourseService courseService, ICartService cartService)
    {
        _courseService = courseService;
        _cartService = cartService;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ICollection<CourseForCartDto>>> GetAll()
    {
        var userId = HttpContext.User.GetId();
        var result = await _courseService.GetCartedCoursesForUser(userId);

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ICollection<CourseForDashboardDto>>> GetForDashboard()
    {
        var userId = HttpContext.User.GetId();
        var result = await _courseService.GetCoursesForDashboard(userId);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> RemoveCourse(long courseId)
    {
        var userId = HttpContext.User.GetId();
        await _cartService.RemoveCourse(courseId, userId);

        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> Checkout()
    {
        var userId = HttpContext.User.GetId();
        await _cartService.Checkout(userId);

        return Ok();
    }
}