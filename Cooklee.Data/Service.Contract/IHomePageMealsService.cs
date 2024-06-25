using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Service.Contract
{
    public interface IHomePageMealsService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
    }
}
