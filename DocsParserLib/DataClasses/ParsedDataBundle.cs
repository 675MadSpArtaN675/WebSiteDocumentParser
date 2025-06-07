namespace DocsParserLib.DataClasses
{
    /// <summary>
    /// Структура для хранения всех распарсенных данных, таких как компетенции, вопросы и практические задачи
    /// </summary>
    public class ParsedDataBundle // Хранение всех распарсенных экземпляров
    {
        /// <summary>
        /// Список распарсенных компетенций
        /// </summary>
        public List<Competention> Competentions { get; set; }

        /// <summary>
        /// Список распарсенных вопросов
        /// </summary> 
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Список распарсенных практических задач
        /// </summary>
        public List<PracticTask> PracticTasks { get; set; }

        public Discipline? Discipline { get; set; }

        public ParsedDataBundle()
        {
            Competentions = new List<Competention>();
            Questions = new List<Question>();
            PracticTasks = new List<PracticTask>();

            Discipline = null;
        }

        public Competention? GetCompetentionByName(string name)
        {
            return Competentions.FirstOrDefault(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
