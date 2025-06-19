using DatabaseWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<IActionResult> TaskRemove(string task_name)
        {
            var tdc = _db.FullTDC.Include("TaskLink").FirstOrDefault(e => e.TaskLink.TaskAnnotation.Equals(task_name));

            if (tdc != null)
            {
                _db.FullTDC.Remove(tdc);
            }
            else
            {
                var task = _db.Tasks.FirstOrDefault(e => e.TaskAnnotation.Equals(task_name));

                if (task != null)
                    _db.Tasks.Remove(task);
            }

            await _db.SaveChangesAsync();
            
            return Redirect("../DataWorker/TaskPage");
        }
    }
}