using System.Text.Json;
using System.Xml.Serialization;

using DocsParserLib;

namespace Serialization
{
    //
    // Структура, интерфейс и его реализация (файл вывода)
    //


    /// <summary>
    /// Структура для хранения всех распарсенных данных, таких как компетенции, вопросы и практические задачи
    /// </summary>
    public struct ParsedDataBundle // Хранение всех распарсенных экземпляров
    {
        /// <summary>
        /// Список распарсенных компетенций
        /// </summary>
        public List<Competention> Competentions { get; set; }

        /// <summary>
        /// Список распарсенных вопросов
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Список распарсенных практических задач
        /// </summary>
        public List<PracticTask> PracticTasks { get; set; }
    }

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
        ParsedDataBundle GetParsedData(Document document);
    }

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

            c_parser.Parse();
            q_parser.Parse();
            p_parser.Parse();

            return new ParsedDataBundle
            {
                Competentions = c_parser.Data,
                Questions = q_parser.Data,
                PracticTasks = p_parser.Data
            };
        }
    }


    //
    // Объеденение полученных данных с процессом сериализации (Файл сериализации данных)
    //


    /// <summary>
    /// Интерфейс для работы с процессами сериализации и десериализации данных
    /// </summary>
    public interface ISerializationData
    {
        /// <summary>
        /// Сериализует данные типа ParsedDataBundle в файл с использованием указанного механизма сериализации
        /// </summary>
        /// <typeparam name="ParsedDataBundle">Тип данных для сериализации</typeparam>
        /// <param name="data">Данные для сериализации</param>
        /// <param name="filePath">Путь к файлу, в который будут сохранены данные</param>
        /// <param name="serializer">Объект, отвечающий за процесс сериализации</param>
        public void SerializeData<ParsedDataBundle>(ParsedDataBundle data, string filePath, ISerialization serializer);

        /// <summary>
        /// Десериализует данные из файла в объект типа ParsedDataBundle
        /// </summary>
        /// <typeparam name="ParsedDataBundle">Тип данных для десериализации</typeparam>
        /// <param name="filePath">Путь к файлу, из которого будут загружены данные</param>
        /// <param name="serializer">Объект, отвечающий за процесс десериализации</param>
        /// <returns>Объект типа ParsedDataBundle, восстановленный из файла</returns>
        public ParsedDataBundle DeserializeData<ParsedDataBundle>(string filePath, ISerialization serializer);
    }

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


    //
    // Сухая сериализация данных (Файл сухой сериализации)
    //


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