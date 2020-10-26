using Microsoft.EntityFrameworkCore;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.User
{
    public class UserService : IUserService
    {
        private readonly UnderseaDbContext context;

        public UserService(UnderseaDbContext context)
        {
            this.context = context;
        }

        public async Task<ApplicationUser> GetUserById(int userId)
        {
            return await context.Users.Include(u => u.Country).FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
