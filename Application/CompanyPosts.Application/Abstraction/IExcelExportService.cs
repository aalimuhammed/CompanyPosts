namespace CompanyPost.Application.Abstraction
{
    public interface IExcelExportService<T> where T : class
    {
        byte[] ExportToExcel<T>(IEnumerable<T> data);
    }
}