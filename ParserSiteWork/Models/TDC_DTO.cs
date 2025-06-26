using DatabaseWork.DataClasses.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ParserSiteWork.Models
{
    public class TaskDesciplineCompetenceLinkDTO
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public string? ProTitle { get; set; }
        public string? CompNumber { get; set; }
        public string? DisTitle { get; set; }
        public string? TaskAnnotation { get; set; }
        public string? TaskCorrectAnswer { get; set; }
    }
    public class SelectedItemsDTO
    {
        public string? SelectValue { get; set; }
        public bool SelectTrue { get; set; }
        public int IdTask { get; set; }
    }
}
