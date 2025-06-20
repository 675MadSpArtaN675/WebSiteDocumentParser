using ParserSiteWork.Models;
using DatabaseWork.DataClasses.Tasks;

public class Extractor
{
    public static IEnumerable<SelectedItems> GetTasksAnswerVariants(DisplayModel Model, Task_d task)
    {
        return Model.SelectedItems.Where(si => si.TaskLink.Equals(task));
    }

    public static IEnumerable<SelectedItems> GetTasksValidAnswerVariants(DisplayModel Model, Task_d task)
    {
        return GetTasksAnswerVariants(Model, task).Where(si => si.SelectTrue);
    }

    public static string? GetAnswerVariants(DisplayModel model, Task_d task, Func<DisplayModel, Task_d, IEnumerable<SelectedItems>> chooser)
    {
        IEnumerable<SelectedItems> answerVariants = chooser(model, task);
        int variants_count = answerVariants.Count();

        if (variants_count > 0)
            return string.Join('\n', answerVariants);

        else
            return task.TaskCorrectAnswer ?? "None";
    }
}