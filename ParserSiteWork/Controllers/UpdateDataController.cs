using DatabaseWork;
using DatabaseWork.DataClasses.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParserSiteWork.Controllers
{
    public class UpdateDataController : Controller
    {
        private DatabaseContext _db;
        public UpdateDataController(DatabaseContext database)
        {
            _db = database;
        }

        [HttpGet]
        public IActionResult UpdateTask(Task_d task, string? message = "Выбрано: \n")
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(message + $"{task.TaskAnnotation}");
                return View("UpdateData/UpdateTask", task);
            }

            return View("DataWorker/Index");
        }

        [HttpPost]
        public IActionResult UpdateTask(Task_d task)
        {
            if (ModelState.IsValid)
            {
                UpdateDataInDB(task, _db.Tasks);
            }
            return View("DataWorker/Index");
        }

        private void UpdateDataInDB<T>(T value, DbSet<T> values) where T : class
        {
            try
            {
                values.Update(value);
                _db.SaveChanges();
            }
            catch (DbUpdateException e) { }

        }
    }
}
