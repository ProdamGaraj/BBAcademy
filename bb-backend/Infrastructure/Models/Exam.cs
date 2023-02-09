namespace Infrastructure.Models
{
    public class Exam : Entity
    {
        public long CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string Description { get; set; }

        public int MinimumPassingGrade { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}