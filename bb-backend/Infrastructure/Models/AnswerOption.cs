namespace Infrastructure.Models
{
    public class AnswerOption : Entity
    {
        public long QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int Order { get; set; }
        
        public string Title { get; set; }

        public bool IsCorrect { get; set; }
        
        public int Weight { get; set; }
    }
}