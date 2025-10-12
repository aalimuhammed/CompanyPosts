namespace CompanyPost.Application.CQRS.Query;
public record GetSuppliersQuery : IRequest<IEnumerable<SupplierDTO>>;