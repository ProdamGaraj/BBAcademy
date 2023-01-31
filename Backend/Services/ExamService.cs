using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Newtonsoft.Json;
using NLog;
using System.Linq;

namespace Backend.Services
{
    public class ExamService : IExamService
    {
        private ICertificateService cs;
        private ICourseRepository cr;
        private IUserRepository ur;
        private IQuestionRepository qr;

        public ExamService(ICertificateService cs, ICourseRepository cr, IUserRepository ur, IQuestionRepository qr)
        {
            this.cs = cs;
            this.cr = cr;
            this.ur = ur;
            this.qr = qr;
        }

        public async Task<IBaseResponce<Exam>> GetExamForUser(ExamViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                var course = new Course();
                var user = new User();
                if (vm.User is null || vm.Course is null)
                {
                    return new BaseResponse<Exam>()
                    {
                        Description = "User is null or Course is null",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                course = await cr.Get(vm.Course.Id);
                user = await ur.Get(vm.User.Id);

                if (user == null || long.Parse(user.PassedCoursesId) == vm.Course.Id)
                {
                    return new BaseResponse<Exam>()
                    {
                        Description = "User is null",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                List<long> passedExam = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                if (passedExam.Contains(course.Id))
                {
                    throw new Exception("This course was already passed");
                }
                Exam exam = (await cr.Get(course.Id)).Exam;
                return new BaseResponse<Exam>()
                {
                    Data = exam,
                    Description = $"Exam for User: {user.Name}.  \n was given at {DateTime.Now}",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<Exam>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<Exam>> CreateExamWithId(string description, string examType, List<long> ids)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                Exam exam = new Exam(description, examType, new List<Question>());
                foreach (long id in ids)
                {
                    exam.Questions.Add(await qr.Get(id));
                }
                return new BaseResponse<Exam>()
                {
                    Data = exam,
                    Description = $"Exam with  Id: {exam.Id}. \nAndname: {exam.Name} \nAnd description: {exam.Description} \n was created at {DateTime.Now}",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<Exam>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }
        public async Task<IBaseResponce<Exam>> CreateExamWithType(string description, string examType, Dictionary<QuestionType, int> keyValues)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                Exam exam = new Exam(description, examType, new List<Question>());
                IList<Question> list = await qr.GetConditionalType(keyValues);
                exam.Questions = list;
                return new BaseResponse<Exam>()
                {
                    Data = exam,
                    Description = $"Exam with type: {exam.ExamType}. \nAnd name:{exam.Name} \nAnd description:{exam.Description} \n was created at {DateTime.Now}",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<Exam>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }
        public async Task<IBaseResponce<bool>> Check(CourseViewModel vm)
        {
            var user = await ur.Get(vm.User.Id);
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                int currentGrade = 0;
                int currentQuestionGrade=0;
                if (vm.Exam is not null && vm.Exam.Questions is not null)
                {
                    var questions = vm.Exam.Questions;
                    foreach (var question in questions)
                    {
                        currentQuestionGrade = 0;
                        foreach (var answer in question.Answers)
                        {
                            if (answer.IsChosen && answer.IsCorrect)
                            {
                                currentQuestionGrade += answer.Cost;
                            }
                            else if (answer.IsChosen && !answer.IsCorrect)
                            {
                                currentQuestionGrade = 0;
                                break;
                            }
                            else if (!answer.IsChosen && answer.IsCorrect)
                            {
                                currentQuestionGrade = 0;
                                break;
                            }
                        }
                        currentGrade += currentQuestionGrade;
                    }
                }
                int roundingUp = 0;

                if (vm.Exam.PassingGrade == 0)
                {
                    vm.Exam.PassingGrade = 1;
                }

                currentGrade *= 100;
                double convertingToDecimal = currentGrade;
                convertingToDecimal /= vm.Exam.PassingGrade;
                convertingToDecimal %= 1;
                if (convertingToDecimal > 0)
                {
                    roundingUp++;
                }
                int percent = (currentGrade / vm.Exam.PassingGrade) + roundingUp;
                bool passed = currentGrade > vm.Exam.PassingGrade * 100;
                if (user.PassedCoursesId is null)
                    user.PassedCoursesId = "";
                List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                if (ids is null)
                {
                    ids = new List<long>();
                }
                if (passed)
                {
                    ids.Add(vm.IdCourse);
                    user.PassedCoursesId = JsonConvert.SerializeObject(ids);
                    await ur.Update(user);
                    await cs.CreateCertificate(vm);
                }

                return new BaseResponse<bool>() { Description = "Result of exam check", Data = passed, StatusCode = StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<bool>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
