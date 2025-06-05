using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class Profile
    {
        public int IDpro { get; set; }
        public string? ProTitle { get; set; }
        public int ProYear { get; set; }
        public int ProAdminissionYear { get; set; }

        public List<Discipline>? Subject { get; set; }
        public Speciality? Spec { get; set; }
        public List<Competence>? CompetenceLink { get; set; }
    }
}
