using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Repository.Contract;
using Cooklee.Data.Service.Contract;
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
        public IChefPageRepo ChefPageRepo { get; set; }
        public IAuthService AuthService { get; set; }
        public ICartRepository CartRepo { get; set; }
        public IMealRepository MealRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IFavouriteRepository FavoriteRepository { get; set; }
        public ISpecialMealRepo SpecialMealRepo { get; set; }

        public UnitOfWork
            (
            IUserRepository<AppUser> userRepo,
            IClientProfileRepo clientProfileRepo,
            IChefPageRepo chefPageRepo,
            IAuthService authService,
            ICartRepository cartRepository,
            IMealRepository mealRepository,
            IOrderRepository orderRepository,
            IFavouriteRepository favouriteRepository,
            ISpecialMealRepo specialMealRepo
            )
        {
            UserRepo = userRepo;
            ClientProfileRepo = clientProfileRepo;
            ChefPageRepo = chefPageRepo;
            AuthService = authService;
            CartRepo = cartRepository;
            MealRepository = mealRepository;
            OrderRepository = orderRepository;
            FavoriteRepository = favouriteRepository;
            SpecialMealRepo = specialMealRepo;
        }
    }
}

