using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities.Cart;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CartController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCart>> GetCart(string id)
        {
            var cart = await _unit.CartRepo.GetCartAsync(id);
            if (cart == null)
            {
                cart = new ClientCart(id);
                var savedCart = await _unit.CartRepo.UpdateCartAsync(cart);
                return Ok(savedCart);
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<ClientCart>> UpdateCart(ClientCartDto cart)
        {
            var mappedCart = _mapper.Map<ClientCartDto, ClientCart>(cart);
            var createdOrUpdatedCart = await _unit.CartRepo.UpdateCartAsync(mappedCart);
            if (createdOrUpdatedCart == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(createdOrUpdatedCart);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCart(string id)
        {
            await _unit.CartRepo.DeleteCartAsync(id);
        }
    }

}
