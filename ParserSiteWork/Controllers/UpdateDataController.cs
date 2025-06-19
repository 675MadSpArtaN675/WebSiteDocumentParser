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
        public IActionResult UpdateTask(string task)
        {
            if (ModelState.IsValid)
            {
                Task_d? task_object = _db.Tasks.FirstOrDefault(e => e.TaskAnnotation == task);

                if (task_object != null)
                {
                    return View("../DataWorker/UpdateData/UpdateTask", task_object);
                }
            }

            foreach(var item in ModelState)
            {
                foreach(var errors in item.Value.Errors)
                {
                    Console.WriteLine(errors.ErrorMessage);
                }
            }

            return Redirect("/DataWorker/Index");
        }

        [HttpPost]
        public IActionResult UpdateTask(Task_d task)
        {
            if (ModelState.IsValid)
            {
                Task_d? task_object = _db.Tasks.FirstOrDefault(e => e.IDtask == task.IDtask);

                if (task_object != null)
                {
                    task_object.TaskAnnotation = task.TaskAnnotation;
                    task_object.TaskCorrectAnswer = task.TaskCorrectAnswer;
                    task_object.TaskStandartAnswer = task.TaskStandartAnswer;
                    task_object.TaskTime = task.TaskTime;

                    _db.Tasks.Update(task_object);
                    _db.SaveChanges();
                }

            }

            foreach(var item in ModelState)
            {
                foreach(var errors in item.Value.Errors)
                {
                    Console.WriteLine(errors.ErrorMessage);
                }
            }

            return Redirect("/DataWorker/Index");
        }
    }
}