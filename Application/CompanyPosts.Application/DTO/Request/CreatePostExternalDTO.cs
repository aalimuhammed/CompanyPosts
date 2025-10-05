namespace CompanyPost.Application.DTO.Request;
public record CreatePostExternalDTO(
int SerialNumber,
string DocumentNumber,
Guid CompanyId,
Guid PublishedId,
Guid RecivedFromId,
string Subject,
string Working,
DateTime DocumentDate,
DateTime DeliveryDate,
string Summary,
string Notes,
int DeliveryMethod,
string IncomingNumber,
List<IFormFile> Attachments);
