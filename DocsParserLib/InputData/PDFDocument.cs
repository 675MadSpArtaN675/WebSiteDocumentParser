using System.Text.RegularExpressions;
using System.Text;

using UglyToad.PdfPig;

using DocsParserLib.Interfaces;
using DocsParserLib.Exceptions;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis;
using System.Formats.Tar;


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
            string competentions = FindTableByColumnAndGetAllText("(№\\s*п/п.*)", "Описание показателей");
            string questions = FindTableByColumnAndGetAllText("Тестовые задания для оценки.*сформированность компетенций(.*)", "Практические задания");
            string practic_tasks = FindTableByColumnAndGetAllText("Практические задания.*сформированность компетенций(.*)", "");

            return new List<string> { competentions, questions, practic_tasks }.AsEnumerable();
        }

        private string FindTableByColumnAndGetAllText(string column_name, string title_of_next_page)
        {
            Regex columnPattern = new(column_name, RegexOptions.IgnoreCase);
            Regex? title_of_next_page_pattern = null;

            if (title_of_next_page != "")
                title_of_next_page_pattern = new(title_of_next_page, RegexOptions.IgnoreCase);

            StringBuilder TableTextBuilder = new StringBuilder();

            bool is_scrapping_info_started = false;
            foreach (var page in pdfDocument.GetPages())
            {
                string page_text = page.Text;

                if (string.IsNullOrEmpty(page_text))
                {
                    continue;
                }

                if (title_of_next_page_pattern is not null && title_of_next_page_pattern.IsMatch(page_text))
                {
                    break;
                }

                Match TableStart = columnPattern.Match(page_text);

                if (TableStart.Success)
                {
                    TableTextBuilder.Append(TableStart.Groups[1]);

                    is_scrapping_info_started = true;
                    continue;
                }

                if (is_scrapping_info_started && !HasImageOnPage(page))
                {
                    TableTextBuilder.Append(page_text);
                }

            }

            return TableTextBuilder.ToString();
        }

        private bool HasImageOnPage(Page page)
        {
            var images = page.GetImages();

            if (images.Count() > 0)
            {
                return true;
            }

            return false;
        }

    }
}
