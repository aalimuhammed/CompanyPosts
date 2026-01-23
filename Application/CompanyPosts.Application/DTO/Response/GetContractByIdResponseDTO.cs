namespace CompanyPost.Application.DTO.Response
{
    public record GetContractByIdResponseDTO(
        Guid Id,
        string ContractNumber,
        double ContractValue,
        string ContractDate,
        string? Details ,
        string? notes , 
        string PurchaseOrderRef,
        int Currency,
        Guid SupplierId,
        Guid ProjectId,
        Guid WorkTypeId,
        string? OldReferenceNumber,
        int Department);
}