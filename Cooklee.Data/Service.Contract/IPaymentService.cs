using Cooklee.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Service.Contract
{
    public interface IPaymentService
    {
        public Task<decimal> GetAmountAsync(string cartid);
        public Task<string> GetAuthTokenAsync();
        public Task<string> GeteOrderIdAsync(string AuthToken, decimal amount);
        public Task<string> GetRequestPaymentKeyAsync(string AuthToken, string PayOrderId, decimal amount, string orderEmail);
    }
}
