using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class SpecialMealRepo : GenericRepo<SpecialMeal>, ISpecialMealRepo
    {
        private readonly CookleeDbContext dbcontext;

        public SpecialMealRepo(CookleeDbContext dbcontext) : base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IReadOnlyList<SpecialMeal>> FindAsync(Expression<Func<SpecialMeal, bool>> predicate)
        {
            return await dbcontext.Set<SpecialMeal>().Where(predicate).ToListAsync();
        }

        async Task<IReadOnlyList<SpecialMeal>> ISpecialMealRepo.getAllByClient(int clientId)
        {
           return await dbcontext.SpecialMeals.Where(sm=>sm.ClientId== clientId).ToListAsync();
        }
    }
}
