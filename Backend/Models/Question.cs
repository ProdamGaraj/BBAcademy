namespace Backend.Models
{
    public class Question:Entity
    {
        public string Content { get; set; }
        public List<Answer> Answers { get; set; }
        public Question(string content,List<Answer> answers)
        {
            Content = content;
            Answers = answers;
        }
    }
}