namespace CompanyPost.Application.CQRS.Query;
public record GetCompaniesQuery : IRequest<IEnumerable<CompanyDto>>;