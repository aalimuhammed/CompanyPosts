namespace CompanyPost.Application.CQRS.Query
{
	public record GetInComingDocumentsQuery : IRequest<IEnumerable<PostDocumentsDTO>>;
}