using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class DataController : Controller
    {
        private ILogger<DataController> _logger;


        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }
        // GET: DataController
        public async Task<ActionResult> IndexAsync()
        {
            return RedirectToAction("Course");
        }
        public async Task<ActionResult> CourseAsync(DataViewModel dvm)
        {
            return View(dvm);
        }
        public async Task<ActionResult> LessonAsync(DataViewModel dvm)
        {
            var s = Request.Form;
            if(ModelString is not null)
                Model = JsonConvert.DeserializeObject<DataViewModel>(ModelString);
            if (Course.Name is not null)
            {
                Model.Course = Course;
            }
            else if(Lesson.Name is not null) 
            {
                if (Model.Course.Lessons is null)
                {
                    Model.Course.Lessons = new List<Lesson>();
                }
                Model.Course.Lessons.Add(Lesson);
            }
            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return View(dvm);
        }
        public async Task<ActionResult> ExamAsync(DataViewModel dvm)
        {
            var s = Request.Form;
            if(ModelString is not null)
                Model = JsonConvert.DeserializeObject<DataViewModel>(ModelString);
            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return View(dvm);
        }
        public async Task<ActionResult> QuestionAsync(DataViewModel dvm)
        {
            var s = Request.Form;
            if(ModelString is not null)
                Model = JsonConvert.DeserializeObject<DataViewModel>(ModelString);
            if(!string.IsNullOrEmpty(Exam.Name))
                Model.Course.Exam = Exam;
            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return View(dvm);
        }
        public async Task<ActionResult> AnswerAsync(DataViewModel dvm)
        {
            var s = Request.Form;
            if (ModelString is not null)
                Model = JsonConvert.DeserializeObject<DataViewModel>(ModelString);

            if(Question.Content is not null)
            {
                if(Model.Course.Exam.Questions is null)
                    Model.Course.Exam.Questions = new List<Question>();
                Model.Course.Exam.Questions.Add(Question);
                Model.CurrentQuestion = Model.Course.Exam.Questions.Count - 1;
            }
            else if(Answer.Content is not null)
            {
                if(Model.Course.Exam.Questions.ElementAt(Model.CurrentQuestion).Answers is null)
                    Model.Course.Exam.Questions.ElementAt(Model.CurrentQuestion).Answers = new List<Answer>();
                Model.Course.Exam.Questions.ElementAt(Model.CurrentQuestion).Answers.Add(Answer);
            }
            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return View(dvm);
        }
        public async Task<ActionResult> CertificateAsync(DataViewModel dvm)
        {
            var s = Request.Form;
            if (ModelString is not null)
                Model = JsonConvert.DeserializeObject<DataViewModel>(ModelString);


            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return View(dvm);
        }
        public async Task<ActionResult> EndAsync(DataViewModel dvm)
        {
            var s = Request.Form;
            if (ModelString is not null)
                Model = JsonConvert.DeserializeObject<DataViewModel>(ModelString);
            Model.Course.CertificateTemplate = CertificateTemplate;
            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm, Formatting.Indented);
            _logger.LogError("Creating {current_struct}", dvm.CurrentStruct);
            await creationService.CreateFullCourse(dvm);
            _logger.LogError("Finished creating");
            return RedirectToAction("Course");
        }

    }
}
