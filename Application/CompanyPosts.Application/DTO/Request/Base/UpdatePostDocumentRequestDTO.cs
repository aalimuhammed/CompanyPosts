namespace CompanyPost.Application.DTO.Request.Base
{
    public record UpdatePostDocumentRequestDTO(
        string documentNumber,
        string? subject,
        DateTime documentDate,
        DateTime deliveryDate,
        Guid companyId,
        Guid publishedId,
        Guid receivedFromId,
        string? notes,
        string? summary,
        int? department,
        int deliveryMethod,
        int status,
	    List<IFormFile>? Attachments ,
        List<Guid>? AttachmentIdsToDelete);
}
