using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors;
using DatabaseWork.Interfaces;
using DocsParserLib.DataClasses;

namespace DatabaseWork.TypeConverters
{
    public class QuestionConverter : ILibTypeConverter<Question, TaskDesciplineCompetenceLink>
    {
        public List<Task_d> Tasks { get; }
        public List<TaskDesciplineCompetenceLink> TDCLinks { get; }

        public QuestionConverter()
        {
            Tasks = new List<Task_d>();
            TDCLinks = new List<TaskDesciplineCompetenceLink>();
        }

        public TaskDesciplineCompetenceLink Convert(Question type, DisciplineCompetenceLink? dc)
        {
            Task_d task = new Task_d {
                TaskAnnotation = type.Description,
                TaskCorrectAnswer = "",
            };

            TaskDesciplineCompetenceLink tdc = new TaskDesciplineCompetenceLink();
            tdc.TaskLink = task;
            
            if (dc != null)
            {
                tdc.FullDCLink = dc;
            }

            Tasks.Add(task);
            TDCLinks.Add(tdc);

            return tdc;
        }

        public List<TaskDesciplineCompetenceLink> ConvertAll(List<Question> list, List<DisciplineCompetenceLink> dc)
        {
            foreach (var element in list)
            {
                DisciplineCompetenceLink? dc_ = dc.FirstOrDefault(e => e.CompetenceLink.CompNumber.Equals(element.Competention.Name, StringComparison.OrdinalIgnoreCase));
                Convert(element, dc_);

            }

            return TDCLinks;
        }

        public List<Task_d> ConvertAll(List<Question> list)
        {
            throw new NotImplementedException();
        }

        public TaskDesciplineCompetenceLink Convert(Question type)
        {
            throw new NotImplementedException();
        }

        List<TaskDesciplineCompetenceLink> ILibTypeConverter<Question, TaskDesciplineCompetenceLink>.ConvertAll(List<Question> list)
        {
            throw new NotImplementedException();
        }
    }
}
