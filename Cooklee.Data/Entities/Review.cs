using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookLeeProject.Data.Entities
{
    public class Review: BaseEntity
    {
        public string Comment { get; set; }

        [AllowedValues(1,2,3,4,5)]
        public int Rate { get; set; } = 1;


        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }
        public Meal Meal { get; set; }
         


    }
}
