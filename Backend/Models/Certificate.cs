namespace Backend.Models
{
    public class Certificate : Entity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public ICollection<Course> Courses { get; set; }
        public string MediaTemplatePath { get; set; }
        public Certificate()
        {
                
        }
        public Certificate(int userId,  string mediaTemplatePath, int courseId, ICollection<Course> courses, bool deleted = false)
        {
            UserId = userId;
            MediaTemplatePath = mediaTemplatePath;
            CourseId = courseId;
            Courses = courses;
            Deleted = deleted;
        }
    }
}