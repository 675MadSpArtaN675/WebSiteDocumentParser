namespace DocsParserLib.Interfaces
{
    public interface IParsable<T>
    {
        /// <summary>
        /// Получить фильтры поиска таблицы
        /// </summary>
        string[] Filters { get; set; }

        /// <summary>
        /// Получить, собранную из документа, информацию
        /// </summary>
        List<T>? Data { get; }

        /// <summary>
        /// Выполняет парсинг документа. Кэширует полученную информацию в свойство <see cref="Data"/>.
        /// </summary>
        /// <returns>Список из полученных строк/ячеек таблицы (Зависит от настроек конкретного парсера)</returns>
        List<T>? Parse();
    }
}
