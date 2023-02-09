using BLL.CourseService;
using BLL.Models.GetCourseForView;
using BLL.Models.GetCoursesForDashboard;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<GetCourseForViewDto>> GetFullInfoForView(long id)
    {
        var result = await _courseService.GetFullInfoForView(id);

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ICollection<CourseForDashboardDto>>> GetForDashboard()
    {
        if (HttpContext.User.Identity?.IsAuthenticated != true)
        {
            return Unauthorized();
        }

        var userId = HttpContext.User.GetId();
        var result = await _courseService.GetCoursesForDashboard(userId);

        return Ok(result);
    }
}