using Backend.Models;

namespace Backend.ViewModels
{
    public class CourseViewModel
    {
        public User User { get; set; }
        public List<Lesson> AllLessons { get; set; }
        public Lesson CurrentLesson { get; set; }
    }
}
