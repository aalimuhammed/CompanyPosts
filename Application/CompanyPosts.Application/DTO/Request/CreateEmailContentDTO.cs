namespace CompanyPost.Application.DTO.Request
{
    public class CreateEmailContentDTO
    {
        public string Subject { get; set; } = null!;
        public string EmailContent { get; set; } = null!;
        public string DocumentNumber { get; set; } = null!;
        public string EmailHeader { get; set; } = null!;
         
    }
}
