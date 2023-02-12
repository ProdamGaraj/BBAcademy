using Infrastructure.Models.Enum;

namespace BLL.Models.SaveCourseEdit;

public class SaveQuestionEditDto
{
    public string MediaPath { get; set; }
    public string Content { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<SaveAnswerOptionEditDto> AnswerOptions { get; set; }
}