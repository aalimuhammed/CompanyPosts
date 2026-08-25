namespace CompanyPost.Application.DTO.Request.Base
{
    public record BaseDocumentFilterRequestDTO(
        DateTime? StartDate ,
        DateTime? EndDate , 
        string? DocumentNumber ,
        string? InComingNumber ,
        string? ProjectId);
}