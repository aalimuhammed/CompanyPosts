namespace CompanyPost.Application.CQRS.Services
{
    public interface IExcelReportQueryService<T,TFilter> 
        where T : class 
        where TFilter : class
    {
        Task<IEnumerable<T>> GetExcelReportAsync(
            TFilter filters,
            CancellationToken cancellationToken);
    }
}