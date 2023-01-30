using Backend.Models.Interfaces;
using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<IBaseResponce<Certificate>> CreateCertificate(CourseViewModel vm);
        Task<IBaseResponce<List<Certificate>>> GetCertificates(CertificateViewModel vm);
    }
}
