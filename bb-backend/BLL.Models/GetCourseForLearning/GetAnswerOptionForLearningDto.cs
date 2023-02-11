namespace BLL.Models.GetCourseForLearning;

public class GetAnswerOptionForLearningDto
{
    public string Title { get; set; }

    public bool IsCorrect { get; set; }

    public int Weight { get; set; }
}