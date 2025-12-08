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
        int DocumentType , 
        int DeliveryMethod);
}