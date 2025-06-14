using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;

namespace DatabaseWork.TypeConverters.DataClasses
{
    public class ConvertedDataBundle
    {
        public List<Competence> Competences { get; }
        public List<Task_d> Tasks { get; }
        public List<SelectedItems> SelectedItems { get; }
        public List<Discipline> Discplines { get; }

        public List<DisciplineCompetenceLink> DCLink { get; set; }
        public List<TaskDesciplineCompetenceLink> TDCLinks { get; set; }

        public ConvertedDataBundle() : this(new List<Competence>(), new List<Task_d>(), new List<SelectedItems>(), new List<Discipline>()) { }

        public ConvertedDataBundle(List<Competence> competences, List<Task_d> tasks, List<SelectedItems> selectedItems, List<Discipline> discplines)
        {
            Competences = competences;
            Tasks = tasks;
            SelectedItems = selectedItems;
            Discplines = discplines;
            DCLink = new();
            TDCLinks = new();
        }
    }
}
