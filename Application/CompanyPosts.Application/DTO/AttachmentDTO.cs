namespace CompanyPost.Application.DTO
{
    public record AttachmentDTO(
        Guid Id, 
        string FileName, 
        string Url);
}