namespace Backend.Models
{
    public class Exam:Entity
    {
        public string Description { get; set; }
        public string ExamType { get; set; } //maybe enum
        public List<ExamToQuestion> Questions { get; set; }
        public Exam()
        {

        }
        public Exam(string description, string examType, List<ExamToQuestion> questions, bool deleted = false)
        {
            Description = description;
            ExamType = examType;
            Questions = questions;
            Deleted = deleted;  
        }
    }
}