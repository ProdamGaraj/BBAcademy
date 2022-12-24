namespace Backend.Models
{
    public class Exam:Entity
    {
        public string ExamType { get; set; } //maybe enum
        public List<Question> Questions { get; set; }
    }
}