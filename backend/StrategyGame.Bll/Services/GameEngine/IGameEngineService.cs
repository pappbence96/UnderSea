using StrategyGame.Model.Entities;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.GameEngineService
{
    public interface IGameEngineService
    {
        Task<Round> GetActiveRoundAsync();
        Task PerformTick();
    }
}