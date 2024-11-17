using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsParserLib
{
    public interface IDataReader<T>
    {
        T? GetData();
    }

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
        /// <param name="filename">Путь к документу для сбора информации</param>
        /// <exception cref="MainPartNotFound">Выбрасывается в случае, если документ не найден.</exception>
        public Document(string filename)
        {
            _wordDoc = WordprocessingDocument.Open(filename, false);
            _mainPart = _wordDoc.MainDocumentPart;

            if (_mainPart is null || _mainPart.Document is null || _mainPart.Document.Body is null)
                throw new MainPartNotFound();

            _body = _mainPart.Document.Body;
        }

        ~Document()
        {
            _wordDoc.Dispose();
        }

        public Body? GetData()
        {
            return _body;
        }
    }
}
