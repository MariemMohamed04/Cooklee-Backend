using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Order
{
    public class MealItemOrder
    {
        public MealItemOrder()
        {
            
        }

        public MealItemOrder(int mealId, string mealName, string pictureUrl)
        {
            MealId = mealId;
            MealName = mealName;
            PictureUrl = pictureUrl;
        }

        public int MealId { get; set; }
        public string MealName { get; set; }
        public string PictureUrl { get; set; }
    }
}
