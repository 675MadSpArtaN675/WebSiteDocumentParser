using DocsParserLib.DataClasses;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;

namespace DocsParserLib.Parsers.WordParser
{
    public class DisciplineParser : WordParser<Discipline>
    {
        public override string[] Filters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override List<Discipline>? Data => throw new NotImplementedException();

        public DisciplineParser(InputData.WordDocument document) : base(document)
        { }

        public DisciplineParser(string filename) : this(new InputData.WordDocument(filename))
        { }

        public override List<Discipline>? Parse()
        {
            throw new NotImplementedException();
        }

        public Discipline ParseOneDiscipline()
        {
            Discipline result = new Discipline();
            result.Name = GetDisciplineName();

            return result;
        }

        public string GetDisciplineName()
        {
            Regex pattern = new Regex("([Дд]исциплин[ыу])?«(?<discipline_name>.*)»");

            IEnumerable<Paragraph>? paragraphs = _doc.GetData()?.Elements<Paragraph>();
            
            if (paragraphs != null)
                foreach(var item in paragraphs)
                {
                    string paragraphs_text = item.InnerText.Trim();
                    Match match = pattern.Match(paragraphs_text);

                    if (match.Success)
                    {
                        return match.Groups["discipline_name"].Value;
                    }
                }

            return string.Empty;
        }
    }
}
