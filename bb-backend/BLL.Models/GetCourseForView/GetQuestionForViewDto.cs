using Infrastructure.Models.Enum;

namespace BLL.Models.GetCourseForView;

public class GetQuestionForViewDto
{
    public string MediaPath { get; set; }
    public string Content { get; set; }
    public QuestionType QuestionType { get; set; }
    public ICollection<GetAnswerOptionForViewDto> AnswerOptions { get; set; }
}