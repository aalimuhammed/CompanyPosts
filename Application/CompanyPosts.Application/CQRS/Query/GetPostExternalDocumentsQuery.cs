using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostExternalDocumentsQuery(BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO)
		: IRequest<IEnumerable<PostDocumentsDTO>>
	{
	}
}
