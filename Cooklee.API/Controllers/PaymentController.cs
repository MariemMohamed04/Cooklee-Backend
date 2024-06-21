using Cooklee.Core.DTOs;
using Cooklee.Data.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooklee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<string>>> PaymentData()
        {

            var  auth_token= await _paymentService.GetAuthTokenAsync();

            var orderId = await _paymentService.GeteOrderIdAsync(auth_token, 1000);
            var payment_key = await _paymentService.GetRequestPaymentKeyAsync(auth_token, orderId, 1000);

            if (payment_key == null)
            {
                return BadRequest(string.Empty);

            }

            return Ok(payment_key);
        }

    }
}