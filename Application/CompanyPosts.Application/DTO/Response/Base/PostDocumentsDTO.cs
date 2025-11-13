namespace CompanyPost.Application.DTO.Response.Base
{
	public record PostDocumentsDTO(
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
		string DepartmentName
	);
}
