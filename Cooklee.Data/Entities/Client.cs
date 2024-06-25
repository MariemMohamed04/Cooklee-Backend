using CookLeeProject.Data.Entities;
using Cooklee.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ImgURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }



        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Meal>? Meals { get; set; }

        public int? ChefId { get; set; }
        public ChefPage? Chef { get; set; }
        public List<SpecialMeal>? SpecialMeals { get; set; }

        public List<Review>? Reviews { get; set; }
        public Client()
        {
            SpecialMeals = new List<SpecialMeal>();
            Meals = new List<Meal>();
        }
    }
}
