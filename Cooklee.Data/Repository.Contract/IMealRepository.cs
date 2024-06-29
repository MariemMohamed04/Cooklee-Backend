using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
	public interface IMealRepository:IGenericRepo<Meal>
	{
		Task<IEnumerable<Meal>> GetMealsOrderedByRateAsync();
		Task<IEnumerable<Meal>> GetAllChefMealsAsync(int id);
		Task<IEnumerable<Meal>?> GetMealByNameAsync(string MealName);
		Task<Meal> UpdateMeal(Meal meal);

        Task<IEnumerable<Meal>> GetUnAcceptedMeals();




    }
	
}
