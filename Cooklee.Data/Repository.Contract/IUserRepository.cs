using Cooklee.Data.Entities.Identity;

namespace Cooklee.Data.Repository.Contract
{
    public interface IUserRepository<T> where T : AppUser
    {
        Task<T?> UpdateAsync(string id, T entity);
        Task<T?> GetAsync(string id);
        Task SaveChanges();
    }
}
