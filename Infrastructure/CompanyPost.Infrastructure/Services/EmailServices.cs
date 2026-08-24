using CompanyPost.Application.DTO.Request;
using CompanyPost.Infrastructure.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Net;

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
				var recipientsList = recipients.ToList();
				_logger.LogInformation("Sending bulk email to {Count} recipients", recipientsList.Count);

				var sendEmails = recipients.Select(
					recipient => SendEmailAsync(recipient, subject, htmlMessage, cancellationToken));

				await Task.WhenAll(sendEmails);
				_logger.LogInformation("Email sent successfully to {Recipients}", recipients);
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

        public string CreateEmailContent(CreateEmailContentDTO EmailContentDTO )
        {
            var dto = EmailContentDTO;

            string keyword = "الموضوع:";

            int index = dto.EmailContent.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                dto.EmailContent = dto.EmailContent.Substring(0, index).Trim();
            }

            var content = dto.EmailContent ?? "";
           
            content = WebUtility.HtmlEncode(content)
                    .Replace("\r\n", "<br>")
                    .Replace("\n", "<br>");

            var subject = WebUtility.HtmlEncode(dto.Subject ?? "");

            dto.EmailContent = $@"
                                    <!DOCTYPE html>
                                    <html lang=""ar"" dir=""rtl"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                        <title>{subject}</title>
                                    </head>

                                    <body style=""
                                        margin:0;
                                        padding:0;
                                        background-color:#f4f6f8;
                                        font-family:Tahoma, Arial, sans-serif;
                                        color:#333333;
                                        direction:rtl;
                                    "">

                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""
                                           style=""background-color:#f4f6f8; padding:30px 15px;"">

                                        <tr>
                                            <td align=""center"">

                                                <!-- Main Container -->
                                                <table width=""700"" cellpadding=""0"" cellspacing=""0"" border=""0""
                                                       style=""
                                                           max-width:700px;
                                                           width:100%;
                                                           background-color:#ffffff;
                                                           border-radius:10px;
                                                           overflow:hidden;
                                                           box-shadow:0 2px 8px rgba(0,0,0,0.08);
                                                       "">

                                                    <!-- Header -->
                                                    <tr>
                                                        <td style=""
                                                            background-color:#17365D;
                                                            padding:22px 30px;
                                                            text-align:right;
                                                        "">

                                                            <div style=""
                                                                color:#ffffff;
                                                                font-size:20px;
                                                                font-weight:bold;
                                                            "">
                                                                {dto.EmailHeader}
                                                            </div>

                                                        </td>
                                                    </tr>

                                                    <!-- Subject -->
                                                    <tr>
                                                        <td style=""padding:28px 30px 15px 30px;"">

                                                            <div style=""
                                                                font-size:13px;
                                                                color:#777777;
                                                                margin-bottom:8px;
                                                            "">
                                                                الموضوع
                                                            </div>

                                                            <div style=""
                                                                font-size:21px;
                                                                line-height:1.5;
                                                                font-weight:bold;
                                                                color:#17365D;
                                                            "">
                                                                {subject}
                                                            </div>

                                                        </td>
                                                    </tr>

                                                    <!-- Separator -->
                                                    <tr>
                                                        <td style=""padding:0 30px;"">
                                                            <div style=""
                                                                height:1px;
                                                                background-color:#e5e7eb;
                                                                font-size:0;
                                                                line-height:0;
                                                            "">&nbsp;</div>
                                                        </td>
                                                    </tr>

                                                    <!-- Content -->
                                                    <tr>
                                                        <td style=""
                                                            padding:25px 30px 30px 30px;
                                                            font-size:15px;
                                                            line-height:2;
                                                            color:#333333;
                                                            text-align:right;
                                                            direction:rtl;
                                                        "">

                                                            {content}

                                                        </td>
                                                    </tr>

                                                    <!-- Footer -->
                                                    <tr>
                                                        <td style=""
                                                            background-color:#f8fafc;
                                                            border-top:1px solid #e5e7eb;
                                                            padding:18px 30px;
                                                            text-align:center;
                                                        "">

                                                            <div style=""
                                                                color:#6b7280;
                                                                font-size:12px;
                                                                line-height:1.7;
                                                            "">
                                                                تم إرسال هذه الرسالة تلقائياً من نظام Company Post
                                                            </div>

                                                        </td>
                                                    </tr>

                                                </table>

                                            </td>
                                        </tr>

                                    </table>

                                    </body>
                                    </html>";

            return dto.EmailContent;
        }
    }
}