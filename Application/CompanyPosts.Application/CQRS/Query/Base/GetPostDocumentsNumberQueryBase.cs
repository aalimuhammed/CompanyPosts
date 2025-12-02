using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Query.Base
{
    public record GetPostDocumentsNumberQueryBase : IRequest<IEnumerable<PostDocumentNumbersDTO>>;
}