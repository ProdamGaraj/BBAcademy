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
                List<Course> boughtCourses = new List<Course>();
                allCourses.AddRange(await cr.GetAll());

                List<long> boughtIds = JsonConvert.DeserializeObject<List<long>>(user.BoughtCourses);

                foreach (long course in user.BoughtCourses)
                {
                    boughtCourses.Add(allCourses.First(x => x.Id == course));
                    allCourses.Remove(boughtCourses.Last());
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
                return new BaseResponse<AccountViewModel>() { Data = new AccountViewModel { AllCourses = allCourses, EndedCourses = passedCourses, BoughtCourses = boughtCourses }, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<AccountViewModel>() { Data = null, Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace, StatusCode = Models.Enum.StatusCode.InternalServerError };
            }
        }
        public async Task<IBaseResponce<CourseViewModel>> GetCourse(CourseViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                CourseRepository cr = new CourseRepository();
                UserRepository ur = new UserRepository();
                var course = await cr.Get(vm.IdCourse);
                var user = await ur.Get(vm.User.Id);
                vm.AllLessons = course.Lessons.ToList();
                if(vm.CurrentLesson == null)
                    vm.CurrentLesson = 0;
                vm.Exam = course.Exam;
                if (user.PassedCoursesId is not null)
                {
                    List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                }

                List<long> boughtIds = JsonConvert.DeserializeObject<List<long>>(user.BoughtCourses);

                if (!boughtIds.Contains(course.Id))
                {
                    vm.IsBought = false;
                    return new BaseResponse<CourseViewModel>() { Data = vm, Description = "You haven`t buy this course yet", StatusCode = Models.Enum.StatusCode.OK };
                }
                vm.IsBought = true;
                return new BaseResponse<CourseViewModel>() { Data = vm, Description = "Get course for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<CourseViewModel>()
                {
                    Data = null,
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponce<CourseViewModel>> BuyCourse(CourseViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                CourseRepository cr = new CourseRepository();
                UserRepository ur = new UserRepository();
                var course = await cr.Get(vm.IdCourse);
                var user = await ur.Get(vm.User.Id);
                vm.AllLessons = course.Lessons.ToList();
                vm.CurrentLesson = 0;
                if (user.PassedCoursesId is not null)
                {
                    List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                }

                List<long> boughtIds = JsonConvert.DeserializeObject<List<long>>(user.BoughtCourses);

                if (boughtIds.Contains(course.Id))
						return new BaseResponse<CourseViewModel>() { Description = "You have already bought this course", StatusCode = Models.Enum.StatusCode.InternalServerError };
                boughtIds.Add(course.Id);
                user.BoughtCourses = JsonConvert.SerializeObject(boughtIds);
                if(await ur.Update(user))
				    return new BaseResponse<CourseViewModel>() { Data = vm, Description = "Buy course for a user", StatusCode = Models.Enum.StatusCode.OK };
                else
                {
                    return new BaseResponse<CourseViewModel>()
                    {
                        Data = null,
                        Description = "Something went wrong while trying to add course to your account please contact our hotline",
                        StatusCode = Models.Enum.StatusCode.InternalServerError
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<CourseViewModel>()
                {
                    Data = null,
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}
