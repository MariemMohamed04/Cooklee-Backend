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
		/*public string MealName { get; set; }
		public string MealDescription { get; set; }
		public bool IsHealthy { get; set; }
		public bool IsSpecial { get; set; }
		public float Price { get; set; }
		public string Image { get; set; }
		public List<string>tags { get; set; }
		public int ChefPageId { get; set; }

*/
		public string MealName { get; set; }
		public string MealDescription { get; set; }
		public bool IsHealthy { get; set; } = false;
		public bool IsAvailable { get; set; } = true;
		public bool IsSpecial { get; set; } = false;
		public float Price { get; set; }
		public float Rate { get; set; } = 0;
		public string Image { get; set; }
		public List<Tag> Tags { get; set; } = new List<Tag>();

		// Constructor for easy instantiation
		public AddMealDto()
		{
			Tags = new List<Tag>();
		}

	}
}
