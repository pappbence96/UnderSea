using Microsoft.EntityFrameworkCore;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Building
{
    public class BuildingService : IBuildingService
    {
        private static readonly int BuildingDefaultLength = 5;

        private readonly UnderseaDbContext context;

        public BuildingService(UnderseaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Model.Entities.Building>> GetAllBuildingTypes()
        {
            return await context.Buildings.ToListAsync();
        }

        public async Task<CountryBuildingConnector> CheckForBuildInProgress(int countryId)
        {
            return await context.CountryBuildingConnectors.FirstOrDefaultAsync(c => c.CountryId == countryId && !c.IsComplete);
        }

        public async Task<CountryBuildingConnector> StartBuildForCountry(int buildingId, int countryId)
        {
            // Check if the specified IDs are valid
            var building = (await context.Buildings.FirstOrDefaultAsync(r => r.Id == buildingId))
                ?? throw new KeyNotFoundException($"Building with ID {buildingId} not found.");
            var country = (await context.Countries.FirstOrDefaultAsync(r => r.Id == countryId))
                ?? throw new KeyNotFoundException($"Country with ID {countryId} not found.");

            // Check if a build is already in progress
            var buildInProgress = CheckForBuildInProgress(countryId)
                ?? throw new InvalidOperationException($"Country {countryId} has a build in progress.");

            // Check if the country has sufficient funds
            if (country.Pearl < building.Price)
            {
                throw new InvalidOperationException($"The country has insufficient funds (has {country.Pearl}, building costs {building.Price})");
            }
            country.Pearl -= building.Price;

            // Create the concrete connection
            var connection = new CountryBuildingConnector
            {
                Country = country,
                Building = building,
                BuildStartedRound = await context.Rounds.FirstOrDefaultAsync(r => r.IsActive),
                RoundsLeftUntilCompletion = BuildingDefaultLength
            };
            context.CountryBuildingConnectors.Add(connection);
            await context.SaveChangesAsync();
            return connection;
        }
    }
}
