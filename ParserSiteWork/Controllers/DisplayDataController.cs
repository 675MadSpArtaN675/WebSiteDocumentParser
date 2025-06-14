using DatabaseWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParserSiteWork.Models;

namespace ParserSiteWork.Controllers
{
    public class DisplayDataController : Controller
    {
        DatabaseContext _db;

        public DisplayDataController(DatabaseContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var base_info = _db.FullTDC
                .Include(t => t.FullDCLink.CompetenceLink)
                    .ThenInclude(p => p.ProfileLink)
                .Include(d => d.FullDCLink.DisciplineLink)
                .Include(t => t.TaskLink).ToList();

            var answer_variants = _db.SelectedItems.ToList();

            return View("Index", new DisplayModel { SelectedItems=answer_variants, TaskCompetenceDisciplineData=base_info });
        }


    }
}