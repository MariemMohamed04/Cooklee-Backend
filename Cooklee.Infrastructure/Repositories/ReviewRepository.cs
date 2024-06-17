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
        public async Task AddAsync(Review review)
        {
            await _dbcontext.Reviews.AddAsync(review);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _dbcontext.Reviews.ToListAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _dbcontext.Reviews.FindAsync(id);
            if (review == null)
            {
                return false;
            }

            _dbcontext.Reviews.Remove(review);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CheckIfExistsAsync(int ReviewId)
        {
            return await _dbcontext.Reviews.AnyAsync(e => e.Id == ReviewId);
        }
        public async Task<bool> UpdateAsync(int id, Review item)
        {
            try
            {
                Review review = await _dbcontext.Reviews.FindAsync(id);
                if (review == null)
                {
                    return false;
                }

                review.Rate = item.Rate;
                review.Comment = item.Comment;

                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CheckIfExistsAsync(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }
        public async Task<IEnumerable<Review>> GetReviewsByMealIdAsync(int mealId)
        {
            return await _dbcontext.Reviews.Where(r => r.MealId == mealId).ToListAsync();
        }



        #endregion
    }
}
