using BLL.Models;
using Infrastructure.Models;

namespace BLL.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<Certificate> CreateCertificate(CreateCertificateDto dto);
        Task<List<Certificate>> GetCertificatesByUser(long userId);
    }
}
