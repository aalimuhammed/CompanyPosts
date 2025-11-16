namespace CompanyPost.Application.DTO.Response
{
    public record ContractAttachedDataResponseDTO(
        Guid Id,
        string ProjectName,
        string DepartmentName,
        string PurchaseOrderRefNumber);
}