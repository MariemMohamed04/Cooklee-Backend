using Cooklee.Data.Entities;
using Cooklee.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        }
    }
}
