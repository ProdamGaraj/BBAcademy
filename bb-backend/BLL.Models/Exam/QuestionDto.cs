using Infrastructure.Models.Enum;

namespace BLL.Models.Exam;

public class QuestionDto
{
    public long QuestionId { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<AnswerOptionDto> AnswerOptions { get; set; }
}