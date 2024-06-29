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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Seed data for ChefPage
        //    modelBuilder.Entity<ChefPage>().HasData(
        //        new ChefPage
        //        {
        //            Id = 1,
        //            DisplayName = "Chef A",
        //            PhoneNumber = "1234567890",
        //            WalletNumber = "wallet123",
        //            FullAddress = "Address 1",
        //            paymentMethod = ChefPaymentMethod.Cash,
        //            IsActive = true,
        //            ClientId = 1
        //        },
        //        new ChefPage
        //        {
        //            Id = 2,
        //            DisplayName = "Chef B",
        //            PhoneNumber = "0987654321",
        //            WalletNumber = "wallet456",
        //            FullAddress = "Address 2",
        //            paymentMethod = ChefPaymentMethod.MobileWallet,
        //            IsActive = true,
        //            ClientId = 2
        //        }
        //    );

        //    // Seed data for Meal
        //    modelBuilder.Entity<Meal>().HasData(
        //        new Meal
        //        {
        //            Id = 1,
        //            MealName = "Meal 1",
        //            MealDescription = "Description 1",
        //            IsHealthy = true,
        //            IsAvailable = true,
        //            IsSpecial = false,
        //            Price = 10,
        //            Rate = 4.5f,
        //            Image = "image1.jpg",
        //            ChefPageId = 1
        //        },
        //        new Meal
        //        {
        //            Id = 2,
        //            MealName = "Meal 2",
        //            MealDescription = "Description 2",
        //            IsHealthy = false,
        //            IsAvailable = true,
        //            IsSpecial = true,
        //            Price = 20,
        //            Rate = 4.8f,
        //            Image = "image2.jpg",
        //            ChefPageId = 1
        //        },
        //        new Meal
        //        {
        //            Id = 3,
        //            MealName = "Meal 3",
        //            MealDescription = "Description 3",
        //            IsHealthy = true,
        //            IsAvailable = false,
        //            IsSpecial = true,
        //            Price = 15,
        //            Rate = 4.6f,
        //            Image = "image3.jpg",
        //            ChefPageId = 2
        //        },
        //        new Meal
        //        {
        //            Id = 4,
        //            MealName = "Meal 4",
        //            MealDescription = "Description 4",
        //            IsHealthy = false,
        //            IsAvailable = true,
        //            IsSpecial = false,
        //            Price = 25,
        //            Rate = 4.2f,
        //            Image = "image4.jpg",
        //            ChefPageId = 2
        //        }
        //    );

        //    // Additional configurations for other entities
        //}


        //internal Task FindAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
