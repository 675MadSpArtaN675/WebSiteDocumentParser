using Microsoft.AspNetCore.Mvc;
using ParserSiteWork.Models;
using DocsParserLib;
using Serialization;

namespace ParserSiteWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
