﻿using Cooklee.API.Errors;
using Cooklee.Data.Entities.Cart;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : BaseApiController
    {
        private readonly IUnitOfWork _unit;

        public CartItemController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpPost]
        public async Task<ActionResult<ClientCart>> AddCartItem(string cartId, CartItem item)
        {
            if(_unit.MealRepository.GetAsync(item.Id) != null)
            {
                var cart = await _unit.CartRepo.AddCartItem(cartId, item);
                if (cart.Items.Any(i => i.Id == item.Id))
                {
                    return BadRequest("Item already exists in Carts.");
                }

                if (cart != null)
                {
                    return Ok(cart);
                }
                return Ok("You can not add more than 10!!");
            }
            else
            {
                return BadRequest("You cannot add a meal that does not exist!!!");
            }
        }

        [HttpPatch("{cartId}")]
        public async Task<ActionResult<ClientCart>> UpdateCartItemQuentity(string cartId, CartItem item)
        {
            var cart = await _unit.CartRepo.UpdateItemQuentity(cartId, item);
            if (cart != null)
            {
                return Ok(cart);
            }
            return BadRequest(new ApiResponse(400, "Failed to update item quantity."));
        }

        [HttpDelete("{cartId}")]
        public async Task<ActionResult<ClientCart>> DeleteCartItem(string cartId, [FromBody] CartItem item)
        {
            var cart = await _unit.CartRepo.DeleteCartItemAsync(cartId, item);
            if (cart != null)
            {
                return Ok(cart);
            }
            return BadRequest(new ApiResponse(400, "Failed to delete item from cart."));
        }
    }


}
