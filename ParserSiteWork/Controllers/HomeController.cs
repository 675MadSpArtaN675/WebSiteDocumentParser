using Microsoft.AspNetCore.Mvc;
using ParserSiteWork.Models;
using DocsParserLib;
using Serialization;
using System.Text.Json;

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

        public IActionResult DisplayDocumentData()
        {
            var doc_data = HttpContext.Request.Form.Files[0];

            using (var stream = doc_data.OpenReadStream())
            {
                Document doc = new Document(stream);

                IDataOutput dataOutput = new DataOutput();
                ParsedDataBundle dataBundle = dataOutput.GetParsedData(doc);

                return View("TablesPage", dataBundle);
            }
        }
    }
}
