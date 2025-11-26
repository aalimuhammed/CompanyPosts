namespace CompanyPost.Application.DTO.Request;
public record CreateIncomingDTO(
	int SerialNumber,
	string DocumentNumber,
	Guid PublishedId,
	Guid OriginalPublisherId,
	Guid ProjectId,
	Guid WorkTypeId,
	string Subject,
	DateTime DocumentDate,
	DateTime DeliveryDate,
	DateTime SaveDate,
	string Summary,
	int DeliveryMethod,
	int DocumentType,
	int Department,
	List<IFormFile>? Attachments);
