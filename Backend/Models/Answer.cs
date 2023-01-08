using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Answer:Entity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        [NotMapped]
        public bool IsChosen { get; set; }
        public int Cost { get; set; } // Number of points the answer is worth
        public Answer()
        {

        }
        public Answer(string content, bool isCorrect, int cost, bool deleted = false)
        {
            Content = content;
            IsCorrect = isCorrect;
            Cost = cost;
            Deleted = deleted;
        }
    }
}