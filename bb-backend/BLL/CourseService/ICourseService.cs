using BLL.Models.CourseForCart;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForDashboard;
using BLL.Models.Save;
using Infrastructure.Models;

namespace BLL.CourseService
{
    public interface ICourseService
    {
        Task<ICollection<Course>> GetCourses(long userId);
        Task<GetCourseForLearningDto> GetForLearning(long courseId);
        Task<long> SaveCourse(SaveCourseDto dto);
        Task<ICollection<CourseForDashboardDto>> GetCoursesForDashboard(long userId);
        Task<ICollection<CourseForCartDto>> GetCartedCoursesForUser(long userId);
    }
}