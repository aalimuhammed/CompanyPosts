using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Query
{
   public record GetPostExternalByIdQuery(Guid Id) : IRequest<SelectedPostByIdDTO>;
}