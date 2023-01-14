using Backend.Models;
using Backend.Services;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;

namespace Backend.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index(ExamViewModel vm)
        {
            return View();
        }
        public async Task<IActionResult> SendAllAsync(ExamViewModel vm)
        {
            ExamService es = new ExamService();
            var values = JObject.FromObject((await es.Check(vm)).Data).ToObject<Dictionary<string, object>>();
            if (values["passed"].Equals("true"))
            {

                UserRepository ur = new UserRepository();
                CourseRepository cor = new CourseRepository();
                User user = await ur.Get(vm.User.Id);
                Course course = await cor.Get(vm.Course.Id);
                CertificateService cs = new CertificateService();
                Certificate certificate = (await cs.CreateCertificate(user, course, true)).Data;
                CertificateRepository cr = new CertificateRepository();
                cr.Add(certificate);
                return Redirect("~/Ending");
            }
            else
                return View();
        }
    }
}
