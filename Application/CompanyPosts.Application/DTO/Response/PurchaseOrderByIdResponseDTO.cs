namespace CompanyPost.Application.DTO.Response
{
    public record PurchaseOrderByIdResponseDTO(
        Guid Id,
        string PurchaseOrderNumber,
        double PurchaseOrderValue,
        Guid WorkTypeId,
        Guid SupplierId,
        Guid ProjectId,
        string PurchaseOrderDate,
        int DepartmentId);
}