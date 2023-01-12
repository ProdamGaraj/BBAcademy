using Backend.Models;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Repository;
using Backend.ViewModels;
using Newtonsoft.Json;
using NLog;

namespace Backend.Services
{
    public class CourseService
    {
        public async Task<IBaseResponce<object>> GetCourses(CourseViewModel vm)
        {
            UserRepository ur = new UserRepository();
            var user = await ur.Get(vm.User.Id);
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                CourseRepository cr = new CourseRepository();
                List<Course> allCourses = new List<Course>();
                allCourses.AddRange(await cr.GetAll());
                foreach (Course course in user.BoughtCourses)
                {
                    allCourses.Remove(course);
                }
                List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                List<Course> passedCourses = new List<Course>();
                foreach (long id in ids)
                {
                    passedCourses.Add(allCourses.First(x => x.Id == id));
                    allCourses.Remove(passedCourses.Last());
                }
                return new BaseResponse<object>() { Data = new { allCourses, passedCourses, user.BoughtCourses }, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<object>() { Data = null, Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace, StatusCode = Models.Enum.StatusCode.InternalServerError };
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
                if (ids.Contains(course.Id))
                    throw new Exception("This course was already passed");
                if (!user.BoughtCourses.Contains(course))
                    throw new Exception("This course was not bought");
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
