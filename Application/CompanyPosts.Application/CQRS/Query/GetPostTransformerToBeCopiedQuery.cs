using CompanyPost.Application.CQRS.Query.Base;

namespace CompanyPost.Application.CQRS.Query
{
    public record GetPostTransformerToBeCopiedQuery(Guid Id) : GetPostToBeCopiedQueryBase(Id);
}