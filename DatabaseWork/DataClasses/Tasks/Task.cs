using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWork.DataClasses.Tasks
{
    public class Task_d
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDtask { get; set; }
        public string? TaskAnnotation { get; set; }
        public string? TaskCorrectAnswer { get; set; }
        public string? TaskStandartAnswer { get; set; }
        public int TaskTime { get; set; }

        public TypeTask? TaskType { get; set; }


        public List<SelectedItems>? SItems { get; set; }

        public List<ItemsAccordance>? ItAccordance { get; set; }
        public List<FirstPartAccordance>? FPAccordance { get; set; }
        public List<SecondPartAccordance>? SPAccordance { get; set; }

        public List<TaskDesciplineCompetenceLink>? TDCLink { get; set; }

    }

    public class TypeTask
    {
        public int Idtt { get; set; }
        public string? TTTitle { get; set; }
        public string? TTInstruction { get; set; }
        public string? TTScenario { get; set; }
        public string? TTGradingGuide { get; set; }
        public string? TTGradingRules { get; set; }

        public List<Task_d>? TaskLink { get; set; }
    }
}
