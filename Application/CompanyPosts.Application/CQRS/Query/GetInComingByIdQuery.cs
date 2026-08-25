namespace CompanyPost.Application.CQRS.Query
{
    public record GetInComingByIdQuery(Guid Id) : IRequest<SelectedInComingByIdDTO>;
}