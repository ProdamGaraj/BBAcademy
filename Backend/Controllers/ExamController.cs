using Backend.Models;
using Backend.Models.Interfaces;
using Backend.Services;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;

namespace Backend.Controllers
{
    public class ExamController : Controller
    {
        private readonly IAccountService accountService;

        public ExamController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        public async Task<IActionResult> Index(ExamViewModel vm)
        {
            ExamService es = new ExamService();
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            vm.Course = (await new CourseService().GetCourses(new CourseViewModel() { User = vm.User, IdCourse= 0})).Data.BoughtCourses.Find(x => x.Exam.Questions == vm.Questions);
            vm.Questions = (await new CourseService().GetCourse(new CourseViewModel() { User = vm.User,  })).Data.Exam.Questions.ToList();
            
            return View(vm);
        }
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
