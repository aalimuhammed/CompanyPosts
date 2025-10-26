namespace CompanyPost.Application.DTO.Request;
public record CreatePostExternalDTO(
int SerialNumber,
string DocumentNumber,
Guid CompanyId,
Guid PublishedId,
Guid RecivedFromId,
Guid WorkTypeId,
string Subject,
DateTime DocumentDate,
DateTime DeliveryDate,
string Summary,
string Notes,
int DeliveryMethod,
string IncomingNumber,
int Department,
List<IFormFile> Attachments);
