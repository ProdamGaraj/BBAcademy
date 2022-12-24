namespace Backend.Models
{
    public class Course : Entity
    {
        public string Duration { get; set; }
        public string Description { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Exam Exam{ get; set; }
        public Course(string duration, string description, List<Lesson> lessons, Exam exam)
        {
            Duration = duration;
            Description = description;
            Lessons = lessons;
            Exam = exam;
        }
    }
}