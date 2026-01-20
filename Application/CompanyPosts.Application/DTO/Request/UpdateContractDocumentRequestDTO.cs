namespace CompanyPost.Application.DTO.Request
{
    public record UpdateContractDocumentRequestDTO
        (double ContractValue,
        string? Details,
        string ContractNumber,
        DateTime ContractDate,
        Guid SupplierId,
        string? Notes,
        Guid ProjectId,
        int Currency,
        string? PurchaseOrderRef,
        int Department);
}