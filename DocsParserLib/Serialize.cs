using System.Xml.Serialization;

namespace Serialize
{
    /// <summary>
    /// Статический класс сериализации в XML
    /// </summary>
    public static class SerializeXML
    {
        public static void SerializeToXML<T>(T parsObject, string filePath) 
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath))
            { 
                serializer.Serialize(writer, parsObject);
            }
        }

        public static T DeserializeFromXML<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}