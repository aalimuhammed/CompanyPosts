namespace CompanyPost.Application.DTO.Request;
public record CreateIncomingDTO(
	int SerialNumber,
	string DocumentNumber,
	Guid PublishedId,
	Guid OriginalPublisherId,
	Guid ProjectId,
	string Subject,
	DateTime DocumentDate,
	DateTime DeliveryDate,
	DateTime SaveDate,
	string Summary,
	int DeliveryMethod,
	int DocumentType,
	List<IFormFile> Attachments);
