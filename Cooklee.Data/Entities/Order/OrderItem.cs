using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities.Order
{
    public class OrderItem : BaseEntity
    {
        //public int MealId { get; set; }
        //public string MealName { get; set; }
        //public string PictureUrl { get; set; }

        public OrderItem()
        {
            
        }

        public OrderItem(MealItemOrder meal, int quantity, float price)
        {
            Meal = meal;
            Quantity = quantity;
            Price = price;
        }

        public MealItemOrder Meal { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
