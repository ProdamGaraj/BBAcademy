using System.ComponentModel.DataAnnotations;
using BLL.CartService;
using BLL.CourseService;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForCart;
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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> RemoveCourse([Required] long id)
    {
        var userId = HttpContext.User.GetId();
        await _cartService.RemoveCourse(id, userId);

        return Ok();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> AddCourse([Required] long id)
    {
        var userId = HttpContext.User.GetId();
        await _cartService.AddCourse(id, userId);

        return Ok();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Checkout()
    {
        var userId = HttpContext.User.GetId();
        await _cartService.Checkout(userId);

        return Ok();
    }
}