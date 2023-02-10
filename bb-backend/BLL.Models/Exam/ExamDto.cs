using Infrastructure.Models;

namespace BLL.Models.Exam;

public class ExamDto
{
    public long UserId { get; set; }
    public ICollection<QuestionDto> Questions { get; set; }
}