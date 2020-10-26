using StrategyGame.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Building
{
    public interface IBuildingService
    {
        Task<CountryBuildingConnector> CheckForBuildInProgress(int countryId);
        Task<IEnumerable<CountryBuildingConnector>> GetAllBuildingsOfCountry(int countryId);
        Task<IEnumerable<Model.Entities.Building>> GetAllBuildingTypes();
        Task<CountryBuildingConnector> StartBuildForCountry(int buildingId, int countryId);
    }
}