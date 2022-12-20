namespace Backend.Models
{
    public class Certificate : Entity
    {
        public string Description { get; set; }
        public string ImageTemplatePath { get; set; }
        public CertificateType CertificateType { get; set; }
    }
}