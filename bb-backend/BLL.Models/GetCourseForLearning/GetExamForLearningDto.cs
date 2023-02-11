namespace BLL.Models.GetCourseForLearning;

public class GetExamForLearningDto
{
    public string Description { get; set; }

    public int MinimumPassingGrade { get; set; }

    public ICollection<GetQuestionForLearningDto> Questions { get; set; }
}