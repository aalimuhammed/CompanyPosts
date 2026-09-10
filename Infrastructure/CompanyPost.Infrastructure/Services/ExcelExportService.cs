namespace CompanyPost.Infrastructure.Services;

public class ExcelExportService : IExcelExportService
{
<<<<<<< Updated upstream
    public byte[] ExportToExcel<T>(
        IEnumerable<T> data,
        IEnumerable<ExcelColumn<T>> columns)
=======
    internal sealed class ExcelExportService<T> 
        : IExcelExportService<T> where T : class
>>>>>>> Stashed changes
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Report");

        var columnList = columns.ToList();

        // Headers
        for (int columnIndex = 0; columnIndex < columnList.Count; columnIndex++)
        {
            worksheet.Cell(1, columnIndex + 1).Value =
                columnList[columnIndex].ColumnName;
        }

        // Data
        var rowIndex = 2;

        foreach (var item in data)
        {
            for (int columnIndex = 0; columnIndex < columnList.Count; columnIndex++)
            {
                var value = columnList[columnIndex].ColumnValue(item);

                worksheet.Cell(rowIndex, columnIndex + 1).Value =
                    value?.ToString() ?? string.Empty;
            }

            rowIndex++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}
//public byte[] ExportToExcel(IEnumerable<T> data)
//{
//    using var workbook = new XLWorkbook();
//    var worksheet = workbook.Worksheets.Add(typeof(T).Name);

//    worksheet.Cell(1, 1).InsertTable(data);

//    worksheet.Columns().AdjustToContents();

//    using var stream = new MemoryStream();
//    workbook.SaveAs(stream);

//    return stream.ToArray();
//}