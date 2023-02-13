using BLL.CourseService;
using BLL.Models.SaveCourseEdit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    [ResponseCache(NoStore = true)]
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
        [Authorize]
        public async Task<IActionResult> SaveCourse([FromBody] SaveCourseEditDto dto)
        {
            var result = await _courseService.SaveCourseEdit(dto);

            return Ok(result);
        }
    }
}