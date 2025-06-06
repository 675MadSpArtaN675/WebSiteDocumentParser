using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWork.DataClasses
{
    public class Level
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDlv { get; set; }
        public string? LvTitle { get; set; }

        public Speciality? Spec { get; set; }
    }
}
