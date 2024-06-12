using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using CookLeeProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepo<Review>, IReviewRepository
    {
        #region field
        private readonly CookleeDbContext _dbcontext;
        #endregion
        #region constructor
        public ReviewRepository(CookleeDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
        #endregion
        #region Handle function
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _dbcontext.Reviews.ToListAsync();
        }
        #endregion
    }
}
