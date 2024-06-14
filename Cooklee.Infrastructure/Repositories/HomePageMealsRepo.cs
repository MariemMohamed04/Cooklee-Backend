using Cooklee.Data.Entities;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cooklee.Data.Repository.Contract.IHomePageMealsRep;

namespace Cooklee.Infrastructure.Repositories
{
    public class HomePageMealsRepo :IMealsRepo
    {
            private readonly CookleeDbContext _dbcontext;

            public HomePageMealsRepo(CookleeDbContext context)
            {
                _dbcontext = context;
            }

            public async Task<List<Meal>> GetAllMealsAsync()
            {
                return await _dbcontext.Meals.ToListAsync();
            }
    }
}
