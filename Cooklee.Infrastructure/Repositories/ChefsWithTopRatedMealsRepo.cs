using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cooklee.Data.Repository.Contract.IChefsWithTopRatedMealsRepo;
using System.ComponentModel;

namespace Cooklee.Infrastructure.Repositories
{
   
    public class ChefsWithTopRatedMealsRepo : IChefsWithMealsRepo
    {
        private readonly CookleeDbContext _context;

        public ChefsWithTopRatedMealsRepo(CookleeDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<ChefsWithTopRatedMealsDto>> GetAllChefsWithTopRatedMealsAsync()
        {
            //return (IEnumerable<ChefsWithTopRatedMealsDto>)await _context.ChefPage
            //    .Include(c => c.Meals.OrderByDescending(m => m.Rate).Take(3))
            //    .ToListAsync();
            var chefs = await _context.ChefPage
                .Include(c => c.Meals)
                .ToListAsync();

            var result = chefs.Select(c => new ChefsWithTopRatedMealsDto
            {
                IdImgURL = c.IdImgURL,
                Name = c.DisplayName,
                TopMeals = c.Meals
                    .OrderByDescending(m => m.Rate)
                    .Take(3)
                    .Select(m => new ChiefMealDto
                    {
                        MealName = m.MealName,
                        Price = m.Price,
                        Rate = m.Rate
                    })
                    .ToList()
            }).ToList();

            return result;
        }

    }
}
