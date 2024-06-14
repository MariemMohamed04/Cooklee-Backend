using Cooklee.Data.Entities;
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
        IClientProfileRepo ClientProfileRepo { get; set; }
        IChefPageRepo ChefPageRepo { get; set; }
        IAuthService AuthService { get; set; }
        ICartRepository CartRepo { get; set; }
        IMealRepository MealRepository { get; set; }
        IGenericRepo<SpecialMeal> SpecialMealRepo { get; set; }
    }
}
