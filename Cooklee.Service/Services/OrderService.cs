using Cooklee.Data.Entities.Order;
using Cooklee.Data.Repository.Contract;
using Cooklee.Data.Service.Contract;
using Cooklee.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unit;
        private readonly ICartRepository cartRepository;

        public OrderService(IUnitOfWork unit, ICartRepository cartRepository)
        {
            this.unit = unit;
            this.cartRepository = cartRepository;
        }

        public async Task<Order?> CreateAsync(string clientEmail, string cartId, ShipmentDetails shipmentDetails)
        {
            var cart = await cartRepository.GetCartAsync(cartId);
            var orderItems = new List<OrderItem>();
            if (cart?.Items?.Count > 0)
            {
                foreach (var cartItem in cart.Items)
                {
                    var meal = await unit.MealRepository.GetAsync(cartItem.Id);
                    if (meal == null) continue;
                    var mealItemOrder = new MealItemOrder(cartItem.Id, meal.MealName, meal.Image);
                    var orderItem = new OrderItem(mealItemOrder, cartItem.Quantity, meal.Price);
                    orderItems.Add(orderItem);
                }
            }
            else
            {
                return null;
            }
            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);
            var order = new Order(clientEmail, shipmentDetails, orderItems, subTotal);
            await unit.OrderRepository.AddAsync(order);
            var result = await unit.OrderRepository.SaveChanges();
            if (result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<Order?>> GetOrdersForClientAsync(string clientEmail)
        {
            var orders = await unit.OrderRepository.GetOrdersByEmailAsync(clientEmail);
            return orders;
        }

        public async Task<Order?> GetOrderByIdForClientAsync(int orderId, string clientEmail)
        {
            return await unit.OrderRepository.GetOrderByIdForClientAsync(orderId, clientEmail);
        }
    }
}
