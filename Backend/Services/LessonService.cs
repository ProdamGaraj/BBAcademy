using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Models;
using Backend.ViewModels;
using Newtonsoft.Json;
using Backend.Models.Enum;
using NLog;

namespace Backend.Services
{
    public class LessonService
    {
        public async Task<IBaseResponce<ICollection<Lesson>>> GetLessons(LessonViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                List<long> ids = JsonConvert.DeserializeObject<List<long>>(vm.User.PassedCoursesId);
                if (ids.Contains(vm.Course.Id))
                    throw new Exception("This course was already passed");
                if (vm.User.BoughtCourses.Contains(vm.Course))
                    throw new Exception("This course was not bought");

                return new BaseResponse<ICollection<Lesson>>() { Data = vm.Course.Lessons, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<ICollection<Lesson>>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
        }
        }
    }
}
