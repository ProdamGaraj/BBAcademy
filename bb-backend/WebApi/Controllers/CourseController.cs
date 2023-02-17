using System.ComponentModel.DataAnnotations;
using BLL.CourseService;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
[ResponseCache(NoStore = true)]
public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<GetCourseForLearningDto>> GetForLearning([Required] long id)
    {
        var userId = HttpContext.User.GetId();
        var result = await _courseService.GetForLearning(id, userId);

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
}