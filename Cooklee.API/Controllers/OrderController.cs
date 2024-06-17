﻿using AutoMapper;
using Cooklee.API.Errors;
using Cooklee.Core.DTOs;
using Cooklee.Data.Entities.Order;
using Cooklee.Data.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cooklee.API.Controllers
{
    [Authorize]
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
            var clientEmailClaim = User.FindFirst(ClaimTypes.Email);
            if (clientEmailClaim == null)
            {
                return Unauthorized(new ApiResponse(401, "User email not found in token"));
            }
            var clientEmail = clientEmailClaim.Value;

            var shippingAddress = _mapper.Map<ShipmentDetailsDto, ShipmentDetails>(orderDto.ShipmentDetails);
            var order = await _orderService.CreateAsync(clientEmail, orderDto.CartId, shippingAddress);
            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Order creation failed"));
            }
            return Ok(order);
        }

        [ProducesResponseType(typeof(IReadOnlyList<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForClient()
        {
            var clientEmailClaim = User.FindFirst(ClaimTypes.Email);
            if (clientEmailClaim == null)
            {
                return Unauthorized(new ApiResponse(401, "User email not found in token"));
            }
            var clientEmail = clientEmailClaim.Value;

            var orders = await _orderService.GetOrdersForClientAsync(clientEmail);
            if (orders == null || orders.Count == 0)
            {
                return NotFound(new ApiResponse(404, "No orders found for the user"));
            }
            return Ok(orders);
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderByIdForClient(int orderId)
        {
            var clientEmailClaim = User.FindFirst(ClaimTypes.Email);
            if (clientEmailClaim == null)
            {
                return Unauthorized(new ApiResponse(401, "User email not found in token"));
            }
            var clientEmail = clientEmailClaim.Value;

            var order = await _orderService.GetOrderByIdForClientAsync(orderId, clientEmail);
            if (order == null)
            {
                return NotFound(new ApiResponse(404, "Order not found"));
            }
            return Ok(order);
        }
    }
}