using Logger.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;

namespace Logger.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly ILogger _logger = LogManager.GetLogger("Web");

        //public HomeController(ILogger<HomeController> logger)
        public HomeController()
        {
            //_logger = LogManager.GetLogger("Web");
        }

        public IActionResult Index()
        {
            _logger.Info("Hello_Info");
            _logger.Warn("Hello_Warning");
            _logger.Log(LogLevel.Error, "ERRORRRRRRRRRRR!!!");

            return View();
        }

        public IActionResult Privacy()
        {
            LogManager.Configuration.Variables["subject"] = "oops something happened";
            _logger.Log(LogLevel.Error, "<p>ERRORRRRRRRRRRR!!!</p>");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
