using CompanyPost.Application.DTO;

namespace CompanyPost.Application.Abstraction
{
    public interface IExcelExportService<T> where T : IExcelExportModel
    {
        byte[] ExportToExcel(IEnumerable<T> data);
    }
}