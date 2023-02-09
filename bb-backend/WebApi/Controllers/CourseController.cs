using BLL.CourseService;
using BLL.Models.GetCourseForView;
using BLL.Services.Interfaces;
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
    public async Task<ActionResult<GetCourseForViewDto>> GetFullInfoForView(long id)
    {
        var result = await _courseService.GetFullInfoForView(id);

        return Ok(result);
    }
}