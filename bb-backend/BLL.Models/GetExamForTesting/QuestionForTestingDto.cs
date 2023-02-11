using Infrastructure.Models.Enum;

namespace BLL.Models.GetExamForTesting;

public class QuestionForTestingDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<AnswerOptionForTestingDto> AnswerOptions { get; set; }
}