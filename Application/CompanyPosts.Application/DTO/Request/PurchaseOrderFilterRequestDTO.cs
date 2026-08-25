using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.DTO.Request
{
    public record PurchaseOrderFilterRequestDTO(
    Guid? ProjectId,
    string? DepartmentId,
    Guid? PublisherId,
    DateTime? StartDate,
    DateTime? EndDate,
    Guid? SupplierId,
    string? PurchaseOrderRef) : BaseFilterRequestDTO(ProjectId, DepartmentId, PublisherId, StartDate, EndDate);
}
