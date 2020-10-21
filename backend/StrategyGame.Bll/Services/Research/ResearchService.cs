using Microsoft.EntityFrameworkCore;
using StrategyGame.Bll.Extensions;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Research
{
    public class ResearchService : IResearchService
    {
        private static readonly int ResearchDefaultLength = 15;

        private readonly UnderseaDbContext context;

        public ResearchService(UnderseaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Model.Entities.Research>> GetAllResearchTypes()
        {
            return await context.Researches.ToListAsync();
        }

        public async Task<IEnumerable<Model.Entities.Research>> GetAvailableResearchTypesForCountry(int countryId)
        {
            return await context.Researches.Where(r => r.Countries.None(c => c.Id == countryId)).ToListAsync();
        }

        public async Task<CountryResearchConnector> CheckForResearchInProgress(int countryId)
        {
            return await context.CountryResearchConnectors.FirstOrDefaultAsync(c => c.CountryId == countryId && !c.IsComplete);
        }

        public async Task<CountryResearchConnector> StartResearchForCountry(int researchId, int countryId)
        {
            // Check if the specified IDs are valid
            var research = (await context.Researches.FirstOrDefaultAsync(r => r.Id == researchId))
                ?? throw new KeyNotFoundException($"Research with ID {researchId} not found.");
            var country = (await context.Countries.FirstOrDefaultAsync(r => r.Id == countryId))
                ?? throw new KeyNotFoundException($"Country with ID {countryId} not found.");

            // Check if the research has been already started
            var connection = await context.CountryResearchConnectors.FirstOrDefaultAsync(c => c.ResearchId == researchId && c.CountryId == countryId);
            if (connection != null)
            {
                throw new InvalidOperationException($"Country {countryId} has already researched {researchId}");
            }
            var researchInProgress = CheckForResearchInProgress(countryId)
                ?? throw new InvalidOperationException($"Country {countryId} has a research in progress.");

            // Check if the country has sufficient funds
            if (country.Pearl < research.Price)
            {
                throw new InvalidOperationException($"The country has insufficient funds (has {country.Pearl}, research costs {research.Price})");
            }
            country.Pearl -= research.Price;

            // Create the concrete connection
            connection = new CountryResearchConnector
            {
                Country = country,
                Research = research,
                ResearchStartedRound = await context.Rounds.FirstOrDefaultAsync(r => r.IsActive),
                RoundsLeftUntilCompletion = ResearchDefaultLength
            };
            context.CountryResearchConnectors.Add(connection);
            await context.SaveChangesAsync();
            return connection;
        }
    }
}
