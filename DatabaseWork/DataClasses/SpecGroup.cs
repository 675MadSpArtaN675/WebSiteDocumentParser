namespace DatabaseWork.DataClasses
{
    public class SpecGroup
    {
        public int IDsg { get; set; }
        public string? SGNumber { get; set; }
        public string? SGTitle { get; set; }

        public Speciality? Spec { get; set; }
    }
}
