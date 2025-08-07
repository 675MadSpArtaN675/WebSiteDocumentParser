using DatabaseWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParserSiteWork.Models;
using DatabaseWork.DataClasses.Tasks;

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
        public async Task<IActionResult> RemoveSI(string sel_item)
        {
            if (ModelState.IsValid)
            {
                SelectedItems? sel_item_obj = await _db.SelectedItems.FirstOrDefaultAsync(e => e.SelectValue.Equals(sel_item, StringComparison.OrdinalIgnoreCase));

                if (sel_item_obj != null)
                {
                    _db.SelectedItems.Remove(sel_item_obj);
                    _db.SaveChanges();

                    return View("DataWorker/Index");
                }

            }

            return View("DataWorker/Index"); ;
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTT(string type_task)
        {
            if (ModelState.IsValid)
            {
                TypeTask? tt = await _db.TaskTypes.FirstOrDefaultAsync(e => e.TTTitle.Equals(type_task, StringComparison.OrdinalIgnoreCase));

                if (tt != null)
                {
                    _db.TaskTypes.Remove(tt);
                    _db.SaveChanges();

                    return View("DataWorker/Index");
                }

            }

            return View("DataWorker/Index");
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