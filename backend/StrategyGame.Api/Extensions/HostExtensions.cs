using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.Api.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> CreateRolesAndUsers(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            if(userManager.Users.Count() != 0)
            {
                return host;
            }
            if(!await roleManager.RoleExistsAsync("user"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("user"));
            }
            for(int i = 0; i < 5; i++)
            {
                var newUser = new ApplicationUser() { FirstName = "Teszt", LastName = $"Elek {i}", UserName = $"tesztelek_{i}", Email = $"tesztelekt{i}@test.com", PhoneNumber = $"{i}" };
                var reszkt = await userManager.CreateAsync(newUser, "123456");
                await userManager.AddToRoleAsync(newUser, "user");
            }

            return host;
        }

        public static async Task<IHost> CreateCountries(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UnderseaDbContext>();
            foreach(var user in dbContext.Users.Include(u => u.Country))
            {
                if(user.Country == null)
                {
                    var country = new Country
                    {
                        Name = $"Test country{user.PhoneNumber}",
                        Population = 100,
                        Pearl = 0,
                        Coral = 0,
                        Garrison = 20,
                        User = user
                    };
                    dbContext.Countries.Add(country);
                }
            }
            await dbContext.SaveChangesAsync();
            return host;
        }
    }
}
