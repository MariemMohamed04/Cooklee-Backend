using Cooklee.API.Errors;
using Cooklee.Data.Entities.Favourite;
using Cooklee.Data.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteItemController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public FavouriteItemController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        //[HttpPost]
        //public async Task<ActionResult<ClientFavourite>> AddFavouriteItem(string favouriteId, FavouriteItem item)
        //{
        //    if (_unit.MealRepository.GetAsync(item.Id) != null)
        //    {
        //        var favourite = await _unit.FavoriteRepository.AddFavouriteItem(favouriteId, item);
        //        if (favourite.Items.Any(i => i.Id == item.Id))
        //        {
        //            return BadRequest("Item already exists in favorites.");
        //        }

        //        if (favourite != null)
        //        {
        //            return Ok(favourite);
        //        }
        //        return Ok("You can not add more than 10!!");
        //    }
        //    else
        //    {
        //        return BadRequest("You cannot add a meal that does not exist!!!");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<ClientFavourite>> AddFavouriteItem(string favouriteId, FavouriteItem item)
        {
            if (_unit.MealRepository.GetAsync(item.Id) != null)
            {
                var favourite = await _unit.FavoriteRepository.AddFavouriteItem(favouriteId, item);
                if (favourite != null)
                {
                    return Ok(favourite);
                }
                return Ok("You cannot add the same meal more than once!!");
            }
            else
            {
                return BadRequest("You cannot add a meal that does not exist!!!");
            }
        }

        [HttpDelete("{favouriteId}")]
        public async Task<ActionResult<ClientFavourite>> DeleteFavouriteItem(string favouriteId, [FromBody] FavouriteItem item)
        {
            var favourite = await _unit.FavoriteRepository.DeleteFavouriteItemAsync(favouriteId, item);
            if (favourite != null)
            {
                return Ok(favourite);
            }
            return BadRequest(new ApiResponse(400));
        }
    }
}
