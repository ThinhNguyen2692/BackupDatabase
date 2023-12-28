using Bus_backUpData.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminLayout_Vuexy.Controllers
{
    public class ConfigSettingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusStoredProcedureServices _busStoredProcedureServices;
        public ConfigSettingController(ILogger<HomeController> logger, IBusStoredProcedureServices busStoredProcedureServices)
        {
            _logger = logger;
            _busStoredProcedureServices = busStoredProcedureServices;
        }
        [Authorize]
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
