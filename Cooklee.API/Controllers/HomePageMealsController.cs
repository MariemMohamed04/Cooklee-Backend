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
            return Ok(meals);
        }
    }
}
