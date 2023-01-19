using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Backend.Controllers
{
    [Route("[controller]")]
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
            if (TempData["idCourse"] is not null)
            {
                vm.IdCourse = long.Parse(TempData["idCourse"].ToString());
            }
            if (TempData["currentLesson"] is not null)
            {
                vm.CurrentLesson = int.Parse(TempData["currentLesson"].ToString());
            }
            if (vm.CurrentLesson < 0)
            {
                TempData["currentLesson"] = "0";
                vm.CurrentLesson = 0;
            }
            CourseService cs = new CourseService();
            vm = (await cs.GetCourse(vm)).Data;
            return View(vm);
        }

        [HttpGet("Buy/{id}")]
        public async Task<IActionResult> Buy(string id)
        {

            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            cvm.IdCourse = long.Parse(id);
            TempData["idCourse"] = cvm.IdCourse.ToString();
            if (cvm.User.BoughtCourses is not null)
            {
                List<long> boughtCourses = JsonConvert.DeserializeObject<List<long>>(cvm.User.BoughtCourses);

                if (boughtCourses.Contains(cvm.IdCourse))
                {
                    return RedirectToAction("Index", "Course");
                }
            }
            CourseService cs = new CourseService();//TODO:Payment
            await cs.BuyCourse(cvm);
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
        [HttpGet("NextLesson/{courseId}/{id}/{count}")]
        public async Task<IActionResult> NextLesson(long courseId, long id, long count)
        {
            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            TempData["idCourse"] = courseId.ToString();
            TempData["currentLesson"] = (++id).ToString();
            return RedirectToAction("Index");
        }
        [HttpGet("PrevLesson/{courseId}/{id}/{count}")]
        public async Task<IActionResult> PrevLesson(long courseId, long id, long count)
        {
            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            TempData["idCourse"] = courseId.ToString();
            if (--id < 0)
            {
                id++;
            }
            TempData["currentLesson"] = (id).ToString();

            return RedirectToAction("Index");
        }
        [HttpGet("Course/Exam")]
        public async Task<IActionResult> GetExam(CourseViewModel vm)
        {
            ExamService es = new ExamService();
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            return View(vm);
        }

        [HttpGet("Course/{id}/SendAllAsync")]
        public async Task<IActionResult> SendAllAsync(long id)
        {
            TempData["idCourse"] = id.ToString();
            CourseViewModel cvm = new CourseViewModel()
            {
                User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data,
                IdCourse= id
            };

            var cos = new CourseService();
            var vm = new ExamViewModel()
            {
                User = cvm.User,
                Course = (await cos.GetCourses(cvm)).Data.BoughtCourses.First(x => x.Id == cvm.IdCourse),
                Questions = cvm.Exam.Questions.ToList()
            };

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