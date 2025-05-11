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
        public Profile? ProfileLink { get; set; }
    }

    public class SpecGroup
    {
        public int IDsg { get; set; }
        public string? SGNumber { get; set; }
        public string? SGTitle { get; set; }

        public Speciality? Spec { get; set; }
    }

    public class Level
    {
        public int IDlv { get; set; }
        public string? LvTitle { get; set; }

        public Speciality? Spec { get; set; }
    }
}
