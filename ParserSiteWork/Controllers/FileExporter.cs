using DatabaseWork;
using DatabaseWork.DataClasses;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ParserSiteWork.Models;
using System.Linq;
using System.Text.Json;


namespace ParserSiteWork.Controllers;
// FileExporter/Index
public class FileExporterController : Controller
{
    private readonly string[] HEADERS = ["Профиль", "Компетенция", "Дисциплина", "Текст задания", "Правильный ответ", "Варианты ответов"];

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
    }


    private IActionResult WordExport(DisplayModel model)
    {
        string filename = $"{CreateFilename()}.docx";

        if (System.IO.File.Exists(filename))
            Directory.Delete(filename);

        MemoryStream word_memory = new MemoryStream();

        try
        {
            using (WordprocessingDocument document = WordprocessingDocument.Create(word_memory, WordprocessingDocumentType.Document))
            {
                var main_part = document.AddMainDocumentPart();
                main_part.Document = new Document();
                main_part.Document.Body = new Body();

                Paragraph title_paragraph = new Paragraph(new Run(new Text("Отчет по заданиям")));

                Table order_table = CreateTable(model);

                main_part.Document.Body.Append(title_paragraph, order_table);

                document.Save();
            }

            word_memory.Position = 0;

            return File(word_memory, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", filename);
        }
        catch (IOException ex)
        {
            return Problem("Не удалось открыть файл!");
        }
    }
    private Table CreateTable(DisplayModel model)
    {
        TableProperties props = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                    ));

        Table result = new Table();
        result.AddChild(props);
        result.Append(CreateHeaders(HEADERS));

        FillData(model, ref result);

        return result;
    }

    private void FillData(DisplayModel model, ref Table table)
    {
        foreach (var item in model.TaskCompetenceDisciplineData)
        {
            TableRow row = CreateFilledRow(
                item.ProTitle ?? "-",
                item.CompNumber ?? "-",
                item.DisTitle ?? "-",
                item.TaskAnnotation ?? "-",
                Extractor.GetAnswerVariants(model, item, Extractor.GetTasksValidAnswerVariants) ?? "-",
                Extractor.GetTasksAnswerVariants(model, item.IdTask)
            );

            table.Append(row);
        }

    }

    private TableRow CreateHeaders(string[] headers)
    {
        TableRow row = new TableRow();

        foreach (string element in headers)
            row.Append(CreateCell(element));

        return row;
    }

    private TableRow CreateFilledRow(string profile, string competence, string discipline, string task_text, string correct_answer, IEnumerable<SelectedItemsDTO> variants)
    {
        TableRow row = new TableRow();
        row.Append(CreateCell(profile));
        row.Append(CreateCell(competence));
        row.Append(CreateCell(discipline));
        row.Append(CreateCell(task_text));
        row.Append(CreateCell(correct_answer));
        row.Append(CreateCell(variants, arg =>
        {
            if (arg.Count() < 1)
                return [new Paragraph(new Run(new Text("-")))];

            Paragraph[] paragraphs = new Paragraph[arg.Count()];

            for (int i = 0; i < paragraphs.Length; i++)
            {
                string? text = arg.ElementAt(i)?.SelectValue;

                if (text != null)
                {
                    Paragraph paragraph = new Paragraph();
                    paragraph.AddChild(new Run(new Text(text)));

                    paragraphs[i] = paragraph;
                }
            }

            return paragraphs;
        }));

        return row;
    }

    private TableCell CreateCell(string text)
    {
        TableCell cell = CreateAndConfigureCell();
        cell.AddChild(new Paragraph(new Run(new Text(text))));

        return cell;
    }

    private TableCell CreateCell<T>(T value, Func<T, Paragraph[]> text_pre_processor) where T : class
    {
        TableCell cell = CreateAndConfigureCell();
        Paragraph[] paragraphs = text_pre_processor(value);

        foreach (var item in paragraphs)
            cell.Append(item);

        return cell;
    }

    private TableCell CreateAndConfigureCell()
    {
        return new TableCell(
            new TableCellProperties(
                             new TableCellWidth() { Type = TableWidthUnitValues.Auto }
                             )
            );
    }

    private IActionResult ExcelExport(DisplayModel model)
    {
        string fileName = $"{CreateFilename()}.xlsx";

        try
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Data");
                CreateHeaders(worksheet);
                WriteRows(model, worksheet);

                worksheet.Cells.AutoFitColumns();

                MemoryStream stream = new MemoryStream();
                excelPackage.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Не удалось сохранить данные: {ex}");
        }

        return Redirect("/DisplayData/Index");
    }

    private static void WriteRows(DisplayModel model, ExcelWorksheet worksheet)
    {
        for (int i = 0; i < model.TaskCompetenceDisciplineData.Length; i++)
        {
            string?[] row = GetRow(model, i);
            for (int j = 0; j < row.Length; j++)
            {
                worksheet.Cells[i + 2, j + 1].Value = row[j];
            }
        }
    }

    private void CreateHeaders(ExcelWorksheet worksheet)
    {
        for (int i = 0; i < HEADERS.Length; i++)
        {
            worksheet.Cells[1, i + 1].Value = HEADERS[i];
            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }
    }

    private static string?[] GetRow(DisplayModel model, int i)
    {
        var task = model.TaskCompetenceDisciplineData[i];
        return [
            task.ProTitle,
            task.CompNumber,
            task.DisTitle,
            task.TaskAnnotation,
            Extractor.GetAnswerVariants(model, task, Extractor.GetTasksValidAnswerVariants),
            Extractor.GetAnswerVariants(model, task, Extractor.GetTasksAnswerVariants)
        ];
    }

    private static string CreateFilename()
    {
        DateTime time = DateTime.Now;

        return $"{time.Year}-{time.Month}-{time.Day}_{time.Hour}.{time.Minute}_report";
    }
}