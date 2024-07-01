using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooklee.Data.Entities.Order;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cooklee.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepo<Order>, IOrderRepository
    {
        private readonly CookleeDbContext dbcontext;

        public OrderRepository(CookleeDbContext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByEmailAsync(string email)
        {
            return await dbcontext.Orders.Include(O => O.Items).Include(o=>o.ShipmentDetails).Where(O => O.ClientEmail == email).ToListAsync();
        }

        public async Task<Order> GetOrderByIdForClientAsync(int orderId, string clientEmail)
        {
            return await dbcontext.Orders.Include(O => O.Items).SingleOrDefaultAsync(O => O.Id == orderId && O.ClientEmail == clientEmail);
        }

        public async Task<Order> GetOrderByEmail(string userEmail)
        {
            return await dbcontext.Orders.Include(O => O.Items).FirstOrDefaultAsync(o=>o.ClientEmail== userEmail);
        }

        public async Task<IReadOnlyList<Order>> GetUndeliverdOrders()
        {
           var UndeliverdOrders = await dbcontext.Orders.Where(o=>o.Status!= OrderStatus.Deliverd).ToListAsync();
            return UndeliverdOrders;
        }

        public async Task<IReadOnlyList<Order>> GetDeliverdOrders()
        {
            var deliverdOrders = await dbcontext.Orders.Where(o => o.Status == OrderStatus.Deliverd).ToListAsync();
            return deliverdOrders;
        }

        public async Task<bool> ChangeStatus(int orderId)
        {
            var undeliverdOrder = await dbcontext.Orders.SingleOrDefaultAsync(o=>o.Id== orderId);
            if (undeliverdOrder != null || (undeliverdOrder.Status!=OrderStatus.Deliverd))
            {
                
                if (undeliverdOrder.Status == OrderStatus.Pending)
                {
                    undeliverdOrder.Status = OrderStatus.OutforDelivery;

                    dbcontext.Update(undeliverdOrder);

                    var resut = await dbcontext.SaveChangesAsync();
                    if (resut > 0)
                    {
                        return true;
                    }
                }

               else if (undeliverdOrder.Status == OrderStatus.OutforDelivery)
                {
                    undeliverdOrder.Status = OrderStatus.Deliverd;

                    dbcontext.Update(undeliverdOrder);

                    var resut = await dbcontext.SaveChangesAsync();
                    if (resut > 0)
                    {
                        return true;
                    }
                }

            }

            return false;

        }

    }
}
