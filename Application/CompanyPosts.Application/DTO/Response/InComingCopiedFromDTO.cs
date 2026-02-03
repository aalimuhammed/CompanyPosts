namespace CompanyPost.Application.DTO.Response
{
    public record InComingCopiedFromDTO(
        Guid Id ,
        Guid ProjectId,
        Guid PublishedId ,
        Guid WorkTypeId,
        int PostDocumentTypes , 
        string? Subject ,
        string? Notes , 
        int DeliveryMethods , 
        int Status);
}
