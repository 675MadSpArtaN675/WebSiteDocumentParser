using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWork.DataClasses
{
    public class SpecGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDsg { get; set; }
        public string? SGNumber { get; set; }
        public string? SGTitle { get; set; }

        public Speciality? Spec { get; set; }
    }
}
