namespace Backend.Models
{
    public class CertificateTemplate:Entity
    {
        public ICollection<Course> Courses { get; set; }
        public string MediaPath { get; set; }
        public string TextContent{ get; set; }
        public CertificateTemplate()
        {

        }
        public CertificateTemplate(ICollection<Course> courses, string mediaPath, string textContent)
        {
            Courses = courses;
            MediaPath = mediaPath;
            TextContent = textContent;
        }
    }

}
