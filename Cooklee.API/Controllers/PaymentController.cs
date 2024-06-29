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
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<string>>> PaymentData( string cartid , string orderEmail)
        {

            var  auth_token= await _paymentService.GetAuthTokenAsync();
            var amount = await _paymentService.GetAmountAsync(cartid);

            var PayOrderId = await _paymentService.GeteOrderIdAsync(auth_token, amount);

            var payment_key = await _paymentService.GetRequestPaymentKeyAsync(auth_token, PayOrderId, 1000 , orderEmail);

            if (payment_key == null)
            {
                return BadRequest(string.Empty);

            }

            return Ok(payment_key);
        }

    }
}