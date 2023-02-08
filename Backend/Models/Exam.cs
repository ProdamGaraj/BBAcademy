using System.Collections.Generic;

namespace Backend.Models
{
    public class Exam:Entity
    {
        public string Description { get; set; }
        public string ExamType { get; set; } //maybe enum
        public int PassingGrade { get; set; }
        public ICollection<Question> Questions { get; set; }
        public Exam()
        {

        }
        public Exam(string description, string examType, ICollection<Question> questions, bool deleted = false)
        {
            Description = description;
            ExamType = examType;
            Questions = questions;
            Deleted = deleted;  
        }
    }
}