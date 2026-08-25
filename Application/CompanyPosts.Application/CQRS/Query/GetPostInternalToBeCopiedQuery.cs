using CompanyPost.Application.CQRS.Query.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostInternalToBeCopiedQuery(Guid Id) : GetPostToBeCopiedQueryBase(Id);
}