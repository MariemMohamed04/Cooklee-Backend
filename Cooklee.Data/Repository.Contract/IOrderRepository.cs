using Cooklee.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface IOrderRepository : IGenericRepo<Order>
    {
        Task<IReadOnlyList<Order>> GetOrdersByEmailAsync(string email);
        Task<Order> GetOrderByIdForClientAsync(int orderId, string clientEmail);
        Task<Order> GetOrderByEmail(string userEmail);
        Task<IReadOnlyList<Order>> GetUndeliverdOrders();
        Task<IReadOnlyList<Order>> GetDeliverdOrders();
        Task<bool> ChangeStatus(int orderId);


    }
}
