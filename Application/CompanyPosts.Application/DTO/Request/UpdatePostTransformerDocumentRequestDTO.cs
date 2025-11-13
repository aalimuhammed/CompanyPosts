using CompanyPost.Application.DTO.Request.Base;

namespace CompanyPost.Application.DTO.Request
{
    public record UpdatePostTransformerDocumentRequestDTO(
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
        int department,
        int deliveryMethod,
        string recivedByName ,
        string postNumber,
        string inComingNumber,
        int documentType)
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
            deliveryMethod);
}