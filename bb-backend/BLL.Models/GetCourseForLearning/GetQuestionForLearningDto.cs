using Infrastructure.Models.Enum;

namespace BLL.Models.GetCourseForLearning;

public class GetQuestionForLearningDto
{
    public string MediaPath { get; set; }
    public string Content { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<GetAnswerOptionForLearningDto> AnswerOptions { get; set; }
}