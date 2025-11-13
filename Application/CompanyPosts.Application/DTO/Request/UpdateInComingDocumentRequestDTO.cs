namespace CompanyPost.Application.DTO.Request
{
    public record UpdateInComingDocumentRequestDTO(
        string documentNumber,
        string subject,
        DateTime documentDate,
        DateTime deliveryDate,
        Guid publishedId,
        Guid receivedFromId,
        Guid workTypeId,
        string notes,
        string summary,
        int department,
        int deliveryMethod,
        Guid projectId );
}
