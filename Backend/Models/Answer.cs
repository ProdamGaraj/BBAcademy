namespace Backend.Models
{
    public class Answer:Entity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int Cost { get; set; } // Number of points the answer is worth
        public Question Question { get; set; }
        public Answer()
        {

        }
        public Answer(string content, bool isCorrect, int cost, Question question, bool deleted = false)
        {
            Content = content;
            IsCorrect = isCorrect;
            Cost = cost;
            Question = question;
            Deleted = deleted;
        }
    }
}