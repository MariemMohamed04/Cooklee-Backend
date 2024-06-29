using Cooklee.Data.Service.Contract;
using Cooklee.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsWithTopRatedMealsController : ControllerBase
    {
        private readonly IChefsWithTopRatedMealsService _chefsService;

        public ChefsWithTopRatedMealsController(IChefsWithTopRatedMealsService chefsService)
        {
            _chefsService = chefsService;
        }

        [HttpGet("top-rated-meals")]
        public async Task<IActionResult> GetAllChefsWithTopRatedMeals()
        {
            var result = await _chefsService.GetAllChefsWithTopRatedMealsAsync();
            return Ok(result);
        }
    }
}
