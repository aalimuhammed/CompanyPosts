using CompanyPost.Application.DTO.Request.Base;
using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostExternalDocumentsQuery(BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO)
		: IRequest<IEnumerable<PostDocumentsDTO>>
	{
	}
}
