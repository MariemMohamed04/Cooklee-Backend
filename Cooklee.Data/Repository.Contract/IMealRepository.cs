using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
	public interface IMealRepository
	{
		public Task<List<Meal>> GetAllMealAsync();
	}
}
