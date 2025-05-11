using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class Competence
    {
        public int IDcomp { get; set; }
        public string? CompNumber { get; set; }
        public string? CompAnnotation { get; set; }

        public TypeCompetence? CompType { get; set; }
    }

    public class TypeCompetence
    {
        public int IDtc { get; set; }
        public string? TCTitle { get; set; }

        public Competence? Competence { get; set; }
    }
}
