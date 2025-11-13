using CompanyPost.Application.DTO.Request.Base;
using CompanyPost.Application.DTO.Response.Base;
using CompanyPost.Domain.Result;

namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostInternalDocumentsQuery(BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO) 
		: IRequest<IEnumerable<PostDocumentsDTO>>;
}