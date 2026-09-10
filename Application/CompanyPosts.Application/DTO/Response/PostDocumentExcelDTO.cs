namespace CompanyPost.Application.DTO.Response
{
    public record PostDocumentExcelDTO 
    (
        int SerialNumber,
        string DocumentNumber,
        string DocumentDate,
        string DeliveryDate,
        string? Subject,
        string? Summary,
        string? Notes,
        string CreatedBy,
        string PublishedName,
        string DeliveryMethod,
        string CompanyName,
        string ReceivedFromName,
        string CreatedAt
     );
}
