using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface IChefPageRepo : IGenericRepo<ChefPage>
    {
        Task<ChefPage?> GetPageByUser(string userId);
        Task<ChefPage?> UpdatePageAsync(int ClientId, ChefPage updatedChefPage);
        //Task<ChefPage?> GetChefByAsync(int id);
        Task<List<ChefPage>>? GetUnActiveChefPages();
        Task<bool> ActivatePage(int chefId);
        Task<bool> SendFeedback(int chefId);
    }
}
