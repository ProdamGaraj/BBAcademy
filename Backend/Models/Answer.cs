namespace Backend.Models
{
    public class Answer:Entity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int Cost { get; set; } // Number of points the answer is worth
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public Answer(string content, bool isCorrect, int cost, int questionId, Question question)
        {
            Content = content;
            IsCorrect = isCorrect;
            Cost = cost;
            QuestionId = questionId;
            Question = question;
        }
    }
}