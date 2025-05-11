using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class DisciplineCompetenceLink
    {
        public int IDdc { get; set; }

        public Competence CompetenceLink { get; set; }
        public Discipline DisciplineLink { get; set; }

        public TaskDesciplineCompetenceLink TDCLink { get; set; }
    }
}
