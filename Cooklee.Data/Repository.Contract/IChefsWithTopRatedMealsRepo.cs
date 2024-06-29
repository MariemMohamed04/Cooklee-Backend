using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public class ChiefMealDto 
    {
        public string MealName { get; set; }
        public float Price { get; set; }
        [DefaultValue(0)]
        public float Rate { get; set; }
    }
    public class ChefsWithTopRatedMealsDto 
    {
        public string IdImgURL { get; set; }
        public string Name { get; set; }
        public List<ChiefMealDto> TopMeals { get; set; }
    }
    public class IChefsWithTopRatedMealsRepo
    {
        public interface IChefsWithMealsRepo 
        {
            public Task<IEnumerable<ChefsWithTopRatedMealsDto>> GetAllChefsWithTopRatedMealsAsync();
        }
    }
}
