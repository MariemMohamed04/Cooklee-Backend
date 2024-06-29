using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Core.Helpers;
using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminChefController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailSetting _emailSetting;

        public AdminChefController(IUnitOfWork unit, IEmailSetting emailSetting)
        {
            _unit = unit;
            _emailSetting = emailSetting;
        }

        [HttpPost("SendFeedback")]
        public async Task<IActionResult> SendFeedback([FromQuery] int chefId, [FromBody] string body)
        {
            var chef = await _unit.ChefPageRepo.GetAsync(chefId);
            if (chef == null)
            {
                return BadRequest(new ApiResponse(404, "Chef not found."));
            }

            var email = new Email
            {
                To = chef.Email,
                Subject = "Invalid Chef Details",
                Body = body
            };

            try
            {
                _emailSetting.SendEmailAsync(email);
                return Ok(new { Message = "Feedback has been sent to the chef's email." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Failed to send email."));
            }
        }

    }
}
