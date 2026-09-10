using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Services
{
    public interface IInComingPostsExcelReportQueryService
    {
        Task<IEnumerable<InComingPostDocumentExcelDTO>> GetInComingPostDocumentsAsync(
            BaseDocumentFilterRequestDTO filters,
            Expression<Func<InComing, bool>> predicate,
            IEnumerable<Expression<Func<InComing, object>>> includes,
            CancellationToken cancellationToken = default);
    }
}
