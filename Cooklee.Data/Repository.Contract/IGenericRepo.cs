using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<T?> UpdateAsync(int id, T entityToUpdate);
        Task<T?> AddAsync(T entity);
        Task<int> SaveChanges();
        Task<bool> CheckIfExistsAsync(int id);
    }

}

