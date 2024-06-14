using Cooklee.Core.Meals.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MealController : ControllerBase
	{
		#region fields 
		private readonly IMediator _mediator;
		#endregion
		public MealController(IMediator mediator)
        {
			_mediator = mediator;
        }
		[HttpGet("/meal/List")]
        
        public async Task<IActionResult> GetAllMeals() 
		{
			var response = await _mediator.Send(new GetAllMealQuery());
			return Ok(response);
		}
    }
}
