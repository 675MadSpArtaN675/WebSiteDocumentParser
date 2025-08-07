using ParserSiteWork.Models;

public class Extractor
{
    public static IEnumerable<SelectedItemsDTO> GetTasksAnswerVariants(DisplayModel Model, int id_task)
    {
        return Model.SelectedItems.Where(si => si.IdTask == id_task);
    }

    public static IEnumerable<SelectedItemsDTO> GetTasksValidAnswerVariants(DisplayModel Model, int id_task)
    {
        return GetTasksAnswerVariants(Model, id_task).Where(si => si.SelectTrue);
    }

    public static string? GetAnswerVariants(DisplayModel model, TaskDesciplineCompetenceLinkDTO task, Func<DisplayModel, int, IEnumerable<SelectedItemsDTO>> chooser)
    {
        IEnumerable<SelectedItemsDTO> answerVariants = chooser(model, task.IdTask);
        int variants_count = answerVariants.Count();

        if (variants_count > 0)
            return string.Join(";\n", answerVariants.Select(e => e.SelectValue));

        else if (task.TaskCorrectAnswer != null && task.TaskCorrectAnswer != "")
            return task.TaskCorrectAnswer;

        else
            return "Не указано!";
    }
}