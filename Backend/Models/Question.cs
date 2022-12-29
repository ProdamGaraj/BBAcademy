namespace Backend.Models
{
    public class Question:Entity
    {
        public string Content { get; set; }
        public List<Answer> Answers { get; set; }
        public Question()
        {
                
        }
        public Question(string content,List<Answer> answers, bool deleted = false)
        {
            Content = content;
            Answers = answers;
            Deleted = deleted;
        }
    }
}