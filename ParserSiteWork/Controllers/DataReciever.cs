using Microsoft.AspNetCore.Mvc;
using ParserSiteWork.Models;
using DocsParserLib;
using Serialization;

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
                Document doc = new Document(stream);

                IDataOutput dataOutput = new DataOutput();
                ParsedDataBundle dataBundle = dataOutput.GetParsedData(doc);

                return View("TablesPage", dataBundle);
            }
        }

        [HttpPost]
        public IActionResult EditedDataRecieve( ParsedDataBundle data)
        {
            if (data is null)
                return Content("Я ошибся!");

            Console.WriteLine($"Competentions: {data.Competentions.Count};\nQuestions: {data.Questions.Count}\nPracticTasks: {data.PracticTasks.Count}");
            foreach (var item in data.Questions)
            {
                if (item.Competention?.Name is not null)
                {
                    item.Competention = data.GetCompetentionByName(item.Competention.Name) ?? new Competention("None", -1);
                }
            }

            foreach (var item in data.PracticTasks)
            {
                if (item.Competention?.Name is not null)
                {
                    item.Competention = data.GetCompetentionByName(item.Competention.Name) ?? new Competention("None", -1);
                }
            }

            string frame = "";
            for (int i = 0; i < 128; i++)
                frame += '=';

            foreach (var competention in data.Competentions)
                Console.WriteLine(competention);

            Console.WriteLine(frame);

            foreach (var question in data.Questions)
                Console.WriteLine(question);

            Console.WriteLine(frame);

            foreach (var task in data.PracticTasks)
                Console.WriteLine(task);

            Console.WriteLine(frame);

            ISerialization xmlSerializer = new SerializeXML();

            SerializationData serializationData = new SerializationData();
            serializationData.SerializeData(data, "data.xml", xmlSerializer);

            return Empty;
        }

        [HttpPost]
        public IActionResult Test(ParsedDataBundle data)
        {
            Console.WriteLine(data);
            Console.WriteLine($"Competentions: {data.Competentions.Count};\nQuestions: {data.Questions.Count}\nPracticTasks: {data.PracticTasks.Count}");

            return Ok();
        }
    }
}
