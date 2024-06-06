﻿using CookLeeProject.Data.Entities;
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
        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Meal>? Meals { get; set; }

        public int? ChefId { get; set; }
        public ChefPage? Chef { get; set; }
        public List<SpecialMeal>? SpecialMeals { get; set; }

        public List<Review>? reviews { get; set; }
        public Client()
        {
            SpecialMeals = new List<SpecialMeal>();
            Meals = new List<Meal>();
        }
    }
}