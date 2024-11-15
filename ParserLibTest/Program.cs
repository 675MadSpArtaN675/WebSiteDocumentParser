using DocsParserLib;
using Serialize;

namespace ParserLibTest
{ 
    internal class Program
    {
        public static void Main(string[] args)
        {
            string line = "C:\\Users\\Иван\\source\\repos\\WebSiteProject\\ParserLibTest\\ОМ__ТЗWebК_2023.docx";
            Document doc = new Document(line);

            CompetentionParser c_parser = new CompetentionParser(doc);
            QuestionParser q_parser = new QuestionParser(doc, c_parser);
            PracticTasksParser p_parser = new PracticTasksParser(doc, c_parser);

            c_parser.Parse();
            q_parser.Parse();
            p_parser.Parse();

            TestParser(c_parser, q_parser, p_parser);

            string[] files = ["Competentions.xml", "Questions.xml", "Practic.xml"];
            SerializeXML.SerializeToXML(c_parser.Data, files[0]);
            SerializeXML.SerializeToXML(q_parser.Data, files[1]);
            SerializeXML.SerializeToXML(q_parser.Data, files[2]);

            Console.WriteLine("Тип данных в c_parser.Data: " + c_parser.Data.GetType());

            var deserializedData = SerializeXML.DeserializeFromXML<List<Competention>>(files[0]);
            foreach (var item in deserializedData)
                Console.WriteLine(item);
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
