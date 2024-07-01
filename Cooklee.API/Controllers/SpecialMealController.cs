using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public SpecialMealController(IUnitOfWork unit, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("SpecialMeals")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetAllSpecialMeals()
        {
            var specialMeals = await _unit.SpecialMealRepo.GetAllAsync();
            var mappedSpecialMeal = _mapper.Map<IEnumerable<SpecialMeal>, IEnumerable<SpecialMealDto>>(specialMeals);
            return Ok(mappedSpecialMeal);
        }

        [HttpGet("SpecialMealsByClient")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetAllSpecialMealsById( string userId)
        {
            var client = await _unit.ClientProfileRepo.GetProfileAsync(userId);
            if (client == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid ClientId"));
            }
            var specialMeals = await _unit.SpecialMealRepo.getAllByClient(client.Id);

            var mappedSpecialMeal = _mapper.Map<IEnumerable<SpecialMeal>, IEnumerable<SpecialMealDto>>(specialMeals);
            return Ok(mappedSpecialMeal);
        }


        [HttpGet]
        public async Task<ActionResult<SpecialMealDto>> GetSpecialMealById( int specialMealId)
        {
            var specialMeal = await _unit.SpecialMealRepo.GetAsync(specialMealId);
            if (specialMeal is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var mappedSpecialMeal = _mapper.Map<SpecialMeal,SpecialMealDto>(specialMeal);
            return Ok(mappedSpecialMeal);
        }




        [HttpGet("byChefPage/{chefPageId}")]
        public async Task<ActionResult<IEnumerable<SpecialMealDto>>> GetSpecialMealByChefPageId(int chefPageId)
        {
            var specialMeals = await _unit.SpecialMealRepo.FindAsync(m => m.ChefId == chefPageId);
            var mappedSpecialMeals = _mapper.Map<IEnumerable<SpecialMeal>, IEnumerable<SpecialMealDto>>(specialMeals);
            return Ok(mappedSpecialMeals);
        }

        [HttpGet("Chefs")]
        public async Task<ActionResult<IEnumerable<ChefPageDto>>> GetAllChefs()
        {
            var chefs = await _unit.ChefPageRepo.GetAllAsync();
            var mappedChefs = _mapper.Map<IEnumerable<ChefPage>, IEnumerable<ChefPageDto>>(chefs);
            return Ok(mappedChefs);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialMealDto>> AddSpecialMeal(SpecialMealDto specialMealDto )
        {
            try
            {
                // Map SpecialMealDto to SpecialMeal entity
                var specialMeal = _mapper.Map<SpecialMealDto, SpecialMeal>(specialMealDto);

                // Check if ClientId exists

                var client =   await _unit.ClientProfileRepo.GetProfileAsync(specialMealDto.UserId);
                if (client==null)
                {
                    return BadRequest(new ApiResponse(400, "Invalid ClientId"));
                }

                specialMeal.ClientId = client.Id;


                // Check if ChefPageId exists
                //var chefPageExists = await _unit.ChefPageRepo.CheckIfExistsAsync(specialMeal.ChefId);
                //if (!chefPageExists)
                //{
                //    return BadRequest(new ApiResponse(400, "Invalid ChefPageId"));
                //}

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


        [HttpPut]
        public async Task<ActionResult> UpdateSpecialMeal( SpecialMealDto specialMealDto)
        {
            try
            {
                var existingSpecialMeal = await _unit.SpecialMealRepo.GetAsync(specialMealDto.id);
                if (existingSpecialMeal == null)
                {
                    return NotFound(new ApiResponse(404, "SpecialMeal not found"));
                }
                var client = await _unit.ClientProfileRepo.GetProfileAsync(specialMealDto.UserId);
                if (client == null)
                {
                    return BadRequest(new ApiResponse(400, "Invalid ClientId"));
                }
                existingSpecialMeal.ClientId = client.Id;
                _mapper.Map(specialMealDto, existingSpecialMeal);
           
                await _unit.SpecialMealRepo.UpdateAsync(specialMealDto.id, existingSpecialMeal);
                await _unit.SpecialMealRepo.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteSpecialMeal(int id)
        //{
        //    try
        //    {
        //        var result = await _unit.SpecialMealRepo.DeleteAsync(id);
        //        if (!result)
        //        {
        //            return NotFound(new ApiResponse(404, "SpecialMeal not found"));
        //        }
        //        await _unit.SpecialMealRepo.SaveChanges();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        //    }
        //}

        ////chat
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteSpecialMeal(int id)
        //{
        //    var existingSpecialMeal = await _unit.SpecialMealRepo.GetAsync(id);
        //    if (existingSpecialMeal == null)
        //    {
        //        return NotFound(new ApiResponse(404, "SpecialMeal not found"));
        //    }

        //    await _unit.SpecialMealRepo.DeleteAsync(id);
        //    await _unit.SpecialMealRepo.SaveChanges();
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpecialMeal(int id)
        {
            var existingSpecialMeal = await _unit.SpecialMealRepo.GetAsync(id);
            if (existingSpecialMeal == null)
            {
                return NotFound(new ApiResponse(404, "SpecialMeal not found"));
            }

            await _unit.SpecialMealRepo.DeleteAsync(id);
            await _unit.SpecialMealRepo.SaveChanges();
            return NoContent();
        }


    }
}
