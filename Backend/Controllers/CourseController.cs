﻿using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class CourseController : Controller
    {
        public CourseController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            List<string> answeredQuestionIds = new List<string>();
            foreach (var item in Request.Form.Keys.Skip(1))
            {
                answeredQuestionIds.Add(Request.Form[item]);
            }

            List<string> answerIds = new List<string>();
            foreach (var item in answeredQuestionIds)
            {
                answerIds.AddRange(item.Split(","));
            }

            CourseViewModel cvm = new CourseViewModel()
            {
                User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data,
                IdCourse = CourseId
            };
            cvm = (await cs.GetCourse(cvm)).Data;
            foreach (Question question in cvm.Exam.Questions)
            {
                //if (question.QuestionType.Equals(QuestionType.TextOneAnswer) || question.QuestionType.Equals(QuestionType.MediaOneAnswer))
                //{
                if (question.Answers is not null)
                {
                    var i = 0;
                    foreach (Answer answer in question.Answers)
                    {
                        if (answerIds.Contains(answer.Id.ToString()))
                        {
                            answer.IsChosen = true;
                        }

                        i++;
                    }
                }
                //}
                //else if (question.QuestionType.Equals(QuestionType.TextManyAnswers) || question.QuestionType.Equals(QuestionType.MediaManyAnswers))
                //{
                //    if (question.Answers is not null)
                //    {
                //        foreach (Answer answer in question.Answers)
                //        {
                //            if (SelectedAnswers.Contains(answer.Id.ToString()))
                //            {
                //                answer.IsChosen = true;
                //            }
                //        }
                //    }
                //}
            }

            if ((await es.Check(cvm)).Data)
            {
                return Redirect($"/Course/NextLesson/{CourseId}/{cvm.AllLessons.Count + 1}/{cvm.AllLessons.Count}");
            }
            else
            {
                //TODO:Добавить переход на страницу провала
                return Redirect($"/Course/NextLesson/{CourseId}/{cvm.AllLessons.Count - 1}/{cvm.AllLessons.Count}");
            }
        }

        //[HttpGet("Buy/{id}")]
        //public async Task<IActionResult> Buy(string id)
        //{
        //
        //    var cvm = new CourseViewModel();
        //    cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
        //    cvm.IdCourse = long.Parse(id);
        //    TempData["idCourse"] = cvm.IdCourse.ToString();
        //    if (cvm.User.BoughtCourses is not null)
        //    {
        //        List<long> boughtCourses = JsonConvert.DeserializeObject<List<long>>(cvm.User.BoughtCourses);
        //
        //        if (boughtCourses.Contains(cvm.IdCourse))
        //        {
        //            return RedirectToAction("Index", "Course");
        //        }
        //    }
        //    //TODO:Payment
        //    await cs.BuyCourses(cvm);
        //    return RedirectToAction("Index", "Course");
        //}
        [HttpGet("GoToCourse/{id}")]
        public async Task<IActionResult> GoToCourse(string id)
        {
            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            cvm.IdCourse = long.Parse(id);
            return RedirectToAction($"Index", new {courseId = id, lessonId = 0});
            ;
        }

        [HttpGet("InCart/{id}")]
        public async Task<IActionResult> InCart(string id)
        {
            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            cvm.IdCourse = long.Parse(id);
            TempData["idCourse"] = cvm.IdCourse.ToString();
            await cs.PutInCartCourse(cvm);
            return RedirectToAction("Index", "Account");
        }

        //[HttpPost]
        //public async Task<IActionResult> Buy(CourseViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CourseService cs = new CourseService();//TODO:Payment
        //        vm = (await cs.BuyCourses(vm)).Data;
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
            return RedirectToAction($"Index", new {courseId = courseId, lessonId = ++id});
        }

        [HttpGet("PrevLesson/{courseId}/{id}/{count}")]
        public async Task<IActionResult> PrevLesson(long courseId, long id, long count)
        {
            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if (--id < 0)
            {
                id++;
            }

            return RedirectToAction($"Index", new {courseId = courseId, lessonId = id});
        }

        [HttpGet("Course/Exam")]
        public async Task<IActionResult> GetExam(CourseViewModel vm)
        {
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            return View(vm);
        }

        [HttpGet("ChangeLang/{id}")]
        public async Task<IActionResult> ChangeLang(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User user = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
                user.Lang = id;
                await ur.Update(user);
            }

            TempData["lang"] = id;
            return RedirectToAction("");
        }

        public async Task<IActionResult> Buy()
        {
            throw new NotImplementedException();
        }
    }
}