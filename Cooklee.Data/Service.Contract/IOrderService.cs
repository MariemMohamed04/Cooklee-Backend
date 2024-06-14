using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Service.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateAsync(string clientEmail, int cartId, OrderAddress shippingAddress);
        Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string clientEmail);
        Task<Order?> GetOrderByIdForUserAsync(int orderId, string clientEmail);

    }
}
