using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface IClientProfileRepo : IGenericRepo<Client>
    {
        Task<Client?> GetProfileAsync(string id);
        Task<Client?> GetProfileByEmailAsync(string Email);
        Task<Client?> GetClientBychefAsync(int id);
        Task<Client?> UpdateProfileAsync(string id,  Client updatedClient);
    }
}
