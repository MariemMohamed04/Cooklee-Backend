using AutoMapper;
using Cooklee.Core.Meals.Queries.Models;
using Cooklee.Data.Entities;
using Cooklee.Core.DTOs;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Cooklee.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MealController : ControllerBase
	{
		#region fields 
		/*private readonly IMediator _mediator;*/
		private readonly IUnitOfWork _unit;
		private readonly IMapper _mapper;
		private readonly IGenericRepo<Meal> _genericMealRepo;// here give me error of <meal> why 
		private readonly IMealRepository _myMealRepo;
		#endregion
		public MealController(IUnitOfWork unit, IMapper mapper,IGenericRepo<Meal> genericMealRepo,IMealRepository myMealRepo)
        {
			_unit = unit;
			_mapper = mapper;
			_genericMealRepo = genericMealRepo;
			_myMealRepo = myMealRepo;
        }
		[HttpGet("/meals/list")]
		public async Task<ActionResult<IEnumerable<MealDto>>> GetMealsOrderedByRate()
		{
			var mealsList = await _myMealRepo.GetMealsOrderedByRateAsync();
			if(mealsList.Count() == 0)
			{
				return NotFound();
			}
			var mealsDto = _mapper.Map<IEnumerable<MealDto>>(mealsList); 
			return Ok(mealsDto); 
		}

		[HttpGet("/meal/{id}")]
		public async Task<ActionResult<MealDto>> GetMealById(int id)
		{
			var meal =await _genericMealRepo.GetAsync(id);
			if(meal == null)
			{
				return NotFound();
			}
			var mealDto = _mapper.Map<MealDto>(meal);
			return Ok(mealDto);
		}

		[HttpGet("/chefmeal/{id}")]
		public async Task<ActionResult<IEnumerable<MealDto>>> GetAllChefMeals(int id)
		{
			var chefMeals = await _myMealRepo.GetAllChefMealsAsync(id);
			if(chefMeals.Count() == 0)
			{
				return NotFound();
			}
			var chefMealsDto = _mapper.Map<MealDto>(chefMeals);
			return Ok(chefMealsDto);	
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteMealAsync(int id)
		{
			bool deleted = await _genericMealRepo.DeleteAsync(id);
			if(!deleted)
			{
				return BadRequest();
			}
			return Ok();
		}
	
	}
}
