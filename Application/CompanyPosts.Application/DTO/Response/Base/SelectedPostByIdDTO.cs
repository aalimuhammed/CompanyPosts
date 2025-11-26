namespace CompanyPost.Application.DTO.Response.Base
{
    public record SelectedPostByIdDTO(
        string DocumentNumber,
        string Subject, 
        string Summary,
        string Notes,
        Guid CompanyId, 
        Guid PublisherId ,
        Guid DeliveredId, 
        Guid WorkTypeId,
        DateTime DocumentDate,
        DateTime DeliveryDate,
        int DeliveryMethod);
}
