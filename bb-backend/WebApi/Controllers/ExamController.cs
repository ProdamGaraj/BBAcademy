using BLL.CourseService;
using BLL.ExamService;
using BLL.Models.GetExamForTesting;
using BLL.Models.SaveCourseExamResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class ExamController : Controller
{
    private ILogger<DataController> _logger;

    private readonly IExamService _examService;

    public ExamController(IExamService examService, ILogger<DataController> logger)
    {
        _examService = examService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> SaveCourseExamResults([FromBody] SaveCourseExamResultsDto dto)
    {
        var userId = HttpContext.User.GetId();
        await _examService.SaveCourseExamResults(userId, dto);
        return Ok();
    }
}