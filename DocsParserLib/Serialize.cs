using System.Text.Json;
using System.Xml.Serialization;

namespace Serialize
{
    /// <summary>
    /// Статический класс для выполнения сериализации и десериализации объектов в формате XML
    /// </summary>
    public static class SerializeXML
    {
        /// <summary>
        /// Сериализует объект указанного типа в XML-файл по заданному пути
        /// </summary>
        /// <typeparam name="T">Тип объекта, который необходимо сериализовать</typeparam>
        /// <param name="parsObject">Объект для сериализации</param>
        /// <param name="filePath">Путь к файлу, в который будет сохранен сериализованный XML</param>
        public static void SerializeToXML<T>(T parsObject, string filePath) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath))
            { 
                serializer.Serialize(writer, parsObject);
            }
        }

        /// <summary>
        /// Десериализует XML-файл по заданному пути в объект указанного типа
        /// </summary>
        /// <typeparam name="T">Тип объекта, в который необходимо десериализовать данные</typeparam>
        /// <param name="filePath">Путь к XML-файлу, из которого будет загружен объект</param>
        /// <returns>Объект типа <typeparamref name="T"/>, восстановленный из XML</returns>
        public static T DeserializeFromXML<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Сериализует объект указанного типа в JSON-файл по заданному пути
        /// </summary>
        /// <typeparam name="T">Тип объекта, который необходимо сериализовать</typeparam>
        /// <param name="parsObject">Объект для сериализации</param>
        /// <param name="filePath">Путь к файлу, в который будет сохранен сериализованный JSON</param>
        public static void SerializeToJSON<T>(T parsObject, string filePath)
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
        public static T DeserializeFromJSON<T>(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}