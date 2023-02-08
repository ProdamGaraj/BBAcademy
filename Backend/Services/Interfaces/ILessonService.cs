using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Interfaces;
using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services.Interfaces
{
    public interface ILessonService
    {
        Task<IBaseResponce<ICollection<Lesson>>> GetLessons(LessonViewModel vm);
    }
}
