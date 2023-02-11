namespace BLL.Models.SaveCourseExamResults;

public class SaveCourseExamResultsDto
{
    public long CourseId { get; set; }

    public ICollection<SaveQuestionDto> Questions { get; set; }
}