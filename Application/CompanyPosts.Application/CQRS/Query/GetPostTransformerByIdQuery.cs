using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Query
{
   public record GetPostTransformerByIdQuery(Guid Id) : IRequest<SelectedPostByIdDTO>;
}