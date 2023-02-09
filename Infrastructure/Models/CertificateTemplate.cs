namespace Infrastructure.Models
{
    public class CertificateTemplate : Entity
    {
        public virtual ICollection<Course> Courses { get; set; }

        public string MediaPath { get; set; }
    }
}