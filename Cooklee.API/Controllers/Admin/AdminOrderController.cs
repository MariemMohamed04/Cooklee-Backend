using AutoMapper;
using Cooklee.Core.DTOs;
using Cooklee.Core.Helpers;
using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Order;
using Cooklee.Data.Repository.Contract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminOrderController : BaseApiController
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IEmailSetting _emailSetting;

        public AdminOrderController(IUnitOfWork unitOfWork, IMapper mapper, IEmailSetting emailSetting)
        {
            _unit = unitOfWork;
           _mapper = mapper;
          _emailSetting = emailSetting;
        }


        //get all orders
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {

            var allOrders = await _unit.OrderRepository.GetAllAsync();
            List<OrderDto> allOrdersDto = new List<OrderDto>();

            //foreach (var order in allOrders)
            //{
           
            //    var orderDto = _mapper.Map<Order, OrderDto>(order);

            //    allOrdersDto.Add(orderDto);
            //}

            return Ok(allOrders);
        }

        //get all orders status != recived


        [HttpGet("UndeliverdOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUndeliverdOrders()
        {
            var undeliverdOrders = await _unit.OrderRepository.GetUndeliverdOrders();
            //List<OrderDto> undeliverdOrdersDto = new List<OrderDto>();

            //foreach (var order in undeliverdOrders)
            //{
            //    var orderDto = _mapper.Map<Order, OrderDto>(order);
            //    undeliverdOrdersDto.Add(orderDto);
            //}

            return Ok(undeliverdOrders);
        }



        [HttpGet("DeliverdOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetDeliverdOrders()
        {
            var deliverdOrders = await _unit.OrderRepository.GetDeliverdOrders();
         

            return Ok(deliverdOrders);
        }


        //change order status

        [HttpPut("ChangeStatus")]
        public async Task<ActionResult<Object>> ChangeOrderStatus( int orderId, string ClientEmail)
        {

            // get client by email
            var client = await _unit.ClientProfileRepo.GetProfileByEmailAsync(ClientEmail);

            var result = await _unit.OrderRepository.ChangeStatus( orderId);

            if (result == true && client!=null)
            {
                var email = new Email()
                {
                    To = ClientEmail,
                    Subject = $"Oder Status",
                    Body = $"Dear {client.FirstName},  order status changed successfully."
                };

                try
                {
                    _emailSetting.SendEmailAsync(email);
                    return Ok(new { status = result, message = "order status changed successfully, please check your email" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
            }

            else return BadRequest(new { status = result, message = "order status faild to change" });
        
        }










        }
}
