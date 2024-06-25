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
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;

namespace Cooklee.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MealController : ControllerBase
	{
		#region fields 
		private readonly IUnitOfWork _unit;
		private readonly IMapper _mapper;
		private readonly IGenericRepo<Meal> _genericMealRepo;// here give me error of <meal> why 
		private readonly IMealRepository _myMealRepo;
		#endregion

		#region constructor
		public MealController(IUnitOfWork unit, IMapper mapper,IGenericRepo<Meal> genericMealRepo,IMealRepository myMealRepo)
        {
			_unit = unit;
			_mapper = mapper;
			_genericMealRepo = genericMealRepo;
			_myMealRepo = myMealRepo;
        }
		#endregion

		#region endPoints

		[HttpGet]
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

		[HttpGet("{id}")]
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


		[HttpPost("mealsearchByName")]
		public async Task<ActionResult<IEnumerable<MealDto>>> GetMealByName([FromBody]string mealName)
		{
			var meals = await _myMealRepo.GetMealByNameAsync(mealName);
			if (meals == null)
			{
				return NotFound();
			}
			var mealDto = _mapper.Map<IEnumerable<MealDto>>(meals);
			return Ok(mealDto);
		}
		

		[HttpGet("chefmeals/{id}")]
		public async Task<ActionResult<IEnumerable<MealDto>>> GetAllChefMeals(int id)
		{
			var chefMeals = await _myMealRepo.GetAllChefMealsAsync(id);
			if(chefMeals.Count() == 0)
			{
				return NotFound();
			}
			var chefMealsDto = _mapper.Map<IEnumerable<MealDto>> (chefMeals);
			return Ok(chefMealsDto);	
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMealAsync(int id)
		{
			bool deleted = await _genericMealRepo.DeleteAsync(id);
			if(!deleted)
			{
				return BadRequest();
			}
			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<AddMealDto>> AddMeal([FromBody]AddMealDto addMeal)
		{
			Meal meal = _mapper.Map<Meal>(addMeal);


			var mealAdded =await _genericMealRepo.AddAsync(meal);
			if (mealAdded==null) 
			{
				BadRequest(addMeal);
			}
			int x = await _genericMealRepo.SaveChanges();
			return Ok(mealAdded);
		}


		[HttpPut("{id}")]
		public async Task<ActionResult<MealDto>> UpdateMeal(int id, [FromBody]AddMealDto mealDto)
		{
			/*if(mealDto.Id !=id)
			{
				return BadRequest();
			}*/
			var meal = await _genericMealRepo.GetAsync(id);
			if (meal==null)
			{
				return NotFound();
			}
			//_mapper.Map(source,destination)
			Meal mappermeal =_mapper.Map<Meal>(mealDto);
			try
			{
				var mealToUpdate = _mapper.Map<Meal>(mealDto);

				// Update the meal in the repository
				var updatedMeal = await _myMealRepo.UpdateMeal(mealToUpdate);
				_genericMealRepo.SaveChanges();

				// Map the updated domain model back to DTO for response
				var updatedMealDto = _mapper.Map<MealDto>(updatedMeal);
				/*mappermeal =await _myMealRepo.UpdateMeal(mappermeal);//here give me exception why 
				//_mapper.Map<destination>(source)
				var updatedMealDto = _mapper.Map<MealDto>(meal);*/

				return Ok(updatedMealDto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while updating the meal.");
			}
		}
		#endregion
	}
}
