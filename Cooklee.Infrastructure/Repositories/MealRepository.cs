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
	public class MealRepository : IMealRepository
		
	{
		#region fields
		public CookleeDbContext _dbContext;
		#endregion

		#region Constructor
		public MealRepository(CookleeDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		#endregion

		#region Handel function
		public async Task<List<Meal>> GetAllMealAsync()
		{
			var meals = await _dbContext.Meals.ToListAsync();
			return meals;
		}
		/*public Task<List<Meal>> GetAllMealAsync()
		{
			throw new NotImplementedException();
		}*/
		#endregion

	}
}
