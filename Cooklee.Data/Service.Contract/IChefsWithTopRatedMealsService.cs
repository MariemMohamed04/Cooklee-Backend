using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Service.Contract
{
    public interface IChefsWithTopRatedMealsService
    {
        Task<IEnumerable<ChefsWithTopRatedMealsDto>> GetAllChefsWithTopRatedMealsAsync();
    }
}
