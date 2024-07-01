using Cooklee.Data.Entities;
using Cooklee.Data.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cooklee.Data.Repository.Contract.IHomePageMealsRep;

namespace Cooklee.Service.Services
{
    public class HomePageMealsService
    {
            public class MealsService : IHomePageMealsService
            {
                private readonly IMealsRepo _mealRepository;

                public MealsService(IMealsRepo mealRepository)
                {
                    _mealRepository = mealRepository;
                }

                public async Task<IEnumerable<Meal>> GetAllMealsAsync()
                {
                    var allMeals = await _mealRepository.GetAllMealsAsync();
                    var randomMeals = GetRandomMeals(allMeals, 8);
                    return randomMeals;
                }
                private IEnumerable<Meal> GetRandomMeals(IEnumerable<Meal> meals, int count)
                {
                    var random = new Random();
                    var selectedMeals = meals.OrderBy(x => random.Next()).Take(count);
                    return selectedMeals;
                }
            }
    }
}
