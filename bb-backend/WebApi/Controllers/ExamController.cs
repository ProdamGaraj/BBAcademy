using BLL.CourseService;
using BLL.ExamService;
using BLL.Models.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class ExamController:Controller
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
    public async Task<ActionResult<ExamDto>> Send(ExamDto dto)
    {
        await _examService.SolveExam(dto);
        return Ok();
    }
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ExamDto>> GetByCourse(long courseId)
    {
        await _examService.GetByCourse(courseId);
        return Ok();
    }
}