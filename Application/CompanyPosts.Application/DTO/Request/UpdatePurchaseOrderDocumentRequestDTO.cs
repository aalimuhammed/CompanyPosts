namespace CompanyPost.Application.DTO.Request
{
    public record UpdatePurchaseOrderDocumentRequestDTO(
        string PurchaseOrderNumber,
        double PurchaseOrderValue,
        Guid WorkTypeId,
        Guid SupplierId,
        Guid ProjectId,
        DateTime PurchaseOrderDate,
        int DepartmentId,
		List<IFormFile>? Attachments);
}