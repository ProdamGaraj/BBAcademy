using Infrastructure.Models.Enum;

namespace BLL.Models.Save;

public class SaveLessonDto
{
    public LessonContentType LessonContentType { get; set; }

    public string Content { get; set; }

    public string MediaContentPath { get; set; }
}