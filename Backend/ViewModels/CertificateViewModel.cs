using Backend.Models;

namespace Backend.ViewModels
{
    public class CertificateViewModel
    {
        public User User{ get; set; }
        public List<Certificate> Certificates { get; set; }
    }
}
