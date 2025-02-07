using UglyToad.PdfPig;
using DocsParserLib.Interfaces;

namespace DocsParserLib.InputData
{
    public class PDFDocument : IDataReader<IEnumerable<string>>
    {
        private PdfDocument pdfDocument;

        public PDFDocument(string path)
        {
            pdfDocument = PdfDocument.Open(path);
        }

        ~PDFDocument()
        {
            pdfDocument.Dispose();
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetData()
        {
            return;
        }

        private void FindTableByTitle(string title)
        {
            return;
        }

        private void FindTableByColumns(string[] columns)
        {
            return;
        }
    }
}
