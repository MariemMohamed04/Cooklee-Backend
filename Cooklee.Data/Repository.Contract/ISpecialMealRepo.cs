using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface ISpecialMealRepo : IGenericRepo<SpecialMeal>
    {
        Task<IReadOnlyList<SpecialMeal>> FindAsync(Expression<Func<SpecialMeal, bool>> predicate);
        Task<IReadOnlyList<SpecialMeal>> getAllByClient(int clientId);

    }
}
