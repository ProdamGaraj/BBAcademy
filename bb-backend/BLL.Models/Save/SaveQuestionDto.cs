using Infrastructure.Models.Enum;

namespace BLL.Models.Save;

public class SaveQuestionDto
{
    public string MediaPath { get; set; }
    public string Content { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<SaveAnswerOptionDto> AnswerOptions { get; set; }
}