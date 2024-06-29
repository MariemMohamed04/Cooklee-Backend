using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit;
using Cooklee.Data.Service.Contract;

namespace Cooklee.Core.Helpers
{
    public class EmailSetting : IEmailSetting
    {
        // To use appsetting data
        private MailSetting _options;

        public EmailSetting(IOptions<MailSetting> options)
        {
            _options = options.Value;
        }

        public void SendEmailAsync(Email email)
        {
            try
            {
                // Sender
                var mail = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_options.Email),
                    Subject = email.Subject
                };

                // Send to Who?
                mail.To.Add(MailboxAddress.Parse(email.To));

                // Body
                var builder = new BodyBuilder();
                builder.TextBody = email.Body;
                mail.Body = builder.ToMessageBody();

                // From
                mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

                // Open Connection
                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);

                smtp.Authenticate(_options.Email, _options.Password);

                smtp.Send(mail);

                smtp.Disconnect(true);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Re-throw the exception to be handled by the global exception handler
            }
        }
    }

}
