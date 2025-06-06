using DocsParserLib.DataClasses;
using DocsParserLib.InputData;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocsParserLib.Parsers.WordParser
{
    /// <summary>
    /// Класс, представляющий парсер компетенций
    /// </summary>
    public class CompetentionParser : WordParser<Competention>
    {
        /// <inheritdoc/>
        public override string[] Filters { get; set; }

        /// <inheritdoc/>
        public override List<Competention>? Data
        {
            get
            {
                if (compets.Count > 0)
                    return compets;

                return null;
            }
        }

        private List<Competention> compets;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CompetentionParser"/>
        /// </summary>
        /// <param name="document">Экземпляр класса <see cref="WordDocument"/>, обозсв начающий документ, из которого нужно спарсить компетенции</param>
        public CompetentionParser(InputData.WordDocument document) : base(document)
        {
            compets = new List<Competention>();
            Filters = new string[] { "ПЕРЕЧЕНЬ", "ПЛАНИРУЕМЫХ", "РЕЗУЛЬТАТОВ" };
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CompetentionParser"/>
        /// </summary>
        /// <param name="filename">Путь и название файла, из которого нужно спарсить компетенции</param>
        public CompetentionParser(string filename) : this(new InputData.WordDocument(filename))
        { }

        /// <inheritdoc/>
        public override List<Competention>? Parse()
        {
            Table? table = FindTableByTitle(Filters);

            table ??= _doc.GetData()?.Elements<Table>().ElementAt(0);

            if (table is null)
            {
                return null;
            }

            IEnumerable<TableRow> competetion_table_rows = table.Elements<TableRow>();

            int rows_count = competetion_table_rows.Count();
            IEnumerable<TableRow> rowsWithoutTitle = competetion_table_rows.Skip(1);

            for (int i = 1; i < rows_count / 2 + 1; i++)
            {
                var row = rowsWithoutTitle.Take(3);
                rowsWithoutTitle = rowsWithoutTitle.Except(row);

                Competention? competetion = CreateCompetention(row);

                if (competetion is not null)
                    compets.Add(competetion);
            }

            compets = compets.GroupBy(n => n.Number).Select(g => g.First()).ToList();
            return compets;
        }

        public Competention? GetCompetentionByName(string name)
        {
            return compets.First(n => n.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public string GetCompetencionDescription(string competention_name)
        {
            IEnumerable<Paragraph>? paragraphs = _doc.GetData()?.Elements<Paragraph>();

            if (paragraphs != null)
                foreach (var paragraph in paragraphs)
                {
                    string paragraph_text = paragraph.InnerText.Trim();

                    if (paragraph_text.Contains(competention_name))
                    {
                        return paragraph_text;
                    }
                }

            return string.Empty;
        }

        private Competention? CreateCompetention(IEnumerable<TableRow> row)
        {
            Competention result = new Competention();
            EvalulationMaterial eval_mat = new EvalulationMaterial();

            IEnumerable<TableCell[]> row_elements = row.Select((elem) => elem.Elements<TableCell>().ToArray());

            int iter = 0;
            foreach (TableCell[] item in row_elements)
            {
                foreach (TableCell cell in item)
                {
                    string text = cell.Elements<Paragraph>().Select(n => n.InnerText).Aggregate((x, y) => $"{x}\n{y}");

                    if (text != "")
                    {
                        switch (iter)
                        {
                            case 0:
                                result.Number = int.Parse(text.Trim());
                                break;

                            case 1:
                                result.Name = text.Trim();
                                break;
                            case 2:
                                string[] sp_text = text.Split('\n');

                                eval_mat.Name = sp_text[0].Trim();
                                eval_mat.Description = sp_text[1].Trim();
                                break;
                            case 3:
                                eval_mat.EM_Type = text.Trim();
                                break;
                            default:
                                eval_mat.EvalulationIndicator = text.Trim();
                                result.EvalulationMaterial.Add(eval_mat);

                                eval_mat = new EvalulationMaterial();
                                break;
                        }

                        iter++;
                    }

                }

                iter = 2;
            }

            if (result.Name == "")
                return null;


            result.Description = GetCompetencionDescription(result.Name);

            return result;
        }

    }

}
