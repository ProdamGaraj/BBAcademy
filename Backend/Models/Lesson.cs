namespace Backend.Models
{
    public class Lesson:Entity
    {
        public LessonType LesssonType { get; set; }
        public string Description { get; set; }
        public string TextContent { get; set; }
        public string MediaContentPath { get; set; }
        public Lesson(LessonType lesssonType, string description, string textContent, string mediaContentPath)
        {
            LesssonType = lesssonType;
            Description = description;
            TextContent = textContent;
            MediaContentPath = mediaContentPath;
        }
    }

}