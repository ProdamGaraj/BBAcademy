using Backend.Models;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    public class DataController : Controller
    {
        [BindProperty]
        public long CurrentId { get; set; }

        [BindProperty]
        public Exam Exam { get; set; }
        [BindProperty]
        public Course Course { get; set; }
        [BindProperty]
        public Lesson Lesson { get; set; }
        [BindProperty]
        public Question Question { get; set; }
        [BindProperty]
        public Answer Answer { get; set; }
        [BindProperty]
        public CertificateTemplate CertificateTemplate { get; set; }
        [BindProperty]
        public string ModelString { get; set; }
        public DataViewModel Model { get; set; } = new DataViewModel();
        private readonly ICreationService creationService;


        public DataController(ICreationService creationService)
        {
            this.creationService = creationService;
          
        }
        // GET: DataController
        public async Task<ActionResult> IndexAsync()
        {
            
            return View();
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
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return RedirectToAction("Course");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            var dataViewModel = Model;
            creationService.CreateFullCourse(dataViewModel);
            return View();
        }
    }
}
