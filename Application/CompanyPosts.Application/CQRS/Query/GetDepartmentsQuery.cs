namespace CompanyPost.Application.CQRS.Query;
public record GetDepartmentsQuery : IRequest<IEnumerable<DepartmentDTO>>;