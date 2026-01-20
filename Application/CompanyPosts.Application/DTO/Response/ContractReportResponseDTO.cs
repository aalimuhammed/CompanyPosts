namespace CompanyPost.Application.DTO.Response
{
    public record ContractReportResponseDTO(
        Guid Id,
        string Project , 
        string ContractNum ,
        string SerialNum , 
        string WorkType ,
        string ContractDate ,
        string Department,
        string PurchaseOrderRef,
        string Currency,
        string PersonOrg,
        string Type,
        string CreatedBy,
        string CreatedAt,
        double Value,
        List<string> AttachmentPaths);
}
