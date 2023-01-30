﻿using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Models;
using Backend.ViewModels;
using Newtonsoft.Json;
using Backend.Models.Enum;
using NLog;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class LessonService:ILessonService
    {
        private ICourseRepository cr;
        private IUserRepository ur;

        public LessonService(ICourseRepository cr, IUserRepository ur)
        {
            this.cr = cr;
            this.ur = ur;
        }

        public async Task<IBaseResponce<ICollection<Lesson>>> GetLessons(LessonViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
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
