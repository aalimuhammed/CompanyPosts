using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.DTO.Request
{
    public record UpdatePostExternalDocumentRequestDTO(
        string documentNumber,
        string? subject,
        DateTime documentDate,
        DateTime deliveryDate,
        Guid companyId ,
        Guid publishedId,
        Guid receivedFromId,
        Guid workTypeId,
        string? notes,
        string? summary,
        string? oldReferenceNumber,
        string? inComingNumber,
        int? department,
        int deliveryMethod , 
        int status,
        List<IFormFile>? Attachments ,
        List<Guid>? AttachmentIdsToDelete) 
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
            status,
            Attachments,
            AttachmentIdsToDelete);
}