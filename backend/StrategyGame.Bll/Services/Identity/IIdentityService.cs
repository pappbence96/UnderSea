using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Identity
{
    public interface IIdentityService
    {
        Task<string> CreateTokenForUser(LoginModel model);
        Task RegisterNewUser(RegisterModel model);
    }
}