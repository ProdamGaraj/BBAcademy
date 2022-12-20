namespace Backend.Models
{
    public class Exam:Entity
    {
        public List<Question> Questions { get; set; }
    }
}