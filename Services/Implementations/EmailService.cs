using System;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace UserTestProject.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmail(string recipient, string body, string subject, string bodyHtml )
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Admin", _config["Email:Sender"]);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(recipient, recipient);
                message.To.Add(to);

                message.Subject = subject;

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = bodyHtml;
                bodyBuilder.TextBody = body;

                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.Connect(_config["Email:Host"], Convert.ToInt32(_config["Email:Port"]));


                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_config["Email:Sender"], _config["Email:Password"]);

                    client.Send(message);
                    client.Disconnect(true);
                }
                
            }
            catch(Exception ex)
            {
                var x = ex.Message;
            }

            

            return true;
        }
    }
}
