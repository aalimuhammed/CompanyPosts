using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Query
{
	public record GetInComingDocumentsQuery(BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO) 
		: IRequest<IEnumerable<PostDocumentsDTO>>;
}