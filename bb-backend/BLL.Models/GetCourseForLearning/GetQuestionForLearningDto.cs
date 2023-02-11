using Infrastructure.Models.Enum;

namespace BLL.Models.GetCourseForLearning;

public class GetQuestionForLearningDto
{
    public long Id { get; set; }
    
    public string MediaPath { get; set; }
    public string Title { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<GetAnswerOptionForLearningDto> AnswerOptions { get; set; }
}