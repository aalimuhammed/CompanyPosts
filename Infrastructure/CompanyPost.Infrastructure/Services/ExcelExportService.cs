namespace CompanyPost.Infrastructure.Services
{
    internal sealed class ExcelExportService<T> 
        : IExcelExportService<T> where T : IExcelExportModel
    {
        public byte[] ExportToExcel(IEnumerable<T> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(typeof(T).Name);

            worksheet.Cell(1, 1).InsertTable(data);

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}