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
		public IActionResult Course()
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
        [HttpPost]
        public async Task<IActionResult> NextLesson(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.CurrentLesson = vm.AllLessons.ElementAt(vm.AllLessons.IndexOf(vm.CurrentLesson)+1);
            }
            return View(vm);
        }
    }
}