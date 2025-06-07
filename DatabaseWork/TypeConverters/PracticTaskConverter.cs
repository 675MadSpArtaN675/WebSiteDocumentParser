using DatabaseWork.DataClasses.Tasks;
using DatabaseWork.Interfaces;
using DatabaseWork.TypeConverters.UtilityTypes;
using DocsParserLib.DataClasses;

namespace DatabaseWork.TypeConverters
{
    public class PracticTaskConverter : ILibTypeConverter<PracticTask, Task_d>
    {
        public List<Task_d> Tasks { get; }
        public List<SelectedItems> AnswerVariants { get; }

        public PracticTaskConverter()
        {
            Tasks = new List<Task_d>();
            AnswerVariants = new List<SelectedItems>();
        }

        public Task_d Convert(PracticTask type)
        {
            AnswerVariantConverter answerVariantConverter = new AnswerVariantConverter();

            Task_d task = new Task_d
            {
                TaskAnnotation = type.Description,

            };

            List<SelectedItems> aw = answerVariantConverter.ConvertAll(type.answerVariants);
            
            foreach (var item in aw)
            {
                item.TaskLink = task;
            }

            AnswerVariants.AddRange(aw);
            Tasks.Add(task);

            return task;
        }

        public List<Task_d> ConvertAll(List<PracticTask> list)
        {
            foreach (var item in list)
            {
                Convert(item);
            }

            return Tasks;
        }
    }
}
