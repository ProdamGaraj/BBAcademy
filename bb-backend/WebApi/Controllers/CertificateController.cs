using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class CertificateController : Controller
    {
        public CertificateController()
        {
        }
    }
}