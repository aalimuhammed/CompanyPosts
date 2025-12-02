namespace CompanyPost.Application.CQRS.Query
{
    public record GetPublisherCompaniesQuery 
         : IRequest<IEnumerable<CompanyDto>>;
}