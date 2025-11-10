namespace CompanyPost.Application.DTO.Request;
public record CreatePostInternalDTO(
	int SerialNumber,
	string DocumentNumber,
	Guid CompanyId ,
	Guid PublishedId,
	Guid RecivedFromId,
	Guid WorkTypeId,
	string Subject,
	DateTime DocumentDate,
	DateTime DeliveryDate ,
	string Summary,
	string? Notes,
	int DeliveryMethod,
	int Department,
	List<IFormFile> Attachments);