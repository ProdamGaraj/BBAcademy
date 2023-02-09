namespace Infrastructure.Models
{
    public class Certificate : Entity
    {
        public long UserId { get; set; }

        public virtual User User { get; set; }

        public long CertificateTemplateId { get; set; }

        public virtual CertificateTemplate CertificateTemplate { get; set; }
    }
}