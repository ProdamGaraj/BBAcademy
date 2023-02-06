namespace Backend.Models
{
    public class Certificate : Entity
    {
        public long UserId { get; set; }
        public CertificateTemplate CertificateTemplate { get; set; }
        public Certificate()
        {
                
        }
        public Certificate(long userId, CertificateTemplate certificateTemplate)
        {
            UserId= userId;
            CertificateTemplate= certificateTemplate;
        }
    }
}