namespace Backend.Models
{
    public class Certificate : Entity
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserLastname { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public string ImageTemplatePath { get; set; }
        public CertificateType CertificateType { get; set; }
    }
}