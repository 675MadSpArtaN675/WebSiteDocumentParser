using System.Text.Json;
using DocsParserLib.Interfaces.Serialization;

namespace DocsParserLib.Serialization
{
    public class SerializeJSON : ISerialization
    {
        /// <summary>
        /// Сериализует объект указанного типа в JSON-файл по заданному пути
        /// </summary>
        /// <typeparam name="T">Тип объекта, который необходимо сериализовать</typeparam>
        /// <param name="parsObject">Объект для сериализации</param>
        /// <param name="filePath">Путь к файлу, в который будет сохранен сериализованный JSON</param>
        public void Serialize<T>(T parsObject, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(parsObject);
            File.WriteAllText(filePath, jsonString);
        }

        /// <summary>
        /// Десериализует JSON-файл по заданному пути в объект указанного типа
        /// </summary>
        /// <typeparam name="T">Тип объекта, в который необходимо десериализовать данные</typeparam>
        /// <param name="filePath">Путь к JSON-файлу, из которого будет загружен объект</param>
        /// <returns>Объект типа <typeparamref name="T"/>, восстановленный из JSON</returns>
        public T Deserialize<T>(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}