using DocsParserLib.InputData;
using DocsParserLib.DataClasses;

namespace DocsParserLib.Interfaces.Serialization
{
    /// <summary>
    /// Интерфейс для получения распарсенных данных из документа
    /// </summary>
    public interface IDataOutput
    {
        /// <summary>
        /// Возвращает структуру ParsedDataBundle, содержащую все распарсенные данные из документа
        /// </summary>
        /// <param name="document">Документ, который необходимо распарсить</param>
        /// <returns>Структура ParsedDataBundle с распарсенными данными</returns>
        ParsedDataBundle GetParsedData(WordDocument document);
    }
}
