using DocsParserLib.DataClasses;
using DocsParserLib.Interfaces.Serialization;
using DocsParserLib.InputData;
using DocsParserLib.Parsers.WordParser;

namespace DocsParserLib.Serialization
{
    /// <summary>
    /// Реализация интерфейса IDataOutput для получения распарсенных данных
    /// </summary>
    public class DataOutput : IDataOutput // Реализация вывода информации
    {
        /// <summary>
        /// Парсит документ, извлекая компетенции, вопросы и практические задачи
        /// </summary>
        /// <param name="document">Документ для парсинга</param>
        /// <returns>Структура ParsedDataBundle с распарсенной информацией</returns>
        public ParsedDataBundle GetParsedData(WordDocument document)
        {
            CompetentionParser c_parser = new CompetentionParser(document);
            QuestionParser q_parser = new QuestionParser(document, c_parser);
            PracticTasksParser p_parser = new PracticTasksParser(document, c_parser);
            DisciplineParser d_parser = new DisciplineParser(document);

            return new ParsedDataBundle(d_parser.ParseOneDiscipline())
            {
                Competentions = c_parser.Parse() ?? new List<Competention>(),
                Questions = q_parser.Parse() ?? new List<Question>(),
                PracticTasks = p_parser.Parse() ?? new List<PracticTask>()
            };
        }
    }
}
