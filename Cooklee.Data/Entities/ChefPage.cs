using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities
{
	public class ChefPage : BaseEntity
	{
        public string DisplayName { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        //public string AppUserId { get; set; }
        //public AppUser AppUser { get; set; }

        public ICollection<Meal> Meals { get; set; }
        public List<SpecialMeal> SpecialMeals { get; set; }
        public ChefPage()
        {
            Meals = new List<Meal>();
        }

    }
}
