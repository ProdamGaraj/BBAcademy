using Infrastructure.Models.Enum;

namespace Infrastructure.Models
{
    public class Lesson : Entity
    {
        public long CourseId { get; set; }

        public virtual Course Course { get; set; }

        public LessonContentType LessonContentType { get; set; }

        public string Content { get; set; }

        public string MediaContentPath { get; set; }
    }
}