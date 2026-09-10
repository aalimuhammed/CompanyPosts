namespace CompanyPost.Application.DTO.Response
{
    public record InComingPostDocumentExcelDTO
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
        string CreatedAt
        );
}
