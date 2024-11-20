using DocsParserLib;
using Serialization;

namespace ParserLibTest
{ 
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Тест 1 (Новое получение данных из парсеров)

            string line = "ОМ__ТЗWebК_2023.docx";
            Document doc = new Document(line);

            IDataOutput dataOutput = new DataOutput();

            ParsedDataBundle dataBundle = dataOutput.GetParsedData(doc);

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

            // Тест 2 (Новый подход в сеарилизации)

            Console.WriteLine("Враги наследника трепищите!!!\n");

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

        public static void TestParser(CompetentionParser c_parser, QuestionParser q_parser, PracticTasksParser p_parser)
        {
            string frame = "";
            for (int i = 0; i < 128; i++)
                frame += '=';

            Console.WriteLine(frame);

            foreach (var item1 in c_parser.Data)
                Console.WriteLine(item1);

            Console.WriteLine(frame);

            foreach (var item in q_parser.Data)
                Console.WriteLine(item);

            Console.WriteLine(frame);

            foreach (var item in p_parser.Data)
                Console.WriteLine(item);

            Console.WriteLine(frame);
        }


    }
}
