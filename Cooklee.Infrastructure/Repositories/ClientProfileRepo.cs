using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class ClientProfileRepo : GenericRepo<Client>, IClientProfileRepo
    {
        private readonly CookleeDbContext _dbcontext;

        public ClientProfileRepo(CookleeDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Client?> UpdateProfileAsync(string id, Client updatedClient)
        {
            Client client = await _dbcontext.Clients.SingleOrDefaultAsync(c  => c.AppUserId==id);
            updatedClient.AppUserId = id;
            updatedClient.Id = client.Id;

            if (client != null)
            {
                _dbcontext.Entry(client).CurrentValues.SetValues(updatedClient);
              
            }
            return updatedClient;
        }

        public async  Task<Client?>  GetProfileAsync(string id)
        {

            Client client = await _dbcontext.Clients.SingleOrDefaultAsync(c => c.AppUserId == id);

            return client;
        }

        public async Task<Client?> GetClientBychefAsync(int  id)
        {
            Client client = await _dbcontext.Clients.FirstOrDefaultAsync(c => c.ChefId == id);

            return client;

        }

        public async Task<Client?> GetProfileByEmailAsync(string Email)
        {
            var client = await _dbcontext.Clients.FirstOrDefaultAsync(c => c.Email == Email);
            return client;
        }
    }
}
