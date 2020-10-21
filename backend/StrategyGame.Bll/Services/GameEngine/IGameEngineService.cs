using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.GameEngineService
{
    public interface IGameEngineService
    {
        Task PerformTick();
    }
}