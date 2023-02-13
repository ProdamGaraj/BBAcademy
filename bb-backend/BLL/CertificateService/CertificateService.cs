using System.Reflection;
using BLL.Models.CreateCertificate;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.CertificateService
{
    public class CertificateService : ICertificateService
    {
        private readonly IRepository<CourseProgress> _courseProgressRepository;
        private readonly ILogger<CertificateService> _logger;

        public CertificateService(IRepository<CourseProgress> courseProgressRepository, ILogger<CertificateService> logger)
        {
            _courseProgressRepository = courseProgressRepository;
            _logger = logger;
        }

        public async Task<ICollection<string>> GetCertificatesByUser(long userId)
        {
            var certificates = await _courseProgressRepository.GetAll()
                .Where(x => x.UserId == userId && x.State == CourseProgressState.Passed)
                .Select(x => x.CertificateName)
                .ToListAsync();

            return certificates;
        }
    }
}