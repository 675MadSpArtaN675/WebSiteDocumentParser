namespace DocsParserLib
{
    public class MainPartNotFound : Exception
    {
        public MainPartNotFound(string message) : base(message) { }
        public MainPartNotFound() : base("Основная часть файла не найдена") { }
    }
}
