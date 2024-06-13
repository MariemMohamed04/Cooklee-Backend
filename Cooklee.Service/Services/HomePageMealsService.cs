using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cooklee.Infrastructure.Repositories.HomePageMeals;

namespace Cooklee.Service.Services
{
    public class HomePageMealsService
    {
        public interface IMealService
        {
            Task<IEnumerable<Meal>> GetAllMealsAsync();
        }

        public class MealService : IMealService
        {
            private readonly IMealsRepo _mealRepository;

            public MealService(IMealsRepo mealRepository)
            {
                _mealRepository = mealRepository;
            }

            public async Task<IEnumerable<Meal>> GetAllMealsAsync()
            {
                return await _mealRepository.GetAllMealsAsync();
            }
        }
    }
}
