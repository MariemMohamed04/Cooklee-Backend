using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Management;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("cooklee.meals@gmail.com"));
            email.To.Add(MailboxAddress.Parse("brie.beau29@gmail.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("cooklee.meals@gmail.com", "tyeiyxbsklxphxzl");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
