namespace Backend.Models
{
    public class Certificate : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string MediaTemplatePath { get; set; }
        public Certificate(int userId, User user, string mediaTemplatePath, int courseId, Course course, bool deleted = false)
        {
            UserId = userId;
            User = user;
            MediaTemplatePath = mediaTemplatePath;
            CourseId = courseId;
            Course = course;
            Deleted = deleted;
        }
    }
}