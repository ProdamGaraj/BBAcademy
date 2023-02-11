namespace BLL.Models.GetCourseForLearning;

public class GetExamForLearningDto
{
    public string Title { get; set; }

    public int MinimumPassingGrade { get; set; }

    public ICollection<GetQuestionForLearningDto> Questions { get; set; }
}