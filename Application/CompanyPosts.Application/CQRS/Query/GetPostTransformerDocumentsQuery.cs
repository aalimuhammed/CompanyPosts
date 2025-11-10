using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.CQRS.Query
{
	public record GetPostTransformerDocumentsQuery(BaseDocumentFilterRequestDTO BaseDocumentFilterRequestDTO) 
		: IRequest<IEnumerable<PostDocumentsDTO>>;
}
