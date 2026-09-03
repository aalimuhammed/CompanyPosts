namespace CompanyPost.Application.DTO.Response.Base
{
	public record PostDocumentsDTO(
		Guid Id,
		int SerialNumber,
		string DocumentNumber,
		string DocumentDate,
		string DeliveryDate,
		List<string>? AttachmentPaths,
		string? Subject,
		string? Summary,
		string? Notes,
		string CreatedBy,
		string PublishedName,
		string DeliveryMethod,
		string CompanyName,
		string ReceivedFromName,
		string CreatedAt,
		bool canEdit);
}
