using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.DTO.Request
{
    public record ContractsFilterRequestDTO(
     Guid? ProjectId,
     string? DepartmentId,
     Guid? PublisherId,
     DateTime? StartDate,
     DateTime? EndDate,
     string? ContractRef,
     string? PurchaseOrderRef) : BaseFilterRequestDTO(ProjectId, DepartmentId, PublisherId, StartDate, EndDate);
}
