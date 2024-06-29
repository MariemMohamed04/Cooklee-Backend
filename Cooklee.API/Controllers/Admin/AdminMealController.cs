using Cooklee.API.Errors;
using Cooklee.Core.Helpers;
using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMealController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailSetting _emailSetting;
        public AdminMealController(IUnitOfWork unit, IEmailSetting emailSetting)
        {
            _unit = unit;
            _emailSetting = emailSetting;
        }

        [HttpPost("AcceptMeal")]
        public async Task<ActionResult<bool>> AcceptMeal([FromQuery] int chefId, [FromQuery] int mealId)
        {
            var client = await _unit.ClientProfileRepo.GetClientBychefAsync(chefId);
            var chef = await _unit.ChefPageRepo.GetAsync(chefId);
            var meal = await _unit.MealRepository.GetAsync(mealId);
            var result = await _unit.MealRepository.AcceptMeal(mealId);
            if (chef == null)
            {
                return Ok(false);
            }

            if (result == true)
            {
                var email = new Email
                {
                    To = chef.Email,
                    Subject = $"{meal.MealName} has bee accepted",
                    Body = $"Dear {client.FirstName},  your meal has been accepted."
                };

                try
                {
                    _emailSetting.SendEmailAsync(email);
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse(400, "Failed to send email."));
                }
            }
            return Ok(false);
        }

        [HttpPost("SendFeedback")]
        public async Task<IActionResult> SendFeedback([FromQuery] int chefId, [FromQuery] int mealId, [FromBody] string body)
        {
            var chef = await _unit.ChefPageRepo.GetAsync(chefId);
            var meal = await _unit.MealRepository.GetAsync(mealId);
            if (chef == null)
            {
                return BadRequest(new ApiResponse(404, "Chef not found."));
            }

            if (meal == null)
            {
                return BadRequest(new ApiResponse(404, "Meal not found."));
            }

            var email = new Email
            {
                To = chef.Email,
                Subject = "Invalid Meal Details",
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
