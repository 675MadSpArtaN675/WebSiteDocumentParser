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
        public ParsedDataBundle GetParsedData(Document document)
        {
            CompetentionParser c_parser = new CompetentionParser(document);
            QuestionParser q_parser = new QuestionParser(document, c_parser);
            PracticTasksParser p_parser = new PracticTasksParser(document, c_parser);

            return new ParsedDataBundle
            {
                Competentions = c_parser.Parse(),
                Questions = q_parser.Parse(),
                PracticTasks = p_parser.Parse()
            };
        }
    }
}
