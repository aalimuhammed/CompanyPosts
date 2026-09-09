using CompanyPost.Application.DTO;

namespace CompanyPost.Application.Abstraction
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel<T>(
        IEnumerable<T> data,
        IEnumerable<ExcelColumn<T>> columns);
    }
}