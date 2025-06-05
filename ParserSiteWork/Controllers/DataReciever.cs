using Microsoft.AspNetCore.Mvc;
using ParserSiteWork.Models;
using DocsParserLib.InputData;
using DocsParserLib.Serialization;
using DocsParserLib.Interfaces.Serialization;
using DocsParserLib.DataClasses;

namespace ParserSiteWork.Controllers
{
    public class DataRecieverController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public DataRecieverController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult DisplayDocumentData()
        {
            var doc_data = HttpContext.Request.Form.Files[0];

            using (var stream = doc_data.OpenReadStream())
            {
                WordDocument doc = new WordDocument(stream);

                IDataOutput dataOutput = new DataOutput();
                ParsedDataBundle dataBundle = dataOutput.GetParsedData(doc);

                return View("TablesPage", dataBundle);
            }
        }

        [HttpPost]
        public IActionResult EditedDataRecieve(ParsedDataBundle data, string[] old_names)
        {
            if (data is null)
                return Content("Я ошибся!");

            Dictionary<string, string> names_comparsion = new Dictionary<string, string>();

            int i = 0;
            foreach (var item in data.Competentions)
            {
                names_comparsion[old_names[i]] = item.Name;
                i++;
            }

            foreach (var item in data.Questions)
            {
                if (item.Competention?.Name is not null)
                {
                    string name = names_comparsion[item.Competention.Name];
                    item.Competention = data.GetCompetentionByName(name) ?? new Competention("None", -1);
                }
            }

            foreach (var item in data.PracticTasks)
            {
                if (item.Competention?.Name is not null)
                {
                    string name = names_comparsion[item.Competention.Name];
                    item.Competention = data.GetCompetentionByName(name) ?? new Competention("None", -1);
                }
            }

            ISerialization xmlSerializer = new SerializeXML();

            SerializationData serializationData = new SerializationData();
            serializationData.SerializeData(data, "data.xml", xmlSerializer);

            return Redirect("/Home/Index");
        }
    }
}
