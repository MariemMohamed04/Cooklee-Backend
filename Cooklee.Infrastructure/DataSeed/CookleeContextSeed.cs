using Cooklee.Data.Entities;
using Cooklee.Infrastructure.Data;
using Cooklee.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CookLeeProject.Data.Entities;

namespace Cooklee.Infrastructure.DataSeed
{
    public class CookleeContextSeed
    {
        public async static Task SeedAsync(CookleeDbContext context)
        {
            if (context.Clients.Count() == 0)
            {
                var clientsData = File.ReadAllText("../Cooklee.Infrastructure/DataSeed/SeedingFiles/Client.json");
                var clients = JsonSerializer.Deserialize<List<Client>>(clientsData);

                if (clients?.Count() > 0)
                {
                    foreach (var client in clients)
                    {
                        context.Set<Client>().Add(client);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (context.ChefPage.Count() == 0)
            {
                var chifPageData = File.ReadAllText("../Cooklee.Infrastructure/DataSeed/SeedingFiles/ChefPage.json");
                var chefs = JsonSerializer.Deserialize<List<ChefPage>>(chifPageData);

                if (chefs?.Count() > 0)
                {
                    foreach (var chef in chefs)
                    {
                        context.Set<ChefPage>().Add(chef);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (context.Meals.Count() == 0)
            {
                var mealData = File.ReadAllText("../Cooklee.Infrastructure/DataSeed/SeedingFiles/Meal.json");
                var options = new JsonSerializerOptions
                {
                    Converters = { new TagEnumConverter() }
                };
               var meals = JsonSerializer.Deserialize<List<Meal>>(mealData, options);

                if (meals?.Count() > 0)
                {
                    foreach (var meal in meals)
                    {
                        context.Set<Meal>().Add(meal);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (context.SpecialMeals.Count() == 0)
            {
                var specialMealData = File.ReadAllText("../Cooklee.Infrastructure/DataSeed/SeedingFiles/SpecialMeal.json");
                var specialMeals = JsonSerializer.Deserialize<List<SpecialMeal>>(specialMealData);

                if (specialMeals?.Count() > 0)
                {
                    foreach (var specialMeal in specialMeals)
                    {
                        context.Set<SpecialMeal>().Add(specialMeal);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (context.Reviews.Count() == 0)
            {
                var reviewsData = File.ReadAllText("../Cooklee.Infrastructure/DataSeed/SeedingFiles/Review.json");
                var reviews = JsonSerializer.Deserialize<List<Review>>(reviewsData);

                if (reviews?.Count > 0)
                {
                    foreach (var review in reviews)
                    {
                        context.Set<Review>().Add(review);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
