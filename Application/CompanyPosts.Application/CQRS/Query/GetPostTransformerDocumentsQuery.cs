namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostTransformerDocumentsQuery : IRequest<IEnumerable<PostDocumentsDTO>>;
}
