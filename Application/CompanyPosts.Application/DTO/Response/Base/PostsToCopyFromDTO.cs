namespace CompanyPost.Application.DTO.Response.Base
{
    public record PostsToCopyFromDTO(
        string? Notes,
        string? Subject,
        string? Summary,
        Guid CompanyId,
        Guid PublisherId,
        Guid ReceivedFromId,
        Guid WorkTypeId,
        DateTime DocumentDate,
        DateTime DeliveryDate,
        string PublisherType ,
        string ReceivedType,
        int DocumentType , 
        int DeliveryMethod,
        int StatusMethod);
}