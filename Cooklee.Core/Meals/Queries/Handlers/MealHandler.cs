//using MediatR;
//using Cooklee.Core.Meals.Queries.Models;
//using Cooklee.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Cooklee.Data.Repository.Contract;

//namespace Cooklee.Core.Meals.Queries.Handlers
//{
//	//da hynfz service
	
//	public class MealHandler : IRequestHandler<GetAllMealQuery, List<Meal>>
//	{
//		#region fields
//		public readonly IMealRepository _mealRepository;
//		#endregion

//		#region Constructor
//		public MealHandler(IMealRepository mealRepository)
//		{
//			_mealRepository = mealRepository;	
//		}
//		#endregion

//		#region handle function
//		//public async Task<List<Meal>> Handle(GetAllMealQuery request, CancellationToken cancellationToken)
//		//{
//		//	return await _mealRepository.GetAllMealAsync();
//		//}
//		#endregion
//	}
//}
