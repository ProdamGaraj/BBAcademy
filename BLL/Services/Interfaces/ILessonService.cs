using Infrastructure.Models;

namespace BLL.Services.Interfaces
{
    public interface ILessonService
    {
        Task<ICollection<Lesson>> GetLessons(long courseId);
    }
}
