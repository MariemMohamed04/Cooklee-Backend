using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialMealController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public SpecialMealController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("SpecialMeals")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetAllSpecialMeals()
        {
            var specialMeals = await _unit.SpecialMealRepo.GetAllAsync();
            var mappedSpecialMeal = _mapper.Map<IEnumerable<SpecialMeal>, IEnumerable<SpecialMealDto>>(specialMeals);
            return Ok(mappedSpecialMeal);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetSpecialMealById(int specialMealId)
        {
            var specialMeals = await _unit.SpecialMealRepo.GetAsync(specialMealId);
            if (specialMeals is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var mappedSpecialMeal = _mapper.Map<SpecialMeal,SpecialMealDto>(specialMeals);
            return Ok(mappedSpecialMeal);
        }

        [HttpGet("byChefPage/{chefPageId}")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetSpecialMealByChefPageId(int chefPageId)
        {
            var specialMeals = await _unit.SpecialMealRepo.FindAsync(m => m.ChefPageId == chefPageId);
            var mappedSpecialMeals = _mapper.Map<IEnumerable<SpecialMeal>, IEnumerable<SpecialMealDto>>(specialMeals);
            return Ok(mappedSpecialMeals);
        }

        [HttpGet("Chefs")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetAllChefs()
        {
            var chefs = await _unit.ChefPageRepo.GetAllAsync();
            var mappedChefs = _mapper.Map<IEnumerable<ChefPage>, IEnumerable<ChefPageDto>>(chefs);
            return Ok(mappedChefs);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialMealDto>> AddSpecialMeal(SpecialMealDto specialMealDto)
        {
            try
            {
                // Map SpecialMealDto to SpecialMeal entity
                var specialMeal = _mapper.Map<SpecialMealDto, SpecialMeal>(specialMealDto);

                // Check if ClientId exists
                var clientExists = await _unit.ClientProfileRepo.CheckIfExistsAsync(specialMeal.ClientId);
                if (!clientExists)
                {
                    return BadRequest(new ApiResponse(400, "Invalid ClientId"));
                }

                // Check if ChefPageId exists
                var chefPageExists = await _unit.ChefPageRepo.CheckIfExistsAsync(specialMeal.ChefPageId);
                if (!chefPageExists)
                {
                    return BadRequest(new ApiResponse(400, "Invalid ChefPageId"));
                }

                // Add the SpecialMeal entity to repository
                await _unit.SpecialMealRepo.AddAsync(specialMeal);
                await _unit.SpecialMealRepo.SaveChanges();

                // Map the created SpecialMeal back to SpecialMealDto
                var createdSpecialMealDto = _mapper.Map<SpecialMeal, SpecialMealDto>(specialMeal);

                // Return successful response with created SpecialMealDto
                return CreatedAtAction(nameof(GetSpecialMealById), new { id = specialMeal.Id }, createdSpecialMealDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSpecialMeal(int id, SpecialMealDto specialMealDto)
        {
            try
            {
                var existingSpecialMeal = await _unit.SpecialMealRepo.GetAsync(id);
                if (existingSpecialMeal == null)
                {
                    return NotFound(new ApiResponse(404, "SpecialMeal not found"));
                }
                _mapper.Map(specialMealDto, existingSpecialMeal);
                var clientExists = await _unit.ClientProfileRepo.CheckIfExistsAsync(existingSpecialMeal.ClientId);
                var chefPageExists = await _unit.ChefPageRepo.CheckIfExistsAsync(existingSpecialMeal.ChefPageId);
                if (!clientExists || !chefPageExists)
                {
                    return BadRequest(new ApiResponse(400, "Invalid ClientId or ChefPageId"));
                }
                await _unit.SpecialMealRepo.UpdateAsync(id, existingSpecialMeal);
                await _unit.SpecialMealRepo.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpecialMeal(int id)
        {
            try
            {
                var result = await _unit.SpecialMealRepo.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new ApiResponse(404, "SpecialMeal not found"));
                }
                await _unit.SpecialMealRepo.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
            }
        }

    }
}
