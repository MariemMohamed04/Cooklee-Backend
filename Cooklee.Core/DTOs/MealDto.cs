using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
	public class MealDto
	{
        public int Id { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        [DefaultValue("false")]
        public bool IsHealthy { get; set; }
        [DefaultValue("true")]
        public bool IsAvailable { get; set; }
        [DefaultValue("false")]
        public bool IsSpecial { get; set; }
        [DefaultValue("false")]
        public bool IsAccepeted { get; set; }

        public float Price { get; set; }
        [DefaultValue(0)]
        public float Rate { get; set; }
        public string Image { get; set; }
        public List<string>? Tags { get; set; }
        public string? chefPageName { get; set; }
        public int ChefPageId { get; set; }
    }
}
