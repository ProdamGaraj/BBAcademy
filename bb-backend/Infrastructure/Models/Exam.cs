namespace Infrastructure.Models
{
    public class Exam : Entity
    {
        // no course ID because it's ID is bound to Course.Id
        public virtual Course Course { get; set; }

        public string Title { get; set; }

        public int MinimumPassingGrade { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}