using DatabaseWork.DataClasses.Tasks;

namespace DatabaseWork.DataClasses
{
    public class TaskDesciplineCompetenceLink
    {
        public int IDtdc { get; set; }

        public Task_d TaskLink { get; set; }
        public DisciplineCompetenceLink FullDCLink { get; set; }
    }
}
