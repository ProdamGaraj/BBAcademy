using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class DataController : Controller
    {
        private ILogger<DataController> _logger;


        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }
    }
}
