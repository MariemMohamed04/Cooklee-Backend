using Cooklee.Data.Entities;
using Cooklee.Data.Repository.Contract;
using Cooklee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class GenericRepo<T> :   IGenericRepo<T>  where T : BaseEntity
    {
        private readonly CookleeDbContext _dbcontext;
        public GenericRepo(CookleeDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await _dbcontext.Set<T>().FindAsync(id);
            if (entity == null)
                return false;

            _dbcontext.Set<T>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<T?> UpdateAsync(int id, T entityToUpdate)
        {
            T entity = await _dbcontext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbcontext.Entry(entity).CurrentValues.SetValues(entityToUpdate);
                await _dbcontext.SaveChangesAsync();
            }
            return entityToUpdate;
        }

        public async Task<T?> AddAsync(T entity)
        {

            await _dbcontext.AddAsync(entity);
			return entity;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbcontext.SaveChangesAsync();
        }
        public async Task<bool> CheckIfExistsAsync(int id)
        {
            T entity = await _dbcontext.Set<T>().FindAsync(id);
            return entity != null;
        }


    }
}

