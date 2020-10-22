using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UnderseaDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public IdentityService(UnderseaDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> CreateTokenForUser(LoginModel model)
        {
            if (model == null || !model.Valid)
            {
                throw new ArgumentException("Login model is null.");
            }
            var user = await userManager.FindByNameAsync(model.Username);
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D0n7_L34v3_M3_H3r3"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };
                foreach (var role in roleManager.Roles)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var tokeOptions = new JwtSecurityToken(
                   issuer: "http://localhost:51554",
                   audience: "http://localhost:51554",
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(5),
                   signingCredentials: signinCredentials
               );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
            else
            {
                throw new ArgumentException("Password is incorrect.");
            }
        }

        public async Task RegisterNewUser(RegisterModel model)
        {
            // Validate input
            if (model == null || !model.Valid)
            {
                throw new ArgumentException("Register model is null.");
            }
            if(model.Password != model.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            // Check for existing user and country
            var user = await userManager.FindByNameAsync(model.Username);
            if(user != null)
            {
                throw new InvalidOperationException("User already exists.");
            }
            var country = await context.Countries.FirstOrDefaultAsync(c => c.Name.ToUpper() == model.CountryName.ToUpper());
            if(country != null)
            {
                throw new InvalidOperationException("Country already exists.");
            }

            // Create the entities
            user = new ApplicationUser
            {
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                throw new Exception("User creation failed.");
            }
            await userManager.AddToRoleAsync(user, "user");
            country = new Country
            {
                Name = model.CountryName,
                User = user
            };
            context.Countries.Add(country);
            await context.SaveChangesAsync();
        }
    }
}
