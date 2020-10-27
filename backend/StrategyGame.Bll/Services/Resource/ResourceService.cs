using Microsoft.EntityFrameworkCore;
using StrategyGame.Bll.Extensions;
using StrategyGame.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Resource
{
    public class ResourceService : IResourceService
    {
        private readonly UnderseaDbContext context;

        private const int PopulationDefaultTax = 25;

        public ResourceService(UnderseaDbContext context)
        {
            this.context = context;
        }

        public async Task<int> GetCoralIncrementOfCountry(int countryId)
        {
            var country = (await context.Countries
                .Include(c => c.Buildings).ThenInclude(b => b.Building)
                .Include(c => c.Researches).ThenInclude(r => r.Research)
                .FirstOrDefaultAsync(c => c.Id == countryId))
                ?? throw new KeyNotFoundException($"Country not found with id: {countryId}");

            int baseTax = country.Population * PopulationDefaultTax;
            int pearlProduction = country.Buildings
                .Where(c => c.IsComplete)
                .Sum(b => b.Building.PearlPerRound);
            double pearlModifier = country.Researches
                .Where(c => c.IsComplete)
                .Product(c => c.Research.TaxMultiplier);

            return (int)((baseTax + pearlProduction) * pearlModifier);
        }

        public async Task<int> GetPearlIncrementOfCountry(int countryId)
        { 
            var country = (await context.Countries
                .Include(c => c.Buildings).ThenInclude(b => b.Building)
                .Include(c => c.Researches).ThenInclude(r => r.Research)
                .FirstOrDefaultAsync(c => c.Id == countryId))
                ?? throw new KeyNotFoundException($"Country not found with id: {countryId}");

            int coralProduction = country.Buildings
                    .Where(c => c.IsComplete)
                    .Sum(b => b.Building.CoralPerRound);
            double coralModifier = country.Researches
                .Where(c => c.IsComplete)
                .Product(c => c.Research.CoralMultiplier);

            return (int)(coralProduction * coralModifier);
        }
    }
}
