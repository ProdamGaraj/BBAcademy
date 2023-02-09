namespace WebApi.ViewModels
{
    public class ExamViewModel
    {
        public User User { get; set; }
        public Course Course { get; set; }
        public List<Question> Questions { get; set; }

    }
}
