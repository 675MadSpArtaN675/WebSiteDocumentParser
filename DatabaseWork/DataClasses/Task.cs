using System.ComponentModel.DataAnnotations;

namespace DatabaseWork.DataClasses
{
    public class Task_d
    {
        [Key]
        public int IDtask { get; set; }
        public string? TaskAnnotation { get; set; }
        public string? TaskCorrectAnswer { get; set; }
        public string? TaskStandartAnswer { get; set; }
        public int TaskTime { get; set; }
    }

    public class TypeTask
    {
        [Key]
        public int Idtt { get; set; }
        public string? TTTitle { get; set; }
        public string? TTInstruction { get; set; }
        public string? TTScenario { get; set; }
        public string? TTGradingGuide { get; set; }
        public string? TTGradingRules { get; set; }
    }

    public class SelectedItems
    {
        [Key]
        public int IDSelect { get; set; }
        public string? SelectValue { get; set; }
        public bool SelectTrue { get; set; }
    }
}
