using BLL.Models.GetCourseForLearning;
using BLL.Models.GetCoursesForCart;
using BLL.Models.GetCoursesForDashboard;
using BLL.Models.SaveCourseEdit;
using Infrastructure.Models;

namespace BLL.CourseService
{
    public interface ICourseService
    {
        Task<GetCourseForLearningDto> GetForLearning(long courseId, long userId);
        Task<long> SaveCourseEdit(SaveCourseEditDto editDto);
        Task<ICollection<CourseForDashboardDto>> GetCoursesForDashboard(long userId);
        Task<ICollection<CourseForCartDto>> GetCartedCoursesForUser(long userId);
    }
}