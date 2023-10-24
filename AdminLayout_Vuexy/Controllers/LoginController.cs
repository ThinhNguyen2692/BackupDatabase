using AdminLayout_Vuexy.Models;
using Bus_backUpData.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminLayout_Vuexy.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public LoginController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
