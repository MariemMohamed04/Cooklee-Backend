using Cooklee.Core.DTOs;
using Cooklee.Data.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Cooklee.Service.Services.HomePageMealsService;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageMealsController : ControllerBase
    {
        private readonly IHomePageMealsService _mealService;

        public HomePageMealsController(IHomePageMealsService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var meals = await _mealService.GetAllMealsAsync();


            var mealsResult = meals.Select(m => new MealDto
            {
                Id = m.Id,
                IsHealthy = m.IsHealthy,
                MealDescription = m.MealDescription,
                MealName = m.MealName,
                Price = m.Price,
            }).ToList();
           return Ok(mealsResult);
        }
    }
}
