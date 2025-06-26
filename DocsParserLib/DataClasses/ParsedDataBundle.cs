using System.Text;

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

        public DocsParserLib.DataClasses.Discipline Discipline { get; set; } = new DocsParserLib.DataClasses.Discipline();

        public ParsedDataBundle()
        {
            Competentions = new List<Competention>();
            Questions = new List<Question>();
            PracticTasks = new List<PracticTask>();
        }

        public Competention? GetCompetentionByName(string name)
        {
            return Competentions.FirstOrDefault(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            PrintList(builder, Competentions);
            PrintList(builder, Questions);
            PrintList(builder, PracticTasks);

            return builder.ToString();
        }

        public void PrintList<T>(StringBuilder builder, List<T> values) where T : class
        {
            foreach (var item in values)
                builder.Append(item.ToString() + "\n");

            builder.Append('\n');
        }

    }
}
