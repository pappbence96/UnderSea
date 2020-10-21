using StrategyGame.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Research
{
    public interface IResearchService
    {
        Task<CountryResearchConnector> CheckForResearchInProgress(int countryId);
        Task<IEnumerable<Model.Entities.Research>> GetAllResearchTypes();
        Task<IEnumerable<Model.Entities.Research>> GetAvailableResearchTypesForCountry(int countryId);
        Task<CountryResearchConnector> StartResearchForCountry(int researchId, int countryId);
    }
}