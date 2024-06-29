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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSetting _emailSetting;

        public AdminMealController(IUnitOfWork unitOfWork, IMapper mapper, IEmailSetting emailSetting)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSetting = emailSetting;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetAllmeals()
        {

            var allMeals = await _unitOfWork.MealRepository.GetAllAsync();
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
            var unAcceptedMeals = await _unitOfWork.MealRepository.GetUnAcceptedMeals();
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
