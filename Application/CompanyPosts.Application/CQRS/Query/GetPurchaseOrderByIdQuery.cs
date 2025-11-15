namespace CompanyPost.Application.CQRS.Query
{
    public record GetPurchaseOrderByIdQuery(Guid Id) : IRequest<PurchaseOrderByIdResponseDTO>;
}