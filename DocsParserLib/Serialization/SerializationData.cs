using DocsParserLib.Interfaces.Serialization;

namespace DocsParserLib.Serialization
{
    /// <summary>
    /// Реализация интерфейса ISerializationData для объединения процесса сериализации и десериализации
    /// </summary>
    public class SerializationData : ISerializationData
    {
        /// <summary>
        /// Выполняет сериализацию данных в файл
        /// </summary>
        /// <typeparam name="T">Тип данных для сериализации</typeparam>
        /// <param name="data">Объект данных</param>
        /// <param name="filePath">Путь к файлу для сохранения</param>
        /// <param name="serializer">Объект для выполнения сериализации</param>
        public void SerializeData<T>(T data, string filePath, ISerialization serializer)
        {
            serializer.Serialize(data, filePath);
        }

        /// <summary>
        /// Выполняет десериализацию данных из файла
        /// </summary>
        /// <typeparam name="T">Тип данных для десериализации</typeparam>
        /// <param name="filePath">Путь к файлу с данными</param>
        /// <param name="serializer">Объект для выполнения десериализации</param>
        /// <returns>Объект типа <typeparamref name="T"/></returns>
        public T DeserializeData<T>(string filePath, ISerialization serializer)
        {
            return serializer.Deserialize<T>(filePath);
        }
    }
}
