using Backend.Models;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog;

namespace Backend.Services
{
    public class CourseService
    {
        //public async Task<IBaseResponce<AccountViewModel>> GetAllCourses(CourseViewModel vm)
        //{
        //    CourseRepository cr = new CourseRepository();
        //    List<Course> allCourses = new List<Course>();
        //    allCourses.AddRange(await cr.GetAll());
        //    return new BaseResponse<AccountViewModel>() { Data = , Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
        //}

        public async Task<IBaseResponce<AccountViewModel>> GetCourses(CourseViewModel vm)
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            UserRepository ur = new UserRepository();
            var user = await ur.Get(vm.User.Id);
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                CourseRepository cr = new CourseRepository();
                List<Course> allCourses = new List<Course>();
                List<Course> passedCourses = new List<Course>();
                allCourses.AddRange(await cr.GetAll());

                foreach (Course course in user.BoughtCourses)
                {
                    allCourses.Remove(course);
                }
                if (!user.PassedCoursesId.IsNullOrEmpty())
                {
                    List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);

                    foreach (long id in ids)
                    {
                        passedCourses.Add(allCourses.First(x => x.Id == id));
                        allCourses.Remove(passedCourses.Last());
                    }
                }
                return new BaseResponse<AccountViewModel>() { Data = new AccountViewModel { AllCourses = allCourses, EndedCourses = passedCourses, BoughtCourses = user.BoughtCourses.ToList() }, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<AccountViewModel>() { Data = null, Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace, StatusCode = Models.Enum.StatusCode.InternalServerError };
            }
        }
        public async Task<IBaseResponce<Course>> GetCourse(BuyViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                CourseRepository cr = new CourseRepository();
                UserRepository ur = new UserRepository();
                var course = await cr.Get(vm.Course.Id);
                var user = await ur.Get(vm.User.Id);
                List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                if (!user.BoughtCourses.Contains(course))
                    return new BaseResponse<Course>() { Description = "You haven`t buy this course yet", StatusCode = Models.Enum.StatusCode.InternalServerError };
                return new BaseResponse<Course>() { Data = await cr.Get(vm.Course.Id), Description = "Get course for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<Course>()
                {
                    Data = null,
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}
