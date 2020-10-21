using StrategyGame.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Units
{
    public interface IUnitsService
    {
        Task<Combat> AttackAnotherCountry(int attackerId, int defenderId, Dictionary<int, int> attackingUnits);
        Task<IEnumerable<CountryUnitConnector>> GetAllUnitsOfCountry(int countryId);
        Task<IEnumerable<Unit>> GetAllUnitTypes();
        Task<IEnumerable<CountryUnitConnector>> RecruitUnitsForCountry(Dictionary<int, int> unitCounts, int countryId);
    }
}