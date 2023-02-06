using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using NLog;

namespace Backend.Services
{
    public class CreationService : ICreationService
    {
        private ICourseRepository courseRepository;
        private IExamRepository examRepository;
        private ILessonRepository lessonRepository;
        private IQuestionRepository questionRepository;
        private IAnswerRepository answerRepository;
        private ICertificateRepository certificateRepository;
        private ICertificateTemplateRepository certificateTemplateRepository;


        public CreationService(IExamRepository examRepository, ICourseRepository courseRepository, ILessonRepository lessonRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, ICertificateRepository certificateRepository, ICertificateTemplateRepository certificateTemplateRepository)
        {
            this.courseRepository = courseRepository;
            this.examRepository = examRepository;
            this.lessonRepository = lessonRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.certificateRepository = certificateRepository;
            this.certificateTemplateRepository = certificateTemplateRepository;
        }

        public async Task<IBaseResponce<DataViewModel>> CreateFullCourse(DataViewModel dataViewModel)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            Exam Exam = null;
            Course Course = null;
            ICollection<Lesson> Lessons = null;
            ICollection<Question> Questions = null;
            ICollection<ICollection<Answer>> Answers = null;
            CertificateTemplate CertificateTemplate = null;
            try
            {
                if (dataViewModel is not null)
                {
                    Exam = dataViewModel.Course.Exam;
                    Course = dataViewModel.Course;
                    Lessons = dataViewModel.Course.Lessons;
                    Questions = dataViewModel.Course.Exam.Questions;
                    foreach (var item in Questions)
                    {
                        Answers.Add(item.Answers);
                    }
                    CertificateTemplate = dataViewModel.Course.CertificateTemplate; //It`s a template adding comming soon
                }
                if (Course is not null && Course.Name is not null)
                {
                    courseRepository.Add(Course);
                }
                if (Exam is not null && Exam.Name is not null)
                {
                    examRepository.Add(Exam);
                }
                if (Lessons is not null && Lessons.Count >= 1)
                {
                    lessonRepository.AddRange(Lessons);
                }
                if (Questions is not null && Questions.Count >= 1)
                {
                    questionRepository.AddRange(Questions);
                }
                if (Answers is not null)
                {
                    foreach (var item in Answers)
                    {
                        if (item.Count >= 1 && item.All(x => x.Name is not null))
                        {
                            answerRepository.AddRange(item);
                        }
                    }
                }
                if (CertificateTemplate is not null && CertificateTemplate.Name is not null)
                {
                    certificateTemplateRepository.Add(CertificateTemplate);
                }
                return new BaseResponse<DataViewModel>()
                {
                    Data = dataViewModel,
                    Description = $"Course has been added succsesfully",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<DataViewModel>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
