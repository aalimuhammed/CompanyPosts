namespace CompanyPost.Application.DTO.Response
{
    public record ContractReportResponseDTO(
        Guid Id,
        string? Project , 
        string ContractNum ,
        string SerialNum , 
        string? WorkType ,
        string ContractDate ,
        string? Department,
        string? PurchaseOrderRef,
        string Currency,
        string? PersonOrg,
        string?  Type,
        string CreatedBy,
        string CreatedAt,
        double Value,
        DateTime? ApprovalDeliveryDate,
        DateTime? DateOfReceipt,
        List<string> AttachmentPaths,
        List<ContractRefResponseDTO> References);

    public record ContractRefResponseDTO(
                Guid Id,
                string ContractNumber,
                string DisplayNumber,
                string ContractDate,
                double Value,
                string CreatedBy,
                string Currency,
                List<string> Attachments
 );
}
