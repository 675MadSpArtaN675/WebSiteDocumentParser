using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocsParserLib.Interfaces;
using DocsParserLib.Exceptions;

namespace DocsParserLib.InputData
{
    /// <summary>
    /// Класс, представляющий документ, из которого будет собрана информация
    /// </summary>
    public class Document : IDataReader<Body>
    {
        private WordprocessingDocument _wordDoc;
        private MainDocumentPart? _mainPart;
        private Body? _body;
        private Document? _document;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Document"/>
        /// </summary>
        /// <param name="file_stream">Поток для считывания из документа</param>
        /// <exception cref="MainPartNotFound">Выбрасывается в случае, если документ не найден.</exception>
        public Document(Stream file_stream)
        {
            _wordDoc = WordprocessingDocument.Open(file_stream, false);
            _mainPart = _wordDoc.MainDocumentPart;

            if (_mainPart is null || _mainPart.Document is null || _mainPart.Document.Body is null)
                throw new MainPartNotFound();

            _body = _mainPart.Document.Body;
        }

        public Document(string filepath) : this(new FileStream(filepath, FileMode.Open))
        { }

        ~Document()
        {
            _wordDoc.Dispose();
        }

        /// <inheritdoc/>
        public Body? GetData()
        {
            return _body;
        }
    }
}
