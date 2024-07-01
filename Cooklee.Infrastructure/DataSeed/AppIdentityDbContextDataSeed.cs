using Cooklee.Data.Entities;
using Cooklee.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.DataSeed
{
    public static class AppIdentityDbContextDataSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                var users = new List<AppUser>
                    { new AppUser{
                        DisplayName = "Mariem",
                        Email = "mariem@gmail.com",
                        UserName = "Mariem.Mohamed",
                        PhoneNumber = "333-333-333-33"
                        },
                  new AppUser{
                    DisplayName = "Dalia",
                    Email = "dalia@gmail.com",
                    UserName = "Dalia.Mansour",
                    PhoneNumber = "333-333-333-33", }

                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                //var user = new AppUser()
                //{
                //    DisplayName = "omnia",
                //    Email = "mariem@gmail.com",
                //    UserName = "omnia.khalil",
                //    PhoneNumber = "333-333-333-33",

                //};
                //await userManager.CreateAsync(user, "Pa$$w0rd");

            }

        }
    }


}

