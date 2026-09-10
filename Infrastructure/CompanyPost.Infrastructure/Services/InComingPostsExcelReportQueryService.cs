using CompanyPost.Application.CQRS.Services;
using CompanyPost.Application.DTO.Request.Base;
using CompanyPost.Application.DTO.Response;
using LinqKit;

namespace CompanyPost.Infrastructure.Services
{
    internal class InComingPostsExcelReportQueryService
        : IInComingPostsExcelReportQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InComingPostsExcelReportQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<InComingPostDocumentExcelDTO>> GetInComingPostDocumentsAsync(
            BaseDocumentFilterRequestDTO filters,
            Expression<Func<InComing, bool>> predicate,
            IEnumerable<Expression<Func<InComing, object>>> includes,
            CancellationToken cancellationToken = default)
        {
            var repo = _unitOfWork.Repository<InComing>();

            if (filters.StartDate.HasValue)
            {
                predicate = predicate.And(p => p.DocumentDate >= filters.StartDate.Value);
            }
            if (filters.EndDate.HasValue)
            {
                predicate = predicate.And(p => p.DocumentDate <= filters.EndDate.Value);
            }
            if (!string.IsNullOrEmpty(filters.DocumentNumber))
            {
                predicate = predicate.And(p => p.DocumentNumber == filters.DocumentNumber);
            }
            if (!string.IsNullOrEmpty(filters.InComingNumber))
            {
                predicate = predicate.And(p => p.InComingNumber == filters.InComingNumber);
            }

            var incomingPosts = await repo.FindWithIncludeAsync(predicate, includes, cancellationToken);

            var result = incomingPosts.Select(x => new InComingPostDocumentExcelDTO(
                        x.SerialNumber,
                        x.DocumentNumber,
                        x.DocumentDate.ToString("yyyy-MM-dd"),
                        x.DeliveryDate.ToString("yyyy-MM-dd"),
                        x.Subject,
                        x.Summary,
                        x.Notes,
                        x.CreatedBy.Name,
                        x.Publisher.Name,
                        x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")));

            return result;
        }
    }
}
