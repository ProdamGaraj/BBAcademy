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
        public Certificate Certificate { get; set; }
        [BindProperty]
        public string ModelString { get; set; }
        public DataViewModel Model { get; set; } = new DataViewModel();
        private ICourseRepository cr;
        private IExamRepository er;
        private ILessonRepository lr;
        private IQuestionRepository qr;
        private IAnswerRepository ar;
        private ICertificateRepository cer;

        [BindProperty]
        public long CurrentId { get; set; }

        public DataController(IExamRepository er, ICourseRepository cr, ILessonRepository lr, IQuestionRepository qr, IAnswerRepository ar, ICertificateRepository cer)
        {
            this.cr = cr;
            this.er = er;
            this.lr = lr;
            this.qr = qr;
            this.ar = ar;
            this.cer = cer;
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
            Model.Certificate = Certificate;
            dvm = Model;
            dvm.CurrentStruct = null;
            dvm.CurrentStruct = JsonConvert.SerializeObject(dvm);
            return RedirectToAction("Course");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (Exam is not null && Exam.Name is not null)
            {
                await er.Add(Exam);
            }
            if (Course is not null && Course.Name is not null)
            {
                await cr.Add(Course);
            }
            if (Lesson is not null && Lesson.Name is not null)
            {
                await lr.Add(Lesson);
            }
            if (Question is not null && Question.Name is not null)
            {
                await qr.Add(Question);
            }
            if (Answer is not null && Answer.Name is not null)
            {
                await ar.Add(Answer);
            }

            return View();
        }
    }
}
