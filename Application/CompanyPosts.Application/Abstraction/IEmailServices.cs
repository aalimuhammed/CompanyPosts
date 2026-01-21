namespace CompanyPost.Application.Abstraction
{
	public interface IEmailServices
	{
		Task<bool> SendEmailAsync(
            string toEmail, 
            string subject, string body , 
            CancellationToken cancellationToken = default);
        Task SendBulkEmailAsync(
           string subject,
           string htmlMessage,
           IEnumerable<string> recipients,
           CancellationToken cancellationToken = default);
    }
}