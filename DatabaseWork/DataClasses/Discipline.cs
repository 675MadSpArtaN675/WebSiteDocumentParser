using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class Discipline
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDdis { get; set; }
        public string? DisNumber { get; set; }
        public string? DisTitle { get; set; }
        public string? DisFinalSemestr { get; set; }

        public Profile? R_Profile { get; set; }
        public List<DisciplineCompetenceLink> DCLink { get; set; }
    }
}
