using Backend.Models.Enum;

namespace Backend.Models
{
    public class Lesson:Entity
    {
        public LessonType LessonType { get; set; }
        public string Description { get; set; }
        public string TextContent { get; set; }
        public string MediaContentPath { get; set; }
        public Lesson()
        {}
        public Lesson(LessonType lesssonType, string description, string textContent, string mediaContentPath, bool deleted=false)
        {
            LessonType = lesssonType;
            Description = description;
            TextContent = textContent;
            MediaContentPath = mediaContentPath;
            Deleted = deleted;
        }
    }

}