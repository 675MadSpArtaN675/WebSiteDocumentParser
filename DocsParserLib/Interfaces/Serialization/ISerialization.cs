namespace DocsParserLib.Interfaces.Serialization
{
    /// <summary>
    /// Интерфейс для выполнения процессов сериализации и десериализации данных
    /// </summary>
    public interface ISerialization
    {
        /// <summary>
        /// Сериализует объект указанного типа в файл по заданному пути
        /// </summary>
        /// <typeparam name="T">Тип объекта, который необходимо сериализовать</typeparam>
        /// <param name="parsObject">Объект для сериализации</param>
        /// <param name="filePath">Путь к файлу, в который будут сохранены сериализованные данные</param>
        public void Serialize<T>(T parsObject, string filePath);

        /// <summary>
        /// Десериализует данные из файла в объект указанного типа
        /// </summary>
        /// <typeparam name="T">Тип объекта, в который необходимо десериализовать данные</typeparam>
        /// <param name="filePath">Путь к файлу с данными</param>
        /// <returns>Объект типа <typeparamref name="T"/>, восстановленный из данных файла</returns>
        public T Deserialize<T>(string filePath);
    }
}
