namespace CompanyPost.Application.DTO.Response.Base
{
    public record SelectedPostByIdDTO(
        string DocumentNumber,
        string Subject, 
        string Summary,
        string Notes,
        Guid CompanyId, 
        Guid PublisherId ,
        Guid RecievedFromId, 
        Guid WorkTypeId,
		DateTime DocumentDate,
        DateTime DeliveryDate,
        int DeliveryMethod,
        int PostDocumentType);
}
