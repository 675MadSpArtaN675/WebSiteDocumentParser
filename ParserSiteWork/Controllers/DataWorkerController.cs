using DatabaseWork;
using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParserSiteWork.Models;
using System.Linq;

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
            return View("Index", Request.Cookies["role"] == "admin");
        }

        [HttpGet]
        public IActionResult SpecialityPage()
        {
            if (HttpContext.Request.Cookies["login_guid"] != null && HttpContext.Request.Cookies["role"] != null)
            {
                ReadData();
                return View("SpecialityAdder");
            }

            return View("Authorization/Login");
        }

        [HttpGet]
        public IActionResult TaskPage()
        {
            if (HttpContext.Request.Cookies["login_guid"] != null && HttpContext.Request.Cookies["role"] != null)
            {
                ReadData();
                return View("TaskMenu");
            }

            return View("Authorization/Login");
        }

        [HttpGet]
        public IActionResult CompetencePage()
        {
            if (HttpContext.Request.Cookies["login_guid"] != null && HttpContext.Request.Cookies["role"] != null)
            {
                ReadData();
                return View("CompetenceAdd");
            }

            return View("Authorization/Login");
        }

        [HttpPost]
        public IActionResult LevelAdd(Level level)
        {
            AddToDatabase(level, _db.Levels);

            return View("SpecialityAdder");
        }

        [HttpPost]
        public IActionResult SgAdd(SpecGroup specGroup)
        {
            AddToDatabase(specGroup, _db.SpecGroups);

            return View("SpecialityAdder");
        }

        [HttpPost]
        public IActionResult SpecAdd(Speciality spec, Level? LevelToSpec, SpecGroup? specGroup)
        {
            if (LevelToSpec != null)
                spec.EdLevel = LevelToSpec;

            if (specGroup != null)
                spec.SGroup = specGroup;

            AddToDatabase(spec, _db.Specialities);

            return View("SpecialityAdder");
        }

        [HttpPost]
        public IActionResult ProfileAdd(Profile profile, Speciality? spec)
        {
            if (spec != null)
                profile.Spec = spec;

            AddToDatabase(profile, _db.Profiles);

            return View("SpecialityAdder");
        }

        [HttpPost]
        public IActionResult DisciplineAdd(Discipline discipline, Profile? profile)
        {
            if (profile != null)
                discipline.R_Profile = profile;

            AddToDatabase(discipline, _db.Disciplines);
            return View("SpecialityAdder");
        }

        [HttpPost]
        public IActionResult TaskAdd(Task_d task, string discip, string comp, string? type)
        {
            Console.WriteLine($"{discip}, {comp}, {type}");
            if (ModelState.IsValid)
            {
                DisciplineCompetenceLink disciplineCompetence = new DisciplineCompetenceLink();

                disciplineCompetence.CompetenceLink = _db.Competences.FirstOrDefault(e => e.CompNumber == comp);
                disciplineCompetence.DisciplineLink = _db.Disciplines.FirstOrDefault(e => e.DisTitle == discip);

                TaskDesciplineCompetenceLink tdc = new TaskDesciplineCompetenceLink();
                tdc.FullDCLink = disciplineCompetence;
                tdc.TaskLink = task;

                if (type != null)
                    task.TaskType = _db.TaskTypes.FirstOrDefault(e => e.TTTitle == type);

                AddToDatabase(tdc, _db.FullTDC);

                return View("Index");
            }

            PrintErrors();
            return View("Index");
        }

        private void PrintErrors()
        {
            foreach (var item in ModelState)
            {
                foreach (var item_2 in item.Value.Errors)
                    Console.WriteLine($"{item.Key} {item_2.ErrorMessage}");
            }
        }

        [HttpPost]
        public IActionResult TypeTaskAdd(TypeTask typeTask)
        {
            AddToDatabase(typeTask, _db.TaskTypes);

            return View("TaskMenu");
        }

        [HttpPost]
        public IActionResult SelectedItemsAdd(SelectedItemModel selIt)
        {
            if (ModelState.IsValid)
            {
                Task_d? task = null;
                if (selIt.TaskObject != null)
                    task = _db.Tasks.FirstOrDefault(t => t.TaskAnnotation == selIt.TaskObject);

                SelectedItems items = new SelectedItems { SelectValue = selIt.SelectValue, SelectTrue = selIt.SelectTrue };

                if (task != null)
                    items.TaskLink = task;

                AddToDatabase(items, _db.SelectedItems);
            }
            else
                PrintErrors();

            return View("TaskMenu");
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
            return View("CompetenceAdd");
        }

        [HttpPost]
        public IActionResult CompTypeAdd(TypeCompetence typeTask)
        {
            AddToDatabase(typeTask, _db.TypesOfCompetences);

            return View("CompetenceAdd");
        }

        private void AddToDatabase<T>(T value, DbSet<T> dbSet) where T : class
        {
            try
            {
                dbSet.Add(value);

                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Ошибка записи в БД: {ex}");
            }

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

            ViewBag.Tasks = _db.Tasks.AsNoTracking().Select(p => new SelectListItem(p.TaskAnnotation, p.TaskAnnotation)).ToList();
        }
    }
}