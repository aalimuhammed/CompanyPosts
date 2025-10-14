namespace CompanyPost.Application.CQRS.Query;
public record GetWorkTypesQuery : IRequest<IEnumerable<WorkTypesResponseDTO>>;