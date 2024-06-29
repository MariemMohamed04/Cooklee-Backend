using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Data.Service.Contract;
using Cooklee.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cooklee.Data.Repository.Contract.IChefsWithTopRatedMealsRepo;

namespace Cooklee.Service.Services
{
    public class ChefsWithTopRatedMealsService : IChefsWithTopRatedMealsService
    {
        private readonly IChefsWithMealsRepo _chefsRepo;

        public ChefsWithTopRatedMealsService(IChefsWithMealsRepo chefsRepo)
        {
            _chefsRepo = chefsRepo;
        }

        public async Task<IEnumerable<ChefsWithTopRatedMealsDto>> GetAllChefsWithTopRatedMealsAsync()
        {
            var allChefs = await _chefsRepo.GetAllChefsWithTopRatedMealsAsync();
            return allChefs;
        }
    }
}
