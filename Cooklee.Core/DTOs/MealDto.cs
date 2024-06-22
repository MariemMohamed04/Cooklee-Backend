using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
	public class MealDto
	{
		public string Id {  get; set; }
		public string MealName { get; set; }
		public string MealDescription { get; set; }
		[DefaultValue(false)]
		public bool IsHealthy { get; set; }
		[DefaultValue(0)]
		public float Rate { get; set; }
		public List<string> tag {  get; set; }
		public float Price {  get; set; }

	}
}
