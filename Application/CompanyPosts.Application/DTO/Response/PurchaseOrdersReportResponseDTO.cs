namespace CompanyPost.Application.DTO.Response
{
    public record PurchaseOrdersReportResponseDTO(
        Guid Id , 
        int SerialNumber,
        string PurchaseOrderNumber,
        string PurchaseOrderType,
        string ProjectName,
        string WorkType,
        string SubContractor,
        string PurchaseOrderValue,
        string Department,
        string Currency,
        string CreatedBy,
        string PurchaseOrderDate,
        List<string> Attachments);
}
