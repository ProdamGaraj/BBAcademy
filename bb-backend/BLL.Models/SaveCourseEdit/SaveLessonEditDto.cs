using Infrastructure.Models.Enum;

namespace BLL.Models.SaveCourseEdit;

public class SaveLessonEditDto
{
    public LessonContentType LessonContentType { get; set; }

    public string Content { get; set; }

    public string MediaContentPath { get; set; }
}