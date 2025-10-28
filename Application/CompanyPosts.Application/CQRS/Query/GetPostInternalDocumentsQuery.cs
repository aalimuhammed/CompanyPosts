using CompanyPost.Domain.Result;

namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostInternalDocumentsQuery : IRequest<PaginatedResult<PostDocumentsDTO>>;
}