using Backend.Models.Interfaces;
using Backend.ViewModels;

namespace Backend.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IBaseResponce<AccountViewModel>> GetCourses(CourseViewModel vm);
        Task<IBaseResponce<CourseViewModel>> GetCourse(CourseViewModel vm);
        Task<IBaseResponce<bool>> PutInCartCourse(CourseViewModel vm);
        Task<IBaseResponce<bool>> RemoveCartCourse(CourseViewModel vm);
        Task<IBaseResponce<CourseViewModel>> BuyCourse(CourseViewModel vm);
    }
}