namespace CompanyPost.Application.DTO.Request
{
    public record UpdateInComingDocumentRequestDTO(
        string documentNumber,
        string? subject,
        DateTime documentDate,
        DateTime deliveryDate,
        Guid publishedArea,
        Guid? receivedFromId,
        string? notes,
        string? summary,
        int department,
        int deliveryMethod,
        Guid projectId,
        string? originalsender,
        string? oldReferenceNumber,
        string? inComingNumber,
        int documentType,
        int status,
		List<IFormFile>? Attachments,
        List<Guid>? AttachmentIdsToDelete);
}
