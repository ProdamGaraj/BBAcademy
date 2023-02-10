using BLL.Models.CourseForExamView;
using BLL.Models.Exam;

namespace BLL.ExamService;

public interface IExamService
{
    Task<bool> SolveExam(ExamDto dto);
    Task<CourseForExamViewDto> GetByCourse(long courseId);
}