using Cooklee.Core.DTOs;
using Cooklee.Data.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly static string _supportEmail= "aminamuhammed34286@gmail.com";

        public ContactUsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] ContactUsDto contactDto)
        {
            // string _body = "phone number: " + contactDto.PhoneNumber  + "\n" +"body :"+ contactDto.Body;
            string _body = $"phone number: {contactDto.PhoneNumber}\nbody: {contactDto.Body}";

            await _emailService.SendEmailAsync(_supportEmail, contactDto.FullName, _body);
            return Ok();
        }
        public class ContactUsDto
        {
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Body { get; set; }
        }
    }
}
