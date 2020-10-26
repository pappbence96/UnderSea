using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Resource
{
    public interface IResourceService
    {
        Task<int> GetCoralIncrementOfCountry(int countryId);
        Task<int> GetPearlIncrementOfCountry(int countryId);
    }
}