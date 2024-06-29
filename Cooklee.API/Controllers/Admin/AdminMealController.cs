using AutoMapper;
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
    public class AdminMealController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IEmailSetting _emailSetting;

        public AdminMealController(IUnitOfWork unitOfWork, IMapper mapper, IEmailSetting emailSetting)

        {
            _unit = unitOfWork;
            _mapper = mapper;
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
                var email = new Email()
                {
                    To = chef.Email,
                    Subject = $"{meal.MealName} has been accepted",
                    Body = $"Dear {client.FirstName},  your meal has been accepted."
                };

                try
                {
                    _emailSetting.SendEmailAsync(email);
                    return Ok(true);
                }
                catch (Exception)
                {
                    return Ok(false);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetAllmeals()
        {

            var allMeals = await _unit.MealRepository.GetAllAsync();
            List<MealDto> allMealsDto = new List<MealDto>();

            foreach (var meal in allMeals)
            {
                var mealDto = _mapper.Map<Meal,MealDto>(meal);
                allMealsDto.Add(mealDto);
            }

            return Ok(allMealsDto);
        }





        //get unActive chefs



        [HttpGet("UnAcceptedMeals")]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetUnAcceptedMeals()
        {
            var unAcceptedMeals = await _unit.MealRepository.GetUnAcceptedMeals();
            List<MealDto> unAcceptedMealsDto = new List<MealDto>();

            foreach (var meal in unAcceptedMeals)
            {
                var mealDto = _mapper.Map<Meal, MealDto>(meal);
                unAcceptedMealsDto.Add(mealDto);
            }

            return Ok(unAcceptedMealsDto);
        }


    }
}
