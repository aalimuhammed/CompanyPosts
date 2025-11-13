using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostInternalByIdQuery(Guid Id) : IRequest<SelectedPostByIdDTO>;
}