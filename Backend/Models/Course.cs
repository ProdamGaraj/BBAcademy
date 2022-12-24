namespace Backend.Models
{
    public class Course : Entity
    {
        public string Duration { get; set; }
        public string Description { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Exam Exam{ get; set; }

        public Course(string description, List<Lesson> lessons, Exam exam)
        {
            Description = description;
            Lessons = lessons;
            Exam = exam;
        }
    }
}