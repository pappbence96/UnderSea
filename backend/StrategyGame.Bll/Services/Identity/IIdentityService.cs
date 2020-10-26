using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Identity
{
    public interface IIdentityService
    {
        Task<LoginResponse> CreateTokenForUser(LoginModel model);
        Task RegisterNewUser(RegisterModel model);
    }
}