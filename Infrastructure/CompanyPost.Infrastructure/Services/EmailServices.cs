using CompanyPost.Infrastructure.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace CompanyPost.Infrastructure.Services
{
	internal sealed class EmailServices : IEmailServices
	{
		private readonly ILogger<EmailServices> _logger;
		private readonly EmailSettings _emailSettings;	
		public EmailServices(
			ILogger<EmailServices> logger, 
			IOptions<EmailSettings> emailSettings)
		{
			_logger = logger;
			_emailSettings = emailSettings.Value;
		}
        public async Task SendBulkEmailAsync(
			string subject, 
			string htmlMessage, 
			IEnumerable<string> recipients, 
			CancellationToken cancellationToken = default)
        {
            var sendEmails = recipients.Select(
				recipient => SendEmailAsync(recipient, subject, htmlMessage, cancellationToken));

            await Task.WhenAll(sendEmails);
        }

        public async Task<bool> SendEmailAsync(
			string toEmail, 
			string subject, 
			string body, 
			CancellationToken cancellationToken = default)
		{
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail));
				message.To.Add(new MailboxAddress("", toEmail));
				message.Subject = subject;

				var bodyBuilder = new BodyBuilder
				{
					HtmlBody = body
				};
				message.Body = bodyBuilder.ToMessageBody();

				using (var client = new SmtpClient())
				{
					await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, false , cancellationToken);
					await client.AuthenticateAsync(_emailSettings.Mail, _emailSettings.Password , cancellationToken);
					await client.SendAsync(message , cancellationToken);
					await client.DisconnectAsync(true, cancellationToken);
				}
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("An Error Occured while sending an email", ex);
			}
		}
	}
}
