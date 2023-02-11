using BLL.Models.GetExamForTesting;
using BLL.Models.SaveCourseExamResults;

namespace BLL.ExamService;

public interface IExamService
{
    Task<bool> SaveCourseExamResults(long userId, SaveCourseExamResultsDto dto);
}