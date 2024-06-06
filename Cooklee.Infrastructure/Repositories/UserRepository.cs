using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;

namespace Cooklee.Infrastructure.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : AppUser
    {
        private readonly CookleeDbContext _dbContext;

        public UserRepository(CookleeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T?> GetAsync(string id)
        {
            T entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> UpdateAsync(string id, T entity)
        {
            T newEntity = await _dbContext.Set<T>().FindAsync(id);
            if (newEntity != null)
            {
                _dbContext.Entry(newEntity).CurrentValues.SetValues(entity);

            }
            return newEntity;
        }
    }
}
