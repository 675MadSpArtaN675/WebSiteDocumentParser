using DatabaseWork.DataClasses.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatabaseWork.DataClasses
{
    public class TaskDesciplineCompetenceLink
    {
        public int IDtdc { get; set; }

        [Required]
        public Task_d TaskLink { get; set; }

        [Required]
        public DisciplineCompetenceLink FullDCLink { get; set; }
    }
}
