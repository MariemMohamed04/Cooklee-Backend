using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Repository.Contract
{
    public interface IUnitOfWork
    {
        IUserRepository<AppUser> UserRepo { get; set; }
        IAuthService AuthService { get; set; }
    }
}
