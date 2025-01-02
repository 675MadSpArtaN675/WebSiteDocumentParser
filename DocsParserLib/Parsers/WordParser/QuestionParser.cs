using System.Text.RegularExpressions;
using DocsParserLib.DataClasses;
using DocsParserLib.InputData;
using DocumentFormat.OpenXml.Wordprocessing;


namespace DocsParserLib.Parsers.WordParser
{
    /// <summary>
    /// Класс, представляющий парсер вопросов
    /// </summary>
    public class QuestionParser : WordParser<Question>
    {
        /// <inheritdoc/>
        public override string[] Filters { get; set; }
        /// <inheritdoc/>
        public override List<Question>? Data
        {
            get
            {
                if (_questions.Count() > 0)
                    return _questions;

                return null;
            }
        }

        private CompetentionParser _comp_parser;
        private List<Question> _questions;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="QuestionParser"></see>
        /// </summary>
        /// <param name="document">Документ, из которого нужно спарсить данные</param>
        /// <param name="comp_parser">Парсер компетенций</param>
        public QuestionParser(InputData.WordDocument document, CompetentionParser comp_parser) : base(document)
        {
            if (comp_parser.Data is null)
                comp_parser.Parse();

            _comp_parser = comp_parser;
            _questions = new List<Question>();
            Filters = new string[] { "результатов", "компетенций", "вопросы" };
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="QuestionParser">
        /// </summary>
        /// <param name="filename">Путь к документу, из которого нужно спарсить данные</param>
        /// <param name="comp_parser">Парсер компетенций</param>
        public QuestionParser(string filename, CompetentionParser comp_parser) : this(new InputData.WordDocument(filename), comp_parser)
        { }

        /// <inheritdoc/>
        public override List<Question>? Parse()
        {
            return ReadTable<Question>(Filters, (question_table, questions) =>
            {
                IEnumerable<TableRow> table_rows = question_table.Elements<TableRow>();
                IEnumerable<TableCell> questions_cells = table_rows.Skip(1).SelectMany(n => n.Elements<TableCell>());

                string title = table_rows.ElementAt(0).InnerText.Trim();
                string? title_match = GetCompetitionNameStr(title);

                Competention? comp = _comp_parser.GetCompetentionByName(title_match);

                ReadQuestions(questions, questions_cells, comp ?? new Competention());
                _questions = questions;
            });

        }

        private void ReadQuestions(List<Question> questions_list, IEnumerable<TableCell> questions_cells, Competention competention)
        {
            foreach (var item in questions_cells)
            {
                int question_num = 1;
                foreach (var item1 in item.Elements<Paragraph>())
                {
                    string question_description = item1.InnerText;

                    if (question_description.Length > 0)
                    {
                        Question question = new Question(question_num, competention, question_description);
                        questions_list.Add(question);

                        question_num++;
                    }
                }

            }
        }
    }

}
