using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Service.Abstracts
{
	public interface IMealService
	{
		public Task<List<Meal>> GetAllMealAsync();
	}
}
