using DatabaseWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

using ParserSiteWork.Models;

class FileExporter : Controller
{
    private DatabaseContext _db;

    public FileExporter(DatabaseContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult WordExport(DisplayModel model)
    {
        Microsoft.Office.Interop.Word.Application? application = null;
        Document? document = null;

        if (ModelState.IsValid)
        {
            try
            {
                application = new Microsoft.Office.Interop.Word.Application();
                document = application.Documents.Add();

                document.Sections[1].PageSetup.LeftMargin = application.CentimetersToPoints(2);
                document.Sections[1].PageSetup.RightMargin = application.CentimetersToPoints(2);
                document.Sections[1].PageSetup.TopMargin = application.CentimetersToPoints(2);
                document.Sections[1].PageSetup.BottomMargin = application.CentimetersToPoints(2);

                string[] headers = { "Профиль", "Компетенция", "Дисциплина", "Текст задания", "Правильный ответ", "Варианты ответов" };

                Table table = document.Tables.Add(document.Range(0, 0), model.TaskCompetenceDisciplineData.Count + 1, headers.Length);
                table.Borders.Enable = 1;
                table.AllowAutoFit = true;

                for (int i = 0; i < headers.Length; i++)
                {
                    table.Cell(1, i + 1).Range.Text = headers[i];
                    table.Cell(1, i + 1).Range.Bold = 1;
                }

                for (int i = 0; i < model.TaskCompetenceDisciplineData.Count; i++)
                {
                    string?[] row = GetRow(model, i);
                    for (int j = 0; j < row.Length; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = row[j];
                    }
                }

                string filePath = ".\\temp\\ready.docx";
                document.SaveAs2(filePath, WdSaveFormat.wdFormatDocumentDefault);

                byte[] file_bytes = System.IO.File.ReadAllBytes(filePath);

                return File(file_bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Не удалось сохранить данные: {ex}");
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    Marshal.ReleaseComObject(document);
                }

                if (application != null)
                {
                    application.Quit();
                    Marshal.ReleaseComObject(application);
                }
            }
        }

        return Redirect("/DisplayData/Index");
    }

    [HttpGet]
    public IActionResult ExcelExport(DisplayModel model)
    {
        Microsoft.Office.Interop.Excel.Application? application = null;
        Workbook? workbook = null;
        Worksheet? worksheet = null;

        if (ModelState.IsValid)
        {
            try
            {
                application = new Microsoft.Office.Interop.Excel.Application();

                workbook = application.Workbooks.Add();
                worksheet = (Worksheet)application.ActiveSheet;

                string?[] headers = { "Профиль", "Компетенция", "Дисциплина", "Текст задания", "Правильный ответ", "Варианты ответов" };
                WriteRow(worksheet, headers, 1, 1);

                for (int i = 0; i < model.TaskCompetenceDisciplineData.Count; i++)
                {
                    string?[] row = GetRow(model, i);
                    WriteRow(worksheet, row, i + 2, 1);
                }

                workbook.SaveAs(".\\temp\\ready.xlsx");

                byte[] file_bytes = System.IO.File.ReadAllBytes(".\\temp\\ready.xlsx");
            
                return File(file_bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Не удалось сохранить данные: {ex}");
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }

                if (application != null)
                {
                    application.Quit();
                    Marshal.ReleaseComObject(application);
                }
            }

        }

        return Redirect("/DisplayData/Index");
    }

    private void WriteRow(Worksheet sheet, string?[] row, int row_num, int start_column)
    {
        for (int i = start_column; i < row.Length; i++)
        {
            sheet.Cells[row_num, i] = row[i - start_column];
        }
    }
    private string?[] GetRow(DisplayModel model, int index)
    {
        var tdc = model.TaskCompetenceDisciplineData;

        return new string?[] {
            tdc[index].FullDCLink.CompetenceLink.ProfileLink.ProTitle,
            tdc[index].FullDCLink.CompetenceLink.CompNumber,
            tdc[index].FullDCLink.DisciplineLink.DisTitle,
            tdc[index].TaskLink.TaskAnnotation,
            Extractor.GetAnswerVariants(model, tdc[index].TaskLink, Extractor.GetTasksValidAnswerVariants),
            Extractor.GetAnswerVariants(model, tdc[index].TaskLink, Extractor.GetTasksAnswerVariants)
        };
    }
}