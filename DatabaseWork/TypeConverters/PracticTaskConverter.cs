using DatabaseWork.DataClasses;
using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.DataProcessors.StandartProcessors;
using DatabaseWork.Interfaces;
using DatabaseWork.TypeConverters.UtilityTypes;
using DocsParserLib.DataClasses;

namespace DatabaseWork.TypeConverters
{
    public class PracticTaskConverter : ILibTypeConverter<PracticTask, TaskDesciplineCompetenceLink>
    {
        public List<Task_d> Tasks { get; }
        public List<SelectedItems> AnswerVariants { get; }
        public List<TaskDesciplineCompetenceLink> TDCLinks { get; }

        public PracticTaskConverter()
        {
            Tasks = new List<Task_d>();
            AnswerVariants = new List<SelectedItems>();
            TDCLinks = new List<TaskDesciplineCompetenceLink>();
        }

        public TaskDesciplineCompetenceLink Convert(PracticTask type, DisciplineCompetenceLink? dc)
        {
            AnswerVariantConverter answerVariantConverter = new AnswerVariantConverter();
            TaskDesciplineCompetenceLink tdc = new TaskDesciplineCompetenceLink();

            Task_d task = new Task_d
            {
                TaskAnnotation = type.Description,

            };

            List<SelectedItems> aw = answerVariantConverter.ConvertAll(type.answerVariants);
            
            for (int i = 0; i < aw.Count; i++)
            {
                SelectedItems item = aw[i];

                Console.WriteLine($"{task.TaskAnnotation} {item.SelectValue}");
                item.TaskLink = task;
            }

            tdc.TaskLink = task;

            if (dc != null)
            {
                tdc.FullDCLink = dc;
            }

            AnswerVariants.AddRange(aw);
            Tasks.Add(task);
            TDCLinks.Add(tdc);

            return tdc;
        }

        public List<TaskDesciplineCompetenceLink> ConvertAll(List<PracticTask> list, List<DisciplineCompetenceLink> dc)
        {
            foreach (var item in list)
            {
                DisciplineCompetenceLink? dc_ = dc.FirstOrDefault(e => e.CompetenceLink.CompNumber.Equals(item.Competention.Name, StringComparison.OrdinalIgnoreCase));
                Convert(item, dc_);
            }

            return TDCLinks;
        }

        public TaskDesciplineCompetenceLink Convert(PracticTask type)
        {
            throw new NotImplementedException();
        }

        public List<TaskDesciplineCompetenceLink> ConvertAll(List<PracticTask> list)
        {
            throw new NotImplementedException();
        }
    }
}
