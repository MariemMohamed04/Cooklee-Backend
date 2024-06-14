using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public class IHomePageMealsRep
    {
        public interface IMealsRepo
        {
            public Task<List<Meal>> GetAllMealsAsync();
        }
    }
}
