using BLL.Models.GetCourseForView;
using BLL.Models.Save;
using Infrastructure.Models;

namespace BLL.CourseService
{
    public interface ICourseService
    {
        Task<ICollection<Course>> GetCourses(long userId);
        Task<GetCourseForViewDto> GetFullInfoForView(long courseId);
        Task<long> SaveCourse(SaveCourseDto dto);
    }
}