using DatabaseWork;
using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Export.ToDataTable;
using ParserSiteWork.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var base_info = _db.FullTDC
                .Include(t => t.FullDCLink.CompetenceLink)
                    .ThenInclude(p => p.ProfileLink)
                .Include(d => d.FullDCLink.DisciplineLink)
                .Include(t => t.TaskLink).Select(e => DisplayDataController.TDC_To_TDC_DTO(e)).AsNoTracking().ToArray();

            var answer_variants = _db.SelectedItems.Select(e => DisplayDataController.SI_To_SIDTO(e)).AsNoTracking().ToArray();

            DisplayModel dm = new DisplayModel { SelectedItems = answer_variants, TaskCompetenceDisciplineData = base_info };

            string json_model = JsonSerializer.Serialize(dm, options);
            ViewBag.SerializedModel = json_model;

            if (base_info != null && base_info.Length > 0)
                return View("Index", dm);

            return Redirect("/DataWorker/Index");
        }

        private static TaskDesciplineCompetenceLinkDTO TDC_To_TDC_DTO(TaskDesciplineCompetenceLink? tdc)
        {
            TaskDesciplineCompetenceLinkDTO dto = new TaskDesciplineCompetenceLinkDTO();

            if (tdc != null)
            {
                dto.Id = tdc.IDtdc;
                dto.IdTask = tdc.TaskLink.IDtask;
                dto.ProTitle = tdc.FullDCLink.CompetenceLink.ProfileLink.ProTitle;
                dto.CompNumber = tdc.FullDCLink.CompetenceLink.CompNumber;
                dto.DisTitle = tdc.FullDCLink.DisciplineLink.DisTitle;
                dto.TaskAnnotation = tdc.TaskLink.TaskAnnotation;
                dto.TaskCorrectAnswer = tdc.TaskLink.TaskCorrectAnswer;
            }

            return dto;
        }

        private static SelectedItemsDTO SI_To_SIDTO(SelectedItems? si)
        {
            SelectedItemsDTO siDTO = new SelectedItemsDTO();

            if (si != null)
            { 
                siDTO.SelectValue = si.SelectValue;
                siDTO.SelectTrue = si.SelectTrue;

                if (si.TaskLink != null)
                    siDTO.IdTask = si.TaskLink.IDtask;
            }

            return siDTO;
        }
    }
}