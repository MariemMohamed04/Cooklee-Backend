﻿using Cooklee.Data.Entities.Identity;
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
        public IAuthService AuthService { get; set; }

        public UnitOfWork(
            IUserRepository<AppUser> userRepo,
            IAuthService authService
            )
        {
            UserRepo = userRepo;
            AuthService = authService;
        }
    }
}
