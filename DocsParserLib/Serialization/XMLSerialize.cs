using DocsParserLib.Interfaces.Serialization;
using System.Xml.Serialization;

namespace DocsParserLib.Serialization
{
    /// <summary>
    /// Класс для выполнения сериализации и десериализации объектов в формате XML
    /// </summary>
    public class SerializeXML : ISerialization
    {
        /// <summary>
        /// Сериализует объект указанного типа в XML-файл по заданному пути
        /// </summary>
        /// <typeparam name="T">Тип объекта, который необходимо сериализовать</typeparam>
        /// <param name="parsObject">Объект для сериализации</param>
        /// <param name="filePath">Путь к файлу, в который будет сохранен сериализованный XML</param>
        public void Serialize<T>(T parsObject, string filePath)
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
        public T Deserialize<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
