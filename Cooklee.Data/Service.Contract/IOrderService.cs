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
        Task<Order?> CreateAsync(string clientEmail, string cartId, ShipmentDetails shipmentDetails);
        Task<IReadOnlyList<Order?>> GetOrdersForClientAsync(string clientEmail);
        Task<Order?> GetOrderByIdForClientAsync(int orderId, string clientEmail);
    }
}
