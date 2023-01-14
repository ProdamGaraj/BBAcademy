using Backend.Models;
using Backend.Services;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CourseController : Controller
    {
		[HttpGet]
		public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Buy() => View();
        [HttpPost]
        public async Task<IActionResult> Buy(BuyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CourseService cs = new CourseService();
                vm.Course = (await cs.GetCourse(vm)).Data;
            }
            return View(vm);
        }
    }
}