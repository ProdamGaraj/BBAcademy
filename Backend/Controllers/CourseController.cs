using Backend.Models;
using Backend.Models.Enum;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Signing;

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly IAccountService accountService;

        [BindProperty]
        public long CourseId { get; set; }
        [BindProperty]
        public List<long> SelectedCheckBoxId { get; set; }
        [BindProperty]
        public List<long> SelectedRadioId { get; set; }

        public CourseController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(CourseViewModel vm)
        {
            List<long> ids = new List<long>();
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if (TempData["idCourse"] is not null)
            {
                vm.IdCourse = long.Parse(TempData["idCourse"].ToString());
            }
            if (TempData["currentLesson"] is not null)
            {
                vm.CurrentLesson = int.Parse(TempData["currentLesson"].ToString());
            }
            CourseService cs = new CourseService();
            vm = (await cs.GetCourse(vm)).Data;
            if (vm.User.PassedCoursesId is not null)
            {
                ids = JsonConvert.DeserializeObject<List<long>>(vm.User.PassedCoursesId);
                if (ids.Contains(vm.IdCourse) && vm.CurrentLesson >= vm.AllLessons.Count)
                {
                    vm.CurrentLesson--;
                    TempData["currentLesson"] = vm.CurrentLesson.ToString();
                }
            }
            if (vm.CurrentLesson < 0)
            {
                TempData["currentLesson"] = "0";
                vm.CurrentLesson = 0;
            }
            return View(vm);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            CourseService cs = new CourseService();
            CourseViewModel cvm = new CourseViewModel()
            {
                User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data,
                IdCourse = CourseId
            };
            cvm = (await cs.GetCourse(cvm)).Data;
            TempData["idCourse"] = CourseId.ToString();

            foreach (Question question in cvm.Exam.Questions)
            {
                if (question.QuestionType.Equals(QuestionType.TextOneAnswer) || question.QuestionType.Equals(QuestionType.MediaOneAnswer))
                {
                    if (question.Answers is not null)
                    {
                        foreach (Answer answer in question.Answers)
                        {
                            if (SelectedRadioId.Contains(answer.Id))
                            {
                                answer.IsChosen = true;
                            }
                        }
                    }
                }
                else if (question.QuestionType.Equals(QuestionType.TextManyAnswers) || question.QuestionType.Equals(QuestionType.MediaManyAnswers))
                {
                    if (question.Answers is not null)
                    {
                        foreach (Answer answer in question.Answers)
                        {
                            if (SelectedCheckBoxId.Contains(answer.Id))
                            {
                                answer.IsChosen = true;
                            }
                        }
                    }
                }
            }
            ExamService es = new ExamService();
            if ((await es.Check(cvm)).Data)
            {
                CertificateService cers = new CertificateService();
                return Redirect($"/Course/NextLesson/{CourseId}/{cvm.AllLessons.Count}/{cvm.AllLessons.Count}");
            }
            else
            {
                //TODO:Добавить переход на страницу провала
                return Redirect($"/Course/NextLesson/{CourseId}/{cvm.AllLessons.Count - 1}/{cvm.AllLessons.Count}");

            }

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

    }
}