using Infrastructure.Models;

namespace BLL.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ICollection<Course>> GetCourses(long userId);
        Task<Course> GetCourse(long courseId);
    }
}