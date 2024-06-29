using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ChiefMealDto
    {
        public string MealName { get; set; }
        public float Price { get; set; }
        [DefaultValue(0)]
        public float Rate { get; set; }
    }
}
