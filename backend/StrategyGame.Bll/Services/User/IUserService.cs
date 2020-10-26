using StrategyGame.Model.Entities;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.User
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserById(int userId);
    }
}