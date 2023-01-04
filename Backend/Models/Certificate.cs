namespace Backend.Models
{
    public class Certificate : Entity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public CertificateToCourse Course { get; set; }
        public string MediaTemplatePath { get; set; }
        public Certificate()
        {
                
        }
        public Certificate(int userId,  string mediaTemplatePath, int courseId, CertificateToCourse course, bool deleted = false)
        {
            UserId = userId;
            MediaTemplatePath = mediaTemplatePath;
            CourseId = courseId;
            Course = course;
            Deleted = deleted;
        }
    }
}