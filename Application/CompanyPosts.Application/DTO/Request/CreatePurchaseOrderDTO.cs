namespace CompanyPost.Application.DTO.Request
{
    public record CreatePurchaseOrderDTO(
    double Value,
    string? Details,
    string PurchaseOrderNumber,
    DateTime? PurchaseOrderDate,
    int SerialNumber,
    Guid PersonOrgId,
    Guid? WorkTypeId,
    string? Notes,
    Guid ProjectId,
    int? Currency,
    int Department,
    List<IFormFile>? Attachments);
}
