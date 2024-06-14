
using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Cooklee.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialMealController : ControllerBase
    {
        private readonly CookleeDbContext _db;
        private readonly IUnitOfWork unit;
        
        private readonly IMapper mapper;
        public SpecialMealController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, CookleeDbContext db)
        {
            _db = db;
            unitOfWork = unit;
             mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialMealDto>> GetMeal(int id)
        {
            
            var meal = await unit.SpecialMealRepo.GetAsync(id);

            if (meal == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(mapper.Map<SpecialMeal,SpecialMealDto>(meal));
        }


        [HttpPost]
        public async Task<ActionResult<SpecialMealDto>> AddAsync(SpecialMeal meal, int Id)
        {
           
                var add = await unit.SpecialMealRepo.AddAsync(meal);

            var spemeal = mapper.Map<SpecialMeal, SpecialMealDto>(add);
                return Ok();
            }
        


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync (int id, SpecialMeal specmeal)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid product ID", StatusCode = "400" });
            

            var updatemeal = await unit.SpecialMealRepo.UpdateAsync(id, specmeal);

           

            return Ok(updatemeal);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
           
            var specmeal = await unit.SpecialMealRepo.GetAsync(id);
            if (specmeal == null)
            {
                return NotFound();
            }

            await unit.SpecialMealRepo.DeleteAsync(id);

            return NoContent();
        }

       



        [HttpGet("getAll")]
        public async Task<ActionResult<IReadOnlyList<SpecialMealDto>>> GetAllSpecialMeal()
        {
            var specmeal = await unit.SpecialMealRepo.GetAllAsync();

            var cmeal = mapper.Map<List<SpecialMeal>, List<SpecialMealDto>>(specmeal.ToList());

            return Ok(specmeal); 
        }

       
    }

}
