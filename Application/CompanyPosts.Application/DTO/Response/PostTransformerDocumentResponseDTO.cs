using CompanyPost.Application.DTO.Response.Base;

namespace CompanyPost.Application.DTO.Response
{
    public record PostTransformerDocumentResponseDTO(
		Guid Id,
		int SerialNumber,
		string DocumentNumber,
		string DocumentDate,
		string DeliveryDate,
		List<string> AttachmentPaths,
		string Subject,
		string Summary,
		string Notes,
		string CreatedByName,
		string PublishedName,
		string DeliveryMethod,
		string CompanyName,
		string WorkTypeName,
		string ReceivedFromName,
		string DepartmentName,
        string InComingNumber ,
        string PostNumber , 
        string FollowingPerson,
        string RecivedByName , 
        int DocumentType) 
        : PostDocumentsDTO(
            Id,
            SerialNumber,
            DocumentNumber,
            DocumentDate,
            DeliveryDate,
            AttachmentPaths,
            Subject,
            Summary,
            Notes,
            CreatedByName,
            PublishedName,
            DeliveryMethod,
            CompanyName,
            WorkTypeName,
            ReceivedFromName);
}