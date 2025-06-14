using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class Competence
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDcomp { get; set; }
        public string? CompNumber { get; set; }
        public string? CompAnnotation { get; set; }


        public TypeCompetence? CompType { get; set; }
        [Key]
        public Profile ProfileLink { get; set; }

        [Key]
        public List<DisciplineCompetenceLink> DCLink { get; set; }
    }

    public class TypeCompetence
    {
        public int IDtc { get; set; }
        public string? TCTitle { get; set; }

        public List<Competence>? Competence { get; set; }
    }
}
