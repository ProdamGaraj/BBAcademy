using BLL.CourseService;
using BLL.Models.Save;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class DataController : Controller
    {
        private ILogger<DataController> _logger;

        private readonly ICourseService _courseService;

        public DataController(ILogger<DataController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCourse([FromBody] SaveCourseDto dto)
        {
            var result = await _courseService.SaveCourse(dto);

            return Ok(result);
        }
    }
}