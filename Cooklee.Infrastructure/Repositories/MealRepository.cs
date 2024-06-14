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

	}
}
