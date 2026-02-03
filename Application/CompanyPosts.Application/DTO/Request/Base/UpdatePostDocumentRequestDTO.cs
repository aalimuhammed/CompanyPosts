namespace CompanyPost.Application.DTO.Request.Base
{
    public record UpdatePostDocumentRequestDTO(
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
        int deliveryMethod,
	    List<IFormFile>? Attachments);
}
