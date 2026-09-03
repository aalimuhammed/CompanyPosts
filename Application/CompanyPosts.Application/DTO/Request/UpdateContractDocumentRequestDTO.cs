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
        DateTime ApprovalDeliveryDate,
        DateTime DateOfReceipt,
        List<IFormFile>? Attachments ,
        List<Guid>? AttachmentIdsToDelete);
}