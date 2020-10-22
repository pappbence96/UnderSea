using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Identity
{
    public interface IIdentityService
    {
        Task<object> CreateTokenForUser(LoginModel model);
    }
}