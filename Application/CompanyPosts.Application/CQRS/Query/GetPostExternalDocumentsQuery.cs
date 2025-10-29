namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostExternalDocumentsQuery 
		: IRequest<IEnumerable<PostDocumentsDTO>>
	{
	}
}
