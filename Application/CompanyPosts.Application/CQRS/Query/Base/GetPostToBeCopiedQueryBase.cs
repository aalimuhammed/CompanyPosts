using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.CQRS.Query.Base
{
    public record GetPostToBeCopiedQueryBase(Guid Id) : IRequest<PostsToCopyFromDTO> , IHasId;
}