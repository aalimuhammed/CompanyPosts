namespace CompanyPost.Application.DTO.Response
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
        int Department,
        int DeliveryMethod);
}
