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
    public class ChefPageRepo : GenericRepo<ChefPage>, IChefPageRepo
    {
        private readonly CookleeDbContext _dbcontext;

        public ChefPageRepo(CookleeDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

       public async Task<ChefPage?> GetPageByUser(string userId)
        {
            Client client = await _dbcontext.Clients.SingleOrDefaultAsync(c => c.AppUserId == userId);

            ChefPage chefPage = await _dbcontext.ChefPage.SingleOrDefaultAsync(ch => ch.ClientId == client.Id);
            
            return chefPage;
        }

       public async Task<ChefPage?> UpdatePageAsync(int ClientId, ChefPage updatedChefPage)
        {
            ChefPage chefPage = await _dbcontext.ChefPage.SingleOrDefaultAsync(ch => ch.ClientId == ClientId);
            updatedChefPage.ClientId=ClientId;
            updatedChefPage.Id = chefPage.Id;

            if (chefPage != null)
            {
                _dbcontext.Entry(chefPage).CurrentValues.SetValues(updatedChefPage);


            }
            return updatedChefPage;
        }

        public async Task<List<ChefPage>>? GetUnActiveChefPages()
        {
            var unActivePages = await _dbcontext.ChefPage.Where(p => p.IsActive == false).ToListAsync();
            return unActivePages;
        }

        public async Task<bool> ActivatePage(int chefId)
        {
            var unActiveChefpage = await _dbcontext.ChefPage.SingleOrDefaultAsync(cp => cp.Id == chefId);
            if (unActiveChefpage != null)
            {
                unActiveChefpage.IsActive = true;

                _dbcontext.Update(unActiveChefpage);

               var resut=  await _dbcontext.SaveChangesAsync();
                if(resut >0)
                {
                    return true;
                }

                return false;
                
            }

            return true;

        }

        public async Task<bool> SendFeedback(int chefId)
        {
            var unActiveChefpage = await _dbcontext.ChefPage.SingleOrDefaultAsync(cp => cp.Id == chefId);
            if (unActiveChefpage.IsActive == false)
            {
                return true;
            }
            return false;
        }
    }
}
