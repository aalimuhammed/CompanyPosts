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
        Guid WorkTypeId,
        string? OldReferenceNumber,
        int Department,
        List<IFormFile>? Attachments);
}