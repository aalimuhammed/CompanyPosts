namespace CompanyPost.Application.CQRS.Commands.PurchaseOrder
{
    public record UpdatePurchaseOrderDocumentCommand(
        Guid Id,
        UpdatePurchaseOrderDocumentRequestDTO UpdateRequestDTO) : IRequest<bool>;
}