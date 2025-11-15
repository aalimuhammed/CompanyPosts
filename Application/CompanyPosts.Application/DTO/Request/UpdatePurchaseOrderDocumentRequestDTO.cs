namespace CompanyPost.Application.DTO.Request
{
    public record UpdatePurchaseOrderDocumentRequestDTO(
        string PurchaseOrderNumber,
        string PurchaseOrderValue,
        Guid WorkTypeId,
        Guid SupplierId,
        Guid ProjectId,
        DateTime PurchaseOrderDate,
        int DepartmentId);
}