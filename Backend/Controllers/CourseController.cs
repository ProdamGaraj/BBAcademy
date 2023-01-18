using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Backend.Controllers
{
    public class CourseController : Controller
    {

        private readonly IAccountService accountService;

        public CourseController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
		public async Task<IActionResult> Index(CourseViewModel vm)
        {
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if(TempData["idCourse"] is not null)
                vm.IdCourse = long.Parse(TempData["idCourse"].ToString());
            if(TempData["currentLesson"] is not null)
                vm.CurrentLesson = int.Parse(TempData["currentLesson"].ToString());

            CourseService cs = new CourseService();
            vm = (await cs.GetCourse(vm)).Data;
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Buy(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CourseService cs = new CourseService();//TODO:Payment
                vm = (await cs.BuyCourse(vm)).Data;
                return RedirectToAction("Index", "Course");

            }
            return RedirectToAction("Index", "Course");
        }
        //[HttpPost]
        //public async Task<IActionResult> Buy(CourseViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CourseService cs = new CourseService();//TODO:Payment
        //        vm = (await cs.BuyCourse(vm)).Data;
        //        return RedirectToAction("Course", "Index");
        //
        //    }
        //    return RedirectToAction("Course", "Index");
        //    return View(vm);
        //}
        [HttpPost]
        public async Task<IActionResult> NextLesson(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if(vm.CurrentLesson + 1 != vm.AllLessons.Count)
                    vm.CurrentLesson = vm.CurrentLesson + 1;
            }
            return RedirectToAction("Index", vm);
        }
        [HttpPost]
        public async Task<IActionResult> PrevLesson(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.CurrentLesson != 0)
                {
                    vm.CurrentLesson = vm.CurrentLesson - 1;
                    TempData["currentLesson"] = vm.CurrentLesson;
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet("Course/Exam")]
        public async Task<IActionResult> GetExam(CourseViewModel vm)
        {
            ExamService es = new ExamService();
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> SendAllAsync(ExamViewModel vm)
        {
            ExamService es = new ExamService();
            var values = JObject.FromObject((await es.Check(vm)).Data).ToObject<Dictionary<string, object>>();
            if (values["passed"].Equals("true"))
            {
                CertificateService cs = new CertificateService();
                Certificate certificate = (await cs.CreateCertificate(vm)).Data;
                return Redirect("~/Ending");
            }
            else
                return View();
        }
    }
}