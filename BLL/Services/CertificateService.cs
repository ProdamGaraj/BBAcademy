using System.Reflection;
using BLL.Models;
using BLL.Services.Interfaces;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IRepository<Certificate> _certificateRepository;
        private readonly ILogger<CertificateService> _logger;

        public CertificateService(IRepository<Certificate> certificateRepository, ILogger<CertificateService> logger)
        {
            _certificateRepository = certificateRepository;
            _logger = logger;
        }

        public async Task<Certificate> CreateCertificate(CreateCertificateDto dto)
        {
            try
            {
                var certificate = new Certificate()
                {
                    UserId = dto.UserId,
                    CertificateTemplateId = dto.CertificateTemplateId
                };

                _certificateRepository.Add(certificate);
                await _certificateRepository.SaveChangesAsync();

                CreateCertificateFile(certificate.Id);

                return certificate;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed creating certificate", ex);
            }
        }

        public async Task<List<Certificate>> GetCertificatesByUser(long userId)
        {
            var certificates = await _certificateRepository.GetAll()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return certificates;
        }

        private static void CreateCertificateFile(long certificateId)
        {
            var basePath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly()
                    .Location
            );
            File.Copy(
                basePath + "\\Files\\Certificates\\certificate.pdf",
                basePath + $"\\Files\\Certificates\\{certificateId}.pdf"
            );
        }
    }
}