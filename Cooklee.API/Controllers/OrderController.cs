using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities.Order;
using Cooklee.Data.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            var shippingAddress = _mapper.Map<OrderAddressDto, OrderAddress>(orderDto.ShippingAddress);
            var order = await _orderService.CreateAsync(orderDto.ClientEmail, orderDto.CartId, shippingAddress);
            if (order == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForClient(string email)
        {
            var orders = await _orderService.GetOrdersForUserAsync(email);
            if (orders == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByIdForClient(int orderId, string email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(orderId, email);
            if (order == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(order);
        }
    }
}
