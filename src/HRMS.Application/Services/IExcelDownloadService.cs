using ClosedXML.Excel;
using System.Data;
using System.Reflection;

namespace HRMS.Application.Services;

public interface IExcelDownloadService
{
    byte[] GenerateExcelFromList<T>(IEnumerable<T> items, string sheetName = "Sheet1");
    byte[] GenerateExcelFromDataTable(DataTable table, string sheetName = "Sheet1");
}

public class ExcelService : IExcelDownloadService
{
    public byte[] GenerateExcelFromList<T>(IEnumerable<T> items, string sheetName = "Sheet1")
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.AddWorksheet(sheetName);

        // Insert full list automatically (NO foreach)
        ws.Cell(1, 1).InsertData(items);

        // Header bold
        ws.Row(1).Style.Font.Bold = true;

        // Column auto width
        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] GenerateExcelFromDataTable(DataTable table, string sheetName = "Sheet1")
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add(table, sheetName);

        // Format headers
        ws.Row(1).Style.Font.Bold = true;

        // Auto adjust width
        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);
        return ms.ToArray();
    }
}
