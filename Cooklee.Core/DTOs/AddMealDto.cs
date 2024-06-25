using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
	public class AddMealDto
	{
		public string MealName { get; set; }
		public string MealDescription { get; set; }
		[DefaultValue(false)]
		public bool IsHealthy { get; set; }
		[DefaultValue(true)]
		public bool IsAvailable { get; set; }
		[DefaultValue(false)]
		public bool IsSpecial { get; set; }
		public float Price { get; set; }
		[DefaultValue(0)]
		public float Rate { get; set; } 
		public string Image { get; set; }
		public List<Tag> Tags { get; set; }
		public int ChefPageId { get; set; }
	}
}
