using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Entities.Order;
using CookLeeProject.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Data
{
    public class CookleeDbContext : IdentityDbContext<AppUser>
    {
        public CookleeDbContext(DbContextOptions<CookleeDbContext> options)
            : base(options) { }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ChefPage> ChefPage { get; set; }
        public DbSet<ClientMeal> ClientMeals { get; set; }
        public DbSet<SpecialMeal> SpecialMeals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientMeal>().HasKey(C => new { C.ClientId, C.MealId, C.Id });
            modelBuilder.Entity<Address>().ToTable("Addresses");
        }

        //internal Task FindAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
