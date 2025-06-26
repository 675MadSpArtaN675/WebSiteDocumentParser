using DatabaseWork;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ParserSiteWork.Models;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace ParserSiteWork.Controllers;
// FileExporter/Index
public class FileExporterController : Controller
{
    private DatabaseContext _db;

    public FileExporterController(DatabaseContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Index(string json_model, string? file_extension)
    {
            DisplayModel? model = JsonSerializer.Deserialize<DisplayModel>(json_model, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
                
            if (model is null)
            {
                return Redirect("/DisplayData/Index");
            }

            switch (file_extension)
            {
                case "word":
                    return WordExport(model);
                case "excel":
                    return ExcelExport(model);
                default:
                    return Redirect("/DisplayData/Index");
            }
        

        return Redirect("/DisplayData/Index");
    }


    private IActionResult WordExport(DisplayModel model)
    {
        Console.WriteLine("Wfadfs");
            try
            {
                using (DocX document = DocX.Create(new MemoryStream()))
                {
                    document.MarginLeft = 2f;
                    document.MarginRight = 2f;
                    document.MarginTop = 2f;
                    document.MarginBottom = 2f;

                    string[] headers = { "Профиль", "Компетенция", "Дисциплина", "Текст задания", "Правильный ответ", "Варианты ответов" };

                    Table table = document.AddTable(model.TaskCompetenceDisciplineData.Length + 1, headers.Length);
                    table.Design = TableDesign.TableGrid;

                    for (int i = 0; i < headers.Length; i++)
                    {
                        Paragraph paragraph = table.Rows[0].Cells[i].Paragraphs[0];
                        paragraph.Append(headers[i]);
                        paragraph.Bold();
                        paragraph.Alignment = Alignment.center;
                        table.Rows[0].Cells[i].FillColor = Xceed.Drawing.Color.LightGray;
                    }

                    for (int i = 0; i < model.TaskCompetenceDisciplineData.Length; i++)
                    {
                        string?[] row = GetRow(model, i);
                        for (int j = 0; j < row.Length; j++)
                        {
                            Paragraph paragraph = table.Rows[i + 1].Cells[j].Paragraphs[0];
                            paragraph.Append(row[j]);
                            paragraph.Alignment = Alignment.center;
                        }
                    }

                    table.SetWidthsPercentage(new float[] { 16.66f, 16.66f, 16.66f, 16.66f, 16.66f, 16.66f });

                    MemoryStream stream = new MemoryStream();
                    document.SaveAs(stream);
                    stream.Position = 0;

                    string fileName = "data.docx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось сохранить данные: {ex}");
            }
        

        return Redirect("/DisplayData/Index");
    }

    private IActionResult ExcelExport(DisplayModel model)
    {
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Data");

                    string[] headers = { "Профиль", "Компетенция", "Дисциплина", "Текст задания", "Правильный ответ", "Варианты ответов" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = headers[i];
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    for (int i = 0; i < model.TaskCompetenceDisciplineData.Length; i++)
                    {
                        string?[] row = GetRow(model, i);
                        for (int j = 0; j < row.Length; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = row[j];
                        }
                    }

                    worksheet.Cells.AutoFitColumns();

                    MemoryStream stream = new MemoryStream();
                    excelPackage.SaveAs(stream);
                    stream.Position = 0;

                    string fileName = "result.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Не удалось сохранить данные: {ex}");
            }

        

        return Redirect("/DisplayData/Index");
    }

    private static string?[] GetRow(DisplayModel model, int i)
    {
        var task = model.TaskCompetenceDisciplineData[i];
        return new string?[] {
            task.ProTitle,
            task.CompNumber,
            task.DisTitle,
            task.TaskAnnotation,
            Extractor.GetAnswerVariants(model, task, Extractor.GetTasksValidAnswerVariants),
            Extractor.GetAnswerVariants(model, task, Extractor.GetTasksAnswerVariants)
        };
    }

    private void WriteRow(ExcelWorksheet sheet, string?[] row, int rowNum, int startColumn)
    {
        for (int i = 0; i < row.Length; i++)
            sheet.Cells[rowNum, startColumn + i].Value = row[i];
    }
}