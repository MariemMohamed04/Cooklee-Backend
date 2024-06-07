using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository<AppUser> UserRepo { get; set; }
        public IClientProfileRepo ClientProfileRepo { get; set; }
          public  IChefPageRepo ChefPageRepo { get; set; }
        public UnitOfWork
            (
            IUserRepository<AppUser> userRepo,
             IClientProfileRepo clientProfileRepo,
             IChefPageRepo chefPageRepo
            )
        
        
        {
            UserRepo = userRepo;
            ClientProfileRepo = clientProfileRepo;
            ChefPageRepo = chefPageRepo;
        }
    }
}
