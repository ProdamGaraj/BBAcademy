using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Repository;
using Backend.ViewModels;
using Newtonsoft.Json;
using NLog;

namespace Backend.Services
{
    public class ExamService
    {
        public async Task<IBaseResponce<Exam>> GetExamForUser(ExamViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {

                CourseRepository cr = new CourseRepository();
                UserRepository ur = new UserRepository();
                var course = await cr.Get(vm.Course.Id);
                var user = await ur.Get(vm.User.Id);

                if (user == null||long.Parse(user.PassedCoursesId) == vm.Course.Id)
                {
                    return new BaseResponse<Exam>()
                    {
                        Description = "User is null",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
                ExamRepository er = new ExamRepository();
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
                    QuestionRepository qr = new QuestionRepository();
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
                QuestionRepository qr = new QuestionRepository();
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
        public async Task<IBaseResponce<object>> Check(ExamViewModel vm)
        {
            UserRepository ur = new UserRepository();
            var user = await ur.Get(vm.User.Id);
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                int currentGrade = 0;
                foreach (Question question in vm.Course.Exam.Questions)
                {
                    currentGrade += question.Answers.FirstOrDefault(x => x.IsChosen == true && x.IsCorrect == true).Cost;
                }
                int roundingUp = 0;
                double convertingToDecimal = currentGrade;
                convertingToDecimal /= vm.Course.Exam.PassingGrade;
                if (convertingToDecimal > 0)
                {
                    roundingUp++;
                }
                currentGrade *= 100;
                int percent = currentGrade / vm.Course.Exam.PassingGrade + roundingUp;
                bool passed = currentGrade > vm.Course.Exam.PassingGrade;
                object ecr = new { percent = percent, passed = passed };
                if (user.PassedCoursesId is null)
                    user.PassedCoursesId = "";
                List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                if (passed)
                {
                    ids.Add(vm.Course.Id);
                    user.PassedCoursesId = JsonConvert.SerializeObject(ids);
                    await ur.Update(user);
                }
                CertificateService cs = new CertificateService();
                await cs.CreateCertificate(vm);

                return new BaseResponse<object>() { Description = "Result of exam check", Data = ecr, StatusCode = StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<object>() { 
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace, 
                    StatusCode = StatusCode.InternalServerError };
            }
        }
    }
}
