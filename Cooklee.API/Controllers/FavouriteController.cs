using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities.Favourite;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public FavouriteController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientFavourite>> GetFavourite(string id)
        {
            var favourite = await _unit.FavoriteRepository.GetFavouriteAsync(id);
            if (favourite == null)
            {
                favourite = new ClientFavourite(id);
                var savedFavourite = await _unit.FavoriteRepository.UpdateFavouriteAsync(favourite);
                return Ok(savedFavourite);
            }
            return Ok(favourite);
        }

        [HttpPost]
        public async Task<ActionResult<ClientFavourite>> UpdateFavourite(ClientFavouriteDto favourite)
        {
            var mappedFavourite = _mapper.Map<ClientFavouriteDto, ClientFavourite>(favourite);
            var newFavourite = await _unit.FavoriteRepository.UpdateFavouriteAsync(mappedFavourite);
            if (newFavourite == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(newFavourite);
        }

        [HttpDelete]
        public async Task DeleteFavourite(string id)
        {
            await _unit.FavoriteRepository.DeleteFavouriteAsync(id);
        }
    }
}
