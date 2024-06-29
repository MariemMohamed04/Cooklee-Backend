using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ChefsWithTopRatedMealsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChiefMealDto> TopMeals { get; set; }
    }
}
