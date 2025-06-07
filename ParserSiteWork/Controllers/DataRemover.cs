using DatabaseWork;
using Microsoft.AspNetCore.Mvc;
using ParserSiteWork.Models;

namespace ParserSiteWork.Controllers
{
    public class DataRemover : Controller
    {
        private DatabaseContext _db;

        public DataRemover(DatabaseContext dbCon)
        {
            _db = dbCon;
        }

        [HttpPost]
        public IActionResult RemoveCompetence(List<DeleteCompModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var competence in model)
                {
                    if (competence.ToDelete && competence.Competence != null)
                        _db.Competences.Remove(competence.Competence);
                }
                
            }

            return View("DataWorker/Index");
        }

        [HttpPost]
        public IActionResult RemoveProfile(List<DeleteProfileModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var competence in model)
                {
                    if (competence.ToDelete && competence.Profile != null)
                        _db.Profiles.Remove(competence.Profile);
                }
                
            }

            return View("DataWorker/Index");
        }

        [HttpPost]
        public IActionResult RemoveTask(List<DeleteTaskModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var competence in model)
                {
                    if (competence.ToDelete && competence.Task != null)
                        _db.Tasks.Remove(competence.Task);
                }
                
            }

            return View("DataWorker/Index");
        }
    }
}
