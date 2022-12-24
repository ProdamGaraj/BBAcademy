namespace Backend.Models
{
    public class Exam:Entity
    {
        public string Description { get; set; }
        public string ExamType { get; set; } //maybe enum
        public List<Question> Questions { get; set; }
        public Exam(string examType, List<Question> questions)
        {
            ExamType = examType;
            Questions = questions;
        }
    }
}