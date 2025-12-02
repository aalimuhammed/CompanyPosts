namespace CompanyPost.Application.DTO.Response.Base
{
    public record PostsToCopyFromDTO(
        string Notes,
        string Subject,
        string Summary,
        Guid CompanyId,
        Guid PublisherId,
        Guid WorkTypeId,
        Guid RecivedFromId,
        DateTime DocumentDate,
        DateTime DeliveryDate);
}