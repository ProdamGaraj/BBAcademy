using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BLL.CertificateService;
using BLL.Models.CertificateOut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Configs;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    [ResponseCache(NoStore = true)]
    public class CertificateController : Controller
    {
        private IOptions<StaticConfig> _staticConfig;
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService, IOptions<StaticConfig> staticConfig)
        {
            _certificateService = certificateService;
            this._staticConfig = staticConfig;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ICollection<string>>> GetAllForDashboard()
        {
            var userId = HttpContext.User.GetId();
            var result = await _certificateService.GetCertificatesByUser(userId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<FileStreamResult> GetCertificate([Required] string name)
        {
            var folderPath = Path.Combine(_staticConfig.Value.StaticFilesPath, "generated-certs");
            var filePath = Path.Combine(folderPath, name);

            var fileStream = new FileStream(filePath, FileMode.Open);

            return new FileStreamResult(fileStream, "application/pdf");
        }
    }
}