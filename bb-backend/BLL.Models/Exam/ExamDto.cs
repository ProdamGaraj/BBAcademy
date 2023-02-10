using Infrastructure.Models;

namespace BLL.Models.Exam;

public class ExamDto
{
    public ICollection<QuestionDto> Questions { get; set; }
}