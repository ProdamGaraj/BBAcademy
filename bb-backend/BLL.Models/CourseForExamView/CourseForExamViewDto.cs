using BLL.Models.Exam;

namespace BLL.Models.CourseForExamView;

public class CourseForExamViewDto
{
    public long CourseID { get; set; }
    public ExamDto Exam { get; set; }
}