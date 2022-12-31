namespace Backend.Models
{
    public class Question:Entity
    {
        public string Content { get; set; }
        public QuestionType QuestionType { get; set; }
        public List<Answer> Answers { get; set; }
        public Question()
        {
                
        }
        public Question(string content,QuestionType questionType, List<Answer> answers, bool deleted = false)
        {
            Content = content;
            QuestionType = questionType;
            Answers = answers;
            Deleted = deleted;
        }
    }
}