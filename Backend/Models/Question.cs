namespace Backend.Models
{
    public class Question:Entity
    {
        public string Description { get; set; }
        public List<Answer> Answers { get; set; }
        public Answer CorrectAnswer { get; set; }
        public Question()
        {

        }
    }
}