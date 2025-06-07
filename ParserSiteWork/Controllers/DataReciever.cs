using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ParserSiteWork.Models;

using DatabaseWork.TypeConverters;
using DocsParserLib.DataClasses;
using DocsParserLib.InputData;
using DocsParserLib.Interfaces.Serialization;
using DocsParserLib.Serialization;
using DatabaseWork.TypeConverters.DataClasses;
using DatabaseWork;

namespace ParserSiteWork.Controllers
{
    public class DataRecieverController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _db;

        public DataRecieverController(ILogger<HomeController> logger, DatabaseContext database)
        {
            _logger = logger;
            _db = database;
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

            Dictionary<string, string> names_comparsion = CompareOldNamesToNewNames(data, old_names);

            ParsedDataBundleConverter conv = new ParsedDataBundleConverter();
            ConvertedDataBundle bundle = conv.Convert(data);
            ExportDataToDatabase(bundle);

            FixData(data, names_comparsion);
            SerializeToXML(data);

            return Redirect("/Home/Index");
        }

        private void ExportDataToDatabase(ConvertedDataBundle bundle)
        {
            try
            {
                _db.Tasks.AddRange(bundle.Tasks);
                _db.Competences.AddRange(bundle.Competences);
                _db.Disciplines.AddRange(bundle.Discplines);

                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            { }
        }

        private static void FixData(ParsedDataBundle data, Dictionary<string, string> names_comparsion)
        {
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
        }

        private static Dictionary<string, string> CompareOldNamesToNewNames(ParsedDataBundle data, string[] old_names)
        {
            Dictionary<string, string> names_comparsion = new Dictionary<string, string>();

            int i = 0;
            foreach (var item in data.Competentions)
            {
                names_comparsion[old_names[i]] = item.Name;
                i++;
            }

            return names_comparsion;
        }

        private static void SerializeToXML(ParsedDataBundle data)
        {
            ISerialization xmlSerializer = new SerializeXML();

            SerializationData serializationData = new SerializationData();
            serializationData.SerializeData(data, "data.xml", xmlSerializer);
        }
    }
}
