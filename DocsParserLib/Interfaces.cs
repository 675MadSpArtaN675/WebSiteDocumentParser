namespace DocsParserLib
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

    public interface IAssessmentItem
    {
        /// <summary>
        /// Номер элемента (от 0)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Описание элемента
        /// </summary>
        public string Description { get; set; }
    }

    public interface ICompetencinable
    {
        /// <summary>
        /// Компетенция, которой привязан экземпляр
        /// </summary>
        public Competention Competention { get; set; }
    }
}
