using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Services
{
    public interface IPostsExcelReportQueryService<T> where T : PostBaseEntity
    {
        Task<IEnumerable<PostDocumentExcelDTO>> GetPostDocumentsAsync(
            BaseDocumentFilterRequestDTO filters,
            Expression<Func<T, bool>> predicate ,
            IEnumerable<Expression<Func<T, object>>> includes,
            CancellationToken cancellationToken = default);
    }
}