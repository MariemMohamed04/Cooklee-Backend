using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using CookLeeProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
	public class MealRepository : GenericRepo<Meal>,IMealRepository		
	{
		#region fields
		public CookleeDbContext _dbContext;
		#endregion

		#region Constructor

		public MealRepository(CookleeDbContext dbContext):base(dbContext)
		{
			_dbContext = dbContext;
		}

        public Task<Meal?> AddAsync(Meal entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Meal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Handel function
        public async Task<List<Meal>> GetAllMealAsync()
		public async Task<IEnumerable<Meal>> GetMealsOrderedByRateAsync()
		{
			return await _dbContext.Set<Meal>()
								 .OrderByDescending(m => m.Rate)
								 .ToListAsync();
		}


		public async Task<IEnumerable<Meal>> GetAllChefMealsAsync(int id)
		{
			return await _dbContext.Set<Meal>()
									.Where(m=> m.ChefPageId == id)
									.ToListAsync();
		}
		#endregion

        public async Task<Meal?> GetAsync(int id)
        {
            return await _dbContext.Set<Meal>().FindAsync(id);
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<Meal?> UpdateAsync(int id, Meal entityToUpdate)
        {
            throw new NotImplementedException();
        }
        /*public Task<List<Meal>> GetAllMealAsync()
{
    throw new NotImplementedException();
}*/
        #endregion

    }
}
