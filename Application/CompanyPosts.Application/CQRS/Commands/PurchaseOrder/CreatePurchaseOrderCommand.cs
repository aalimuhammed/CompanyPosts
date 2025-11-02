namespace CompanyPost.Application.CQRS.Commands.PurchaseOrder
{
    public record CreatePurchaseOrderCommand(CreatePurchaseOrderDTO CreatePurchaseOrderDTO) 
        : IRequest<Unit>;
}