using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.DTO.Request
{
    public record UpdatePostInternalDocumentRequestDTO(
        string documentNumber,
        string subject,
        DateTime documentDate,
        DateTime deliveryDate,
        Guid companyId,
        Guid publishedId,
        Guid receivedFromId,
        Guid workTypeId,
        string notes,
        string summary,
        int? department,
        int deliveryMethod ,
		List<IFormFile>? Attachments)
        : UpdatePostDocumentRequestDTO(
            documentNumber,
            subject,
            documentDate,
            deliveryDate,
            companyId,
            publishedId,
            receivedFromId,
            workTypeId,
            notes,
            summary,
            department,
            deliveryMethod ,
            Attachments);
}