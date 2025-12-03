using CompanyPost.Application.CQRS.Query.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostExternalToBeCopiedQuery(Guid Id) : GetPostToBeCopiedQueryBase(Id);
}