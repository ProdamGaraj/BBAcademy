using Infrastructure.Models.Enum;

namespace BLL.Models.GetCourseForLearning;

public class GetLessonForLearningDto
{
    public string Title { get; set; }

    public LessonContentType LessonContentType { get; set; }

    public string Content { get; set; }

    public string MediaContentPath { get; set; }
}