namespace CompanyPost.Application.CQRS.Query
{
    public record GetInComingToBeCopiedQuery(Guid Id) : IRequest<InComingCopiedFromDTO>;
}