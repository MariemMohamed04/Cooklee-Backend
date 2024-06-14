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
            return await dbcontext.Orders.Include(O => O.Items).Where(O => O.ClientEmail == email).ToListAsync();
        }

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string clientEmail)
        {
            return await dbcontext.Orders.Include(O => O.Items).SingleOrDefaultAsync(O => O.Id == orderId && O.ClientEmail == clientEmail);
        }
    }
}
