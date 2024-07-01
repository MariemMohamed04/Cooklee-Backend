using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
		#endregion

		#region Handel function
		//public async Task<List<Meal>> GetAllMealAsync()

		public async Task<IEnumerable<Meal>> GetMealsOrderedByRateAsync()
		{
			return await _dbContext.Set<Meal>()
								 .OrderByDescending(m => m.Rate)
								 .ToListAsync();
		}

		public async Task<IEnumerable<Meal>> GetAllChefMealsAsync(int id)
		{
			return await _dbContext.Set<Meal>()
									.Where(m	=> m.ChefPageId == id)
									.OrderByDescending(m => m.Rate)
									.ToListAsync();
		}

		public async Task<IEnumerable<Meal>?> GetMealByNameAsync(string MealName)
		{
			var meal = await _dbContext.Set<Meal>()
										.Where(m => m.MealName == MealName)
										.OrderByDescending(m => m.Rate)
										.ToListAsync();
			return meal;
		}
		public async Task<Meal> UpdateMeal( Meal meal)
		{
			_dbContext.Entry(meal).State = EntityState.Modified;
			//await _dbContext.SaveChangesAsync();
			return meal;
		}
        #endregion

        public async Task<bool> AcceptMeal(int mealId)
        {
            var unAcceptedMeal = await _dbContext.Meals.SingleOrDefaultAsync(m => m.Id == mealId);
            if (unAcceptedMeal != null)
            {
                unAcceptedMeal.IsAccepted = true;
                _dbContext.Update(unAcceptedMeal);
                var resut = await _dbContext.SaveChangesAsync();
                if (resut > 0)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Meal>> GetUnAcceptedMeals()
        {
            var unAcceptedMeals = await _dbContext.Meals.Where(m => m.IsAccepted == false).ToListAsync();
            return unAcceptedMeals;
        }

    }
}
