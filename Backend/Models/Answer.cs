namespace Backend.Models
{
    public class Answer:Entity
    {
        public string Description { get; set; }
        public bool IsCorrect { get; set; }
        public int Points { get; set; } // Number of points the answer is worth
        public int Order { get; set; } // Order in which the answer is presented to the user

        // Foreign key to the question to which the answer belongs
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public Answer(string description, bool isCorrect, int points, int order, int questionId, Question question)
        {
            Description = description;
            IsCorrect = isCorrect;
            Points = points;
            Order = order;
            QuestionId = questionId;
            Question = question;
        }
    }
}