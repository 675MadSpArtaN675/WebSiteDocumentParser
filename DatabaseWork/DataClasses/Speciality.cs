using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWork.DataClasses
{
    public class Speciality
    {
        public int IDspec { get; set; }
        public string? SpecNumber { get; set; }
        public string? SpecTitle { get; set; }

        public SpecGroup? SGroup { get; set; }
        public Level? EdLevel { get; set; }
        public List<Profile>? ProfileLink { get; set; }
    }
}
