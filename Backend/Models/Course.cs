namespace Backend.Models
{
    public class Course : Entity
    {
        public string Description { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Exam Exam{ get; set; }
        public Certificate Certificate { get; set; }
        public CourseType CourseType { get; set; }
    }
}