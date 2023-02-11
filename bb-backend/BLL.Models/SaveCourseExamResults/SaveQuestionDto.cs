namespace BLL.Models.SaveCourseExamResults;

public class SaveQuestionDto
{
    public long Id { get; set; }

    public IList<long> SelectedAnswerIds { get; set; }
}