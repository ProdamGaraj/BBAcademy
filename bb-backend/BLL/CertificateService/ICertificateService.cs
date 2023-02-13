using BLL.Models.CreateCertificate;
using Infrastructure.Models;

namespace BLL.CertificateService
{
    public interface ICertificateService
    {
        Task<ICollection<string>> GetCertificatesByUser(long userId);
    }
}
