namespace BLL.Models.GetCourseForView;

public class GetExamForViewDto
{
    public string Description { get; set; }

    public int MinimumPassingGrade { get; set; }

    public ICollection<GetQuestionForViewDto> Questions { get; set; }
}