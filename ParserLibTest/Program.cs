using DocsParserLib.InputData;
using DocsParserLib.Interfaces.Serialization;
using DocsParserLib.DataClasses;
using DocsParserLib.Serialization;
using DocsParserLib.Parsers.WordParser;

namespace ParserLibTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            TestParser();
        }

        public static ParsedDataBundle TestParseWordFile()
        {
            string? line = "C:\\Users\\Иван\\source\\repos\\WebSiteProject\\ОМ__ТЗWebК_2023.docx";

            WordDocument doc = new WordDocument(line);

            IDataOutput dataOutput = new DataOutput();
            ParsedDataBundle dataBundle = dataOutput.GetParsedData(doc);

            return dataBundle;
        }

        public static void TestParser()
        {
            ParsedDataBundle dataBundle = TestParseWordFile();

            string frame = "";
            for (int i = 0; i < 128; i++)
                frame += '=';

            foreach (var competention in dataBundle.Competentions)
                Console.WriteLine(competention);

            Console.WriteLine(frame);

            foreach (var question in dataBundle.Questions)
                Console.WriteLine(question);

            Console.WriteLine(frame);

            foreach (var task in dataBundle.PracticTasks)
                Console.WriteLine(task);

            Console.WriteLine(frame);
        }

        public static void Test2()
        {
            ParsedDataBundle dataBundle = TestParseWordFile();

            ISerialization xmlSerializer = new SerializeXML();
            ISerialization jsonSerializer = new SerializeJSON();

            string xmlFilePath = "data.xml";
            string jsonFilePath = "data.json";

            SerializationData serializationData = new SerializationData();

            serializationData.SerializeData(dataBundle, xmlFilePath, xmlSerializer);
            serializationData.SerializeData(dataBundle, jsonFilePath, jsonSerializer);

            // Тест 2 (Новый подход в десеарилизации)
            ParsedDataBundle xmlData = serializationData.DeserializeData<ParsedDataBundle>(xmlFilePath, xmlSerializer);
            ParsedDataBundle jsonData = serializationData.DeserializeData<ParsedDataBundle>(jsonFilePath, jsonSerializer);

            Console.WriteLine("Компетенции из XML:");
            foreach (var comp in xmlData.Competentions)
                Console.WriteLine(comp);

            Console.WriteLine("Вопросы из JSON:");
            foreach (var question in jsonData.Questions)
                Console.WriteLine(question);
        }


    }
}
