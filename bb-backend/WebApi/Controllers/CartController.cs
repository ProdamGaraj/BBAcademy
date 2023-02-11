using BLL.CartService;
using BLL.CourseService;
using BLL.Models.CourseForCart;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForDashboard;
using BLL.Services.Interfaces;
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
    public async Task<ActionResult<CourseForCartDto>> RemoveCourse(long courseId)
    {
        var userId = HttpContext.User.GetId();
        var result = await _cartService.RemoveCourse(courseId, userId);

        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<CourseForCartDto>> Checkout()
    {
        var userId = HttpContext.User.GetId();
        var result = await _cartService.Checkout(userId);

        return Ok(result);
    }
}