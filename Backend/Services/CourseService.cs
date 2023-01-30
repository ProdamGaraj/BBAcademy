using Backend.Models;
using Backend.Models.Interfaces;
using Backend.Models.Responce;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;

namespace Backend.Services
{
    public class CourseService:ICourseService
    {
        //public async Task<IBaseResponce<AccountViewModel>> GetAllCourses(CourseViewModel vm)
        //{
        //    CourseRepository cr = new CourseRepository();
        //    List<Course> allCourses = new List<Course>();
        //    allCourses.AddRange(await cr.GetAll());
        //    return new BaseResponse<AccountViewModel>() { Data = , Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
        //}
        //private ICertificateRepository cr;
        private IUserRepository ur;
        private ICourseRepository cr;
        private IQuestionRepository qr;
        private IExamRepository er;
        public CourseService(IUserRepository ur, ICourseRepository cr, IQuestionRepository qr, IExamRepository er)
        {
            this.ur = ur;
            this.cr = cr;
            this.qr = qr;
            this.er = er;
        }

        public async Task<IBaseResponce<AccountViewModel>> GetCourses(CourseViewModel vm)
        {
            AccountViewModel accountViewModel = new AccountViewModel();

            var user = new User();
            if (vm.User is not null)
            {
                user = await ur.Get(vm.User.Id);
            }
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                List<Course> allCourses = new List<Course>();
                List<Course> passedCourses = new List<Course>();
                List<Course> boughtCourses = new List<Course>();
                List<Course> inKartCourses = new List<Course>();
                allCourses.AddRange(await cr.GetAll());
                if (!user.PassedCoursesId.IsNullOrEmpty())
                {
                    List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);

                    if (ids is not null)
                    {
                        foreach (long id in ids)
                        {
                            try
                            {
                                passedCourses.Add(allCourses.First(x => x.Id == id));
                                allCourses.Remove(passedCourses.Last());
                            }
                            catch (Exception ex) { logger.Error(ex.Message); }
                        }
                    }
                }
                if (user.BoughtCourses is not null)
                {
                    List<long> boughtIds = JsonConvert.DeserializeObject<List<long>>(user.BoughtCourses);

                    if (boughtIds is not null)
                    {
                        foreach (long course in boughtIds)
                        {
                            try
                            {
                                boughtCourses.Add(allCourses.First(x => x.Id == course));
                                allCourses.Remove(boughtCourses.Last());
                            }
                            catch { }
                        }
                    }
                }

                if (user.InKartCourses is not null)
                {
                    List<long> kartIds = JsonConvert.DeserializeObject<List<long>>(user.InKartCourses);
                    if (kartIds is not null)
                    {
                        foreach (long course in kartIds)
                        {
                            try
                            {
                                inKartCourses.Add(allCourses.First(x => x.Id == course));
                                allCourses.Remove(inKartCourses.Last());
                            }
                            catch { }
                        }
                    }
                }
                return new BaseResponse<AccountViewModel>() { Data = new AccountViewModel { AllCourses = allCourses, InKartCourses = inKartCourses, EndedCourses = passedCourses, BoughtCourses = boughtCourses }, Description = "Get all courses for a user", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                return new BaseResponse<AccountViewModel>()
                {
                    Data = null,
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponce<CourseViewModel>> GetCourse(CourseViewModel vm)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                var course = await cr.Get(vm.IdCourse);
                var user = await ur.Get(vm.User.Id);
                vm.AllLessons = course.Lessons.ToList();
                if (vm.CurrentLesson == null)
                    vm.CurrentLesson = 0;
                if (course.Exam is not null)
                {
                    vm.Exam = await er.Get(course.Exam.Id);

                    List<Question> questions = new List<Question>();
                    if (vm.Exam is not null && vm.Exam.Questions is not null)
                    {

                        foreach (Question question in vm.Exam.Questions)
                        {
                            questions.Add(await qr.Get(question.Id));
                        }
                    }
                    vm.Exam.Questions = questions;

                }
                if (user.BoughtCourses is not null)
                {
                    List<long> boughtIds = JsonConvert.DeserializeObject<List<long>>(user.BoughtCourses);
                    if (boughtIds is not null)
                        if (boughtIds.Contains(course.Id))
                        {
                            vm.IsBought = true;
                            return new BaseResponse<CourseViewModel>() { Data = vm, Description = "Get course for a user", StatusCode = Models.Enum.StatusCode.OK };
                        }
                }
                vm.IsBought = false;
                return new BaseResponse<CourseViewModel>() { Data = vm, Description = "You haven`t buy this course yet", StatusCode = Models.Enum.StatusCode.OK };
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
        public async Task<IBaseResponce<bool>> PutInCartCourse(CourseViewModel vm)
        {
            try
            {
                List<long> ids = new List<long>();
                if (vm.User.InKartCourses is not null)
                    ids.AddRange(JsonConvert.DeserializeObject<List<long>>(vm.User.InKartCourses));
                if (ids is null)
                {
                    ids = new List<long>();
                }
                ids.Add(vm.IdCourse);
                vm.User.InKartCourses = JsonConvert.SerializeObject(ids);
                await ur.Update(vm.User);
                return new BaseResponse<bool>() { Data = true, Description = "Put in cart", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace,
                    StatusCode = Models.Enum.StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponce<bool>> RemoveCartCourse(CourseViewModel vm)
        {
            try
            {
                List<long> ids = new List<long>();
                if (vm.User.InKartCourses is not null)
                    ids.AddRange(JsonConvert.DeserializeObject<List<long>>(vm.User.InKartCourses));
                if (ids is null)
                {
                    ids = new List<long>();
                }
                ids.Remove(vm.IdCourse);
                vm.User.InKartCourses = JsonConvert.SerializeObject(ids);

                await ur.Update(vm.User);
                return new BaseResponse<bool>() { Data = true, Description = "Put in cart", StatusCode = Models.Enum.StatusCode.OK };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
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
                List<long> boughtIds = new List<long>();

                var course = await cr.Get(vm.IdCourse);
                var user = await ur.Get(vm.User.Id);
                vm.AllLessons = course.Lessons.ToList();
                vm.CurrentLesson = 0;
                if (user.PassedCoursesId is not null)
                {
                    List<long> ids = JsonConvert.DeserializeObject<List<long>>(user.PassedCoursesId);
                }
                if (user.BoughtCourses is not null)
                {
                    boughtIds = JsonConvert.DeserializeObject<List<long>>(user.BoughtCourses);

                    if (boughtIds.Contains(course.Id))
                        return new BaseResponse<CourseViewModel>() { Description = "You have already bought this course", StatusCode = Models.Enum.StatusCode.InternalServerError };

                }
                boughtIds.Add(course.Id);
                user.BoughtCourses = JsonConvert.SerializeObject(boughtIds);
                if (await ur.Update(user))
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
