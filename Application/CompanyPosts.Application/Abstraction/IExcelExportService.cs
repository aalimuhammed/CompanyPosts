namespace CompanyPost.Application.Abstraction
{
<<<<<<< Updated upstream
    public interface IExcelExportService
=======
    public interface IExcelExportService<T> where T : class
>>>>>>> Stashed changes
    {
        byte[] ExportToExcel<T>(
        IEnumerable<T> data,
        IEnumerable<ExcelColumn<T>> columns);
    }
}