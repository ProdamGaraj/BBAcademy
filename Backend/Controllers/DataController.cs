using Backend.Models;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private ICourseRepository cr;
        private IExamRepository er;
        private ILessonRepository lr;
        private IQuestionRepository qr;
        private IAnswerRepository ar;

       [BindProperty]
        public long CourseId { get; set; }
        [BindProperty]
        public List<long> SelectedCheckBoxId { get; set; }
        [BindProperty]
        public List<long> SelectedRadioId { get; set; }

        public DataController(IExamRepository er, ICourseRepository cr, ILessonRepository lr, IQuestionRepository qr, IAnswerRepository ar)
        {
            this.cr = cr;
            this.er = er;
            this.lr = lr;
            this.qr = qr;
            this.ar = ar;
        }
        // GET: DataController
        public async Task<ActionResult> IndexAsync()
        {
            if (Exam is not null && Exam.Name is not null)
            {
                await er.Add(Exam);
            }
            if (Course is not null&& Course.Name is not null)
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
