using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ParserSiteWork.Models;
using DocsParserLib;
using DocsParserLib.Serialization;

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
            if (HttpContext.Request.Cookies["login_guid"] != null && HttpContext.Request.Cookies["role"] != null)
                return View();

            return View("Authorization/Login");
        }


    }
}