using BLL.Models;
using Infrastructure.Models;

namespace BLL.CertificateService
{
    public interface ICertificateService
    {
        Task<Certificate> CreateCertificate(CreateCertificateDto dto);
        Task<List<Certificate>> GetCertificatesByUser(long userId);
    }
}
