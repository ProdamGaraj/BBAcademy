using Infrastructure.Models.Enum;

namespace BLL.Models.GetCourseForView;

public class GetLessonForViewDto
{
    public LessonContentType LessonContentType { get; set; }

    public string Content { get; set; }

    public string MediaContentPath { get; set; }
}