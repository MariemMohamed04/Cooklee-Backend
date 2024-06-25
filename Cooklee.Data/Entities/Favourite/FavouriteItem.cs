using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Favourite
{
    public class FavouriteItem
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
    }
}
