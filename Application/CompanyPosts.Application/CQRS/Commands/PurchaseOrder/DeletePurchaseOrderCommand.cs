namespace CompanyPost.Application.CQRS.Commands.PurchaseOrder
{
    public record DeletePurchaseOrderCommand(Guid Id) : IRequest<bool>;
}