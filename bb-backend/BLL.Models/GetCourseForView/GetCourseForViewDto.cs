namespace BLL.Models.GetCourseForView;

public class GetCourseForViewDto
{
    public string MediaPath { get; set; }

    public float DurationHours { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public GetExamForViewDto Exam { get; set; }

    public ICollection<GetQuestionForViewDto> Lessons { get; set; }
}