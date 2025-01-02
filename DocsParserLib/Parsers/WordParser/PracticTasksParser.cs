using DocsParserLib.DataClasses;
using DocsParserLib.InputData;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocsParserLib.Parsers.WordParser
{
    /// <summary>
    /// Класс, представляющий парсер практических заданий
    /// </summary>
    public class PracticTasksParser : WordParser<PracticTask>
    {
        /// <inheritdoc/>
        public override string[] Filters { get; set; }
        /// <inheritdoc/>
        public override List<PracticTask>? Data
        {
            get
            {
                if (_practicTasks.Count() > 0)
                    return _practicTasks;

                return null;
            }
        }

        private List<PracticTask> _practicTasks;
        private CompetentionParser _comp_parser;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="PracticTasksParser"></see>
        /// </summary>
        /// <param name="document">Документ, из которого нужно спарсить данные</param>
        /// <param name="comp_parser">Парсер компетенций</param>
        public PracticTasksParser(InputData.Document document, CompetentionParser comp_parser) : base(document)
        {
            if (comp_parser.Data is null)
                comp_parser.Parse();

            _comp_parser = comp_parser;
            _practicTasks = new List<PracticTask>();

            Filters = ["Практические", "задания", "результатов"];
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="PracticTasksParser"></see>
        /// </summary>
        /// <param name="filename">Путь к документу, из которого нужно спарсить данные</param>
        /// <param name="comp_parser">Парсер компетенций</param>
        public PracticTasksParser(string filename, CompetentionParser comp_parser) : this(new InputData.Document(filename), comp_parser)
        { }

        /// <inheritdoc/>
        public override List<PracticTask>? Parse()
        {
            var result_tasks = ReadTable<PracticTask>(Filters,
                (question_table, tasks) =>
                {
                    List<TableRow> tasks_rows = question_table.Elements<TableRow>().ToList();

                    int question_num = 1;
                    Competention? comp = null;
                    foreach (TableRow row in tasks_rows)
                    {
                        string? title = GetCompetitionNameStr(row.InnerText);

                        if (title is not null)
                        {
                            comp = _comp_parser.GetCompetentionByName(title);
                            question_num = 1;
                            continue;
                        }

                        tasks.Add(PracticeTaskRowParse(row, question_num, comp ?? new Competention()));
                        question_num++;
                    }
                });

            _practicTasks = result_tasks;
            return result_tasks;
        }

        private PracticTask PracticeTaskRowParse(TableRow row, int question_num, Competention comp)
        {
            IEnumerable<Paragraph> answer = row.Elements<TableCell>().ElementAt(1).Elements<Paragraph>();

            string title = answer.ElementAt(0).InnerText.Trim();

            List<AnswerVariant> variants = new List<AnswerVariant>();

            int answer_variant_number = 0;
            foreach (var paragraph in answer.Skip(1))
            {
                string answer_description = paragraph.InnerText.Trim();

                bool valid_variant = false;

                if (answer_description != "")
                {
                    if (paragraph.Elements<Run>().Any(n => n.RunProperties?.Bold is not null))
                        valid_variant = true;

                    variants.Add(new AnswerVariant(answer_variant_number, answer_description, valid_variant));
                    answer_variant_number++;
                }
                else
                    continue;
            }

            PracticTask task = new PracticTask(question_num, comp, title, variants);
            return task;
        }
    }
}
