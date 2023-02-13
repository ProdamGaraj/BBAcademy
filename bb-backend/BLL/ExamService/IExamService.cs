using BLL.Models.GetExamForTesting;
using BLL.Models.SaveCourseExamResults;

namespace BLL.ExamService;

public interface IExamService
{
    Task<SaveCourseExamResultsResult> SaveCourseExamResults(long userId, SaveCourseExamResultsDto dto);
}