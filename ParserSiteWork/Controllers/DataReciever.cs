using DatabaseWork;
using DatabaseWork.DataClasses;
using DatabaseWork.TypeConverters;
using DatabaseWork.TypeConverters.DataClasses;
using DocsParserLib.DataClasses;
using DocsParserLib.InputData;
using DocsParserLib.Interfaces.Serialization;
using DocsParserLib.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParserSiteWork.Models;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

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
            var doc_data = HttpContext.Request.Form.Files.Count > 0 ? HttpContext.Request.Form.Files[0] : null;

            if (doc_data is null)
                return View("../Home/Index");

            using (var stream = doc_data.OpenReadStream())
            {
                WordDocument doc = new WordDocument(stream);

                IDataOutput dataOutput = new DataOutput();
                ParsedDataBundle dataBundle = dataOutput.GetParsedData(doc);

                ViewBag.Profiles = _db.Profiles.AsNoTracking().Select(p => new SelectListItem(p.ProTitle, p.ProTitle)).ToList();
                return View("TablesPage", dataBundle);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditedDataRecieve(ParsedDataBundle data, Profile? profile, string[] old_names)
        {
            if (data is null)
                return Content("Я ошибся!");

            var disciplineName = Request.Form["Discipline.Name"];
            if (!string.IsNullOrEmpty(disciplineName))
            {
                data.Discipline = data.Discipline ?? new DocsParserLib.DataClasses.Discipline();
                data.Discipline.Name = disciplineName;
            }

            if (ModelState.IsValid)
            {
                if (profile is null || profile.ProTitle == "" || profile.ProTitle is null)
                {
                    profile = await _db.Profiles.FirstAsync(e => e.ProTitle.Equals("None"));
                }

                Dictionary<string, string> names_comparsion = CompareOldNamesToNewNames(data, old_names);

                ParsedDataBundleConverter conv = new ParsedDataBundleConverter();

                ConvertedDataBundle bundle = conv.Convert(data, profile);
                ExportDataToDatabase(bundle);

                FixData(data, names_comparsion);
                SerializeToXML(data);

                return Redirect("/DataWorker/Index");
            }

            foreach (var item in ModelState)
            {
                foreach (var errors in item.Value.Errors)
                {
                    Console.WriteLine(errors.ErrorMessage);
                }
            }

            return View("../Home/Index");
        }


        private void ExportDataToDatabase(ConvertedDataBundle bundle)
        {
            try
            {
                Console.WriteLine(bundle.TDCLinks.Count);
                Console.WriteLine(bundle.DCLink.Count);
                Console.WriteLine(bundle.SelectedItems.Count);

                var sel_task = _db.SelectedItems.AddRangeAsync(bundle.SelectedItems);
                var disc_task = _db.FullDiscipline.AddRangeAsync(bundle.DCLink);
                var tdc_task = _db.FullTDC.AddRangeAsync(bundle.TDCLinks);

                Task.WhenAll(sel_task, disc_task, tdc_task).Wait();
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Не удалось обновить Базу данных.\n{ex}");
            }
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