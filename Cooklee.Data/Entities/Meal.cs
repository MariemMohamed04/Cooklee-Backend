using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cooklee.Data.Entities;
using CookLeeProject.Data.Entities;

namespace Cooklee.Data.Entities
{
    public enum Tag 
	{ 
		EASTERN_FOOD, 
		WESTERN_FOODD, 
		VEGETABLES, 
		MEATS, 
		GRAINS_AND_PASTA, 
		SALADS_AND_DIPS, 
		SOUPS, 
		SEA_FOOD, 
		DESSERTS 
	}

    public class Meal : BaseEntity
    {
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        [DefaultValue("false")]
        public bool IsHealthy { get; set; }

        [DefaultValue("true")]
        public bool IsAvailable { get; set; }
        [DefaultValue("false")]
        public bool IsSpecial { get; set; }
        [DefaultValue("false")]
        public bool IsAccepted { get; set; }
        public float Price { get; set; }
        [DefaultValue(0)]
        public float Rate { get; set; }
        public string Image { get; set; }
        public List<string>? Tags { get; set; }



        public List<Review>? Reviews { get; set; }
        [ForeignKey(nameof(ChefPage))]
        public int ChefPageId { get; set; }
        public ChefPage ChefPage { get; set; }
        public List<Client>? clients { get; set; }

        public Meal()
        {
            clients = new List<Client>();
            Tags = new List<string>();
        }
    }
}
