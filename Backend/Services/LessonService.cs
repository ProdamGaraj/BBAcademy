using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Models;
using Backend.ViewModels;
using Newtonsoft.Json;
using Backend.Models.Enum;
using Backend.Services.Repository.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backend.Services
{
    public class LessonService:ILessonService
    {
        private ICourseRepository cr;
        private IUserRepository ur;
        private ILogger<LessonService> _logger;

        public LessonService(ICourseRepository cr, IUserRepository ur, ILogger<LessonService> logger)
        {
            this.cr = cr;
            this.ur = ur;
            _logger = logger;
        }

        public async Task<IBaseResponce<ICollection<Lesson>>> GetLessons(LessonViewModel vm)
        {
            try
            {
                var course = await cr.Get(vm.Course.Id);
                var user = await ur.Get(vm.User.Id);
                List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                if (ids.Contains(course.Id))
                    throw new Exception("This course was already passed");
                //if (!user.BoughtCourses.Contains(course))
                //    throw new Exception("This course was not bought");

                return new BaseResponse<ICollection<Lesson>>() { Data = course.Lessons, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<ICollection<Lesson>>()
                {
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
