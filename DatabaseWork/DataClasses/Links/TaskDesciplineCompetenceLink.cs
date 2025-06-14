using DatabaseWork.DataClasses.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWork.DataClasses
{
    public class TaskDesciplineCompetenceLink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDtdc { get; set; }

        [Key]
        public Task_d TaskLink { get; set; }

        public DisciplineCompetenceLink FullDCLink { get; set; }
    }
}
