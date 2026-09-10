using CompanyPost.Application.CQRS.Services;
using CompanyPost.Application.DTO.Request.Base;
using CompanyPost.Application.DTO.Response;
using CompanyPost.Application.Extension;
using LinqKit;

namespace CompanyPost.Infrastructure.Services
{
    internal class PostsExcelReportQueryService<T> : 
        IPostsExcelReportQueryService<T> where T : PostBaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostsExcelReportQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PostDocumentExcelDTO>> GetPostDocumentsAsync(
            BaseDocumentFilterRequestDTO filters, 
            Expression<Func<T, bool>> predicate = null,
             IEnumerable<Expression<Func<T, object>>> includes = null,
            CancellationToken cancellationToken = default)
        {
            var repo = _unitOfWork.Repository<T>();

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
                predicate = predicate.And(p => p.DocumentNumber ==  filters.DocumentNumber);
            }
            if (!string.IsNullOrEmpty(filters.InComingNumber))
            {
                predicate = predicate.And(p => p.InComingNumber == filters.InComingNumber);
            }

            var posts = await repo.FindWithIncludeAsync(predicate, includes, cancellationToken);

            var result = posts.Select(x => new PostDocumentExcelDTO(
                        x.SerialNumber,
                        x.DocumentNumber,
                        x.DocumentDate.ToString("yyyy-MM-dd"),
                        x.DeliveryDate.ToString("yyyy-MM-dd"),
                        x.Subject,
                        x.Summary,
                        x.Notes,
                        x.CreatedBy.Name,
                        x.Publisher.Name,
                        x.DeliveryMethods.GetDisplayName(),
                        x.Company.Name,
                        x.RecievedFrom.Name,
                        x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")));

            return result;
        }
    }
}
