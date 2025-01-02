using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;
using DocsParserLib.Interfaces;

namespace DocsParserLib.Parsers.WordParser
{
    public abstract class WordParser<T> : Interfaces.IParsable<T>
    {
        protected IDataReader<Body> _doc;
        /// <inheritdoc/>
        public abstract string[] Filters { get; set; }

        /// <inheritdoc/>
        public abstract List<T>? Data { get; }

        /// <inheritdoc/>
        public abstract List<T>? Parse();

        public WordParser(IDataReader<Body> document)
        {
            _doc = document;
        }

        protected string? GetCompetitionNameStr(string title_text)
        {
            Regex comp_pat = new Regex(@"([А-Я]{2}-\d+)\s*");
            Match match = comp_pat.Match(title_text.Trim());

            if (match.Success && match.Value != "")
                return match.Value.Trim();

            return null;
        }

        protected List<Y>? ReadTable<Y>(string[] filters, Action<Table?, List<Y>> read_rows)
        {
            Regex pattern = CreateFilterPattern(filters);

            List<Y> result = new List<Y>();

            Table? question_table = FindTableByTitle(filters);

            if (question_table is null)
                return null;

            var par_1 = question_table.PreviousSibling<Paragraph>();
            var par_2 = par_1?.PreviousSibling<Paragraph>();

            while (question_table is not null)
            {
                if (pattern.Matches($"{par_1?.InnerText} {par_2?.InnerText}").Count == filters.Length)
                {
                    read_rows(question_table, result);
                }
                else
                    break;

                question_table = question_table.NextSibling<Table>();

                FindPrevTitle(in question_table, ref par_1, ref par_2);
            }

            return result;
        }

        protected void FindPrevTitle(in Table? question_table, ref Paragraph? par_1, ref Paragraph? par_2)
        {
            if (question_table is not null)
            {
                par_2 = question_table.PreviousSibling<Paragraph>();
                par_1 = par_2?.PreviousSibling<Paragraph>();

                while (par_1.InnerText == "" || par_2.InnerText == "")
                {
                    par_2 = par_2.PreviousSibling<Paragraph>();
                    par_1 = par_2.PreviousSibling<Paragraph>();
                }
            }
        }

        protected Table? FindTableByTitle(string[] filters)
        {
            if (_doc.GetData() is null) return null;

            var paragraphs = _doc.GetData()?.Elements<Paragraph>().ToArray();
            Regex title_pattern = CreateFilterPattern(filters);

            foreach (var paragraph in paragraphs)
            {
                string par_text = UnionRuns(paragraph);

                if (title_pattern.Matches(par_text).Count() == filters.Length)
                    return paragraph.NextSibling<Table>();

                Paragraph? next_par = paragraph.NextSibling<Paragraph>();
                string par_text_2 = UnionRuns(next_par);

                par_text += " " + par_text_2;

                if (title_pattern.Matches(par_text).Count() == filters.Length)
                    return next_par?.NextSibling<Table>();
            }

            return null;
        }

        protected string UnionRuns(Paragraph? paragraph)
        {
            if (paragraph == null) return "";

            var run_texts = paragraph.Elements<Run>().Select(n => n.InnerText);

            string par_text = "";
            if (run_texts.Count() > 0)
                par_text = run_texts.Aggregate((x, y) => $"{x} {y}");

            return par_text;
        }

        protected TableRow GetTitleRow(Table table)
        {
            return table.Elements<TableRow>().ElementAt(0);
        }

        protected Regex CreateFilterPattern(string[] filters)
        {
            string pat = string.Join('|', EscapingSequences(filters).Select(n => $"({n.Replace("-", "\\-")})"));
            return new Regex(pat, RegexOptions.IgnoreCase);
        }

        protected string[] EscapingSequences(string[] filters)
        {
            Regex change_symbols = new Regex(@"([\-\[\]\(\)])");

            for (int i = 0; i < filters.Length; i++)
            {
                MatchCollection m_colls = change_symbols.Matches(filters[i]);

                if (m_colls.Count > 0)
                    filters = DestroyEscapes(i, filters, m_colls);
            }

            return filters;
        }

        protected string[] DestroyEscapes(int line_num, string[] lines, MatchCollection matches)
        {
            foreach (Match item in matches)
                lines[line_num] = lines[line_num].Replace(item.Value, $"\\{item.Value}");

            return lines;
        }
    }
}
