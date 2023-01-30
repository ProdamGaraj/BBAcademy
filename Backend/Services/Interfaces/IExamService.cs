using Backend.Models.Interfaces;
using Backend.Models;
using Backend.ViewModels;
using Backend.Models.Enum;

namespace Backend.Services.Interfaces
{
    public interface IExamService
    {
        Task<IBaseResponce<Exam>> GetExamForUser(ExamViewModel vm);
        Task<IBaseResponce<Exam>> CreateExamWithId(string description, string examType, List<long> ids);
        Task<IBaseResponce<Exam>> CreateExamWithType(string description, string examType, Dictionary<QuestionType, int> keyValues);
        Task<IBaseResponce<bool>> Check(CourseViewModel vm);

    }
}
