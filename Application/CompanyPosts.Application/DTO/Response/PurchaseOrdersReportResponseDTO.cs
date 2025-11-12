namespace CompanyPost.Application.DTO.Response
{
    public record PurchaseOrdersReportResponseDTO(
        Guid Id , 
        int SerialNumber,
        string PurchaseOrderNumber,
        string ProjectName,
        string WorkType,
        string SubContractor,
        string Department,
        string Currency,
        string CreatedBy,
        string PurchaseOrderDate,
        string CreatedAt,
        string Value,
        List<string> Attachments);
}
