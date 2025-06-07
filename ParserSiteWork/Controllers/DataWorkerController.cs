using DatabaseWork;
using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ParserSiteWork.Controllers
{
    public class DataWorkerController : Controller
    {
        private DatabaseContext _db;

        public DataWorkerController(DatabaseContext database)
        {
            _db = database;
        }

        public IActionResult Index()
        {
            ReadData();
            return View();
        }

        [HttpGet]
        public IActionResult SpecialityPage()
        {
            if (HttpContext.Request.Cookies["login_guid"] != null && HttpContext.Request.Cookies["role"] != null)
            {
                ReadData();
                return View("../DataWorker/SpecialityAdder");
            }

            return View("Authorization/Login");
        }

        [HttpPost]
        public IActionResult LevelAdd(Level level)
        {
            AddToDatabase(level, _db.Levels);

            return View("Index");
        }

        [HttpPost]
        public IActionResult SgAdd(SpecGroup specGroup)
        {
            AddToDatabase(specGroup, _db.SpecGroups);

            return View("Index");
        }

        [HttpPost]
        public IActionResult SpecAdd(Speciality spec, Level? LevelToSpec, SpecGroup? specGroup)
        {
            if (LevelToSpec != null)
                spec.EdLevel = LevelToSpec;
            
            if (specGroup != null)
                spec.SGroup = specGroup;

            AddToDatabase(spec, _db.Specialities);

            return View("Index");
        }

        [HttpPost]
        public IActionResult ProfileAdd(Profile profile, Speciality? spec)
        {
            if (spec != null)
                profile.Spec = spec;

            AddToDatabase(profile, _db.Profiles);

            return View("Index");
        }

        [HttpGet]
        public IActionResult DisciplineAdd(Discipline discipline, Profile? profile)
        {
            if (profile != null)
                discipline.R_Profile = profile;

            AddToDatabase(discipline, _db.Disciplines);
            return View("Index");
        }

        [HttpPost]
        public IActionResult TaskAdd(Task_d task, Discipline discip, Competence comp, List<SelectedItems>? si, TypeTask? type)
        {
            DisciplineCompetenceLink disciplineCompetence = new DisciplineCompetenceLink();
            disciplineCompetence.CompetenceLink = comp;
            disciplineCompetence.DisciplineLink = discip;

            TaskDesciplineCompetenceLink tdc = new TaskDesciplineCompetenceLink();
            tdc.FullDCLink = disciplineCompetence;
            tdc.TaskLink = task;

            if (si != null)
                foreach (SelectedItems item in si)
                {
                    item.TaskLink = task;
                }

            if (si != null)
                task.SItems = si;

            if (type != null)
                task.TaskType = type;

            AddToDatabase(task, _db.Tasks);

            return View("Index");
        }

        [HttpPost]
        public IActionResult TypeTaskAdd(TypeTask typeTask)
        {
            AddToDatabase(typeTask, _db.TaskTypes);

            return View("Index");
        }

        [HttpPost]
        public IActionResult SelectedItemsAdd(SelectedItems selIt)
        {
            AddToDatabase(selIt, _db.SelectedItems);

            return View("Index");
        }

        [HttpPost]
        public IActionResult CompetenceAdd(Competence comp, Profile profile, TypeCompetence? type)
        {
            if (type != null)
            {
                comp.CompType = type;
            }

            comp.ProfileLink = profile;

            AddToDatabase(comp, _db.Competences);
            return View("Index");
        }

        [HttpPost]
        public IActionResult CompTypeAdd(TypeCompetence typeTask)
        {
            AddToDatabase(typeTask, _db.TypesOfCompetences);

            return View("Index");
        }

        private void AddToDatabase<T>(T value, DbSet<T> dbSet) where T : class
        {
            try
            {
                dbSet.Add(value);

                _db.SaveChanges();
            }
            catch (DbUpdateException ex) { }

            ReadData();
        }

        private void ReadData()
        {
            ViewBag.Levels = _db.Levels.AsNoTracking().Select(l => new SelectListItem(l.LvTitle, l.LvTitle)).ToList();
            ViewBag.SpecGroups = _db.SpecGroups.AsNoTracking().Select(s => new SelectListItem(s.SGTitle, s.SGTitle)).ToList();
            ViewBag.Specialities = _db.Specialities.AsNoTracking().Select(s => new SelectListItem(s.SpecNumber, s.SpecNumber)).ToList();
            ViewBag.Profiles = _db.Profiles.AsNoTracking().Select(p => new SelectListItem(p.ProTitle, p.ProTitle)).ToList();
            
            ViewBag.Competences = _db.Competences.AsNoTracking().Select(p => new SelectListItem(p.CompNumber, p.CompNumber)).ToList();
            ViewBag.SelectItem = _db.SelectedItems.AsNoTracking().Select(p => new SelectListItem(p.SelectValue, p.SelectValue)).ToList();
            ViewBag.TypeOfCompetences = _db.TypesOfCompetences.AsNoTracking().Select(p => new SelectListItem(p.TCTitle, p.TCTitle)).ToList();
            ViewBag.TypeTask = _db.TaskTypes.AsNoTracking().Select(p => new SelectListItem(p.TTTitle, p.TTTitle)).ToList();
            ViewBag.Discipline = _db.Disciplines.AsNoTracking().Select(p => new SelectListItem(p.DisTitle, p.DisTitle)).ToList();
        }
    }
}
