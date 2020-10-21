using Microsoft.EntityFrameworkCore;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Units
{
    public class UnitsService : IUnitsService
    {
        private readonly UnderseaDbContext context;

        public UnitsService(UnderseaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllUnitTypes()
        {
            return await context.Units.ToListAsync();
        }

        public async Task<IEnumerable<CountryUnitConnector>> GetAllUnitsOfCountry(int countryId)
        {
            var country = (await context.Countries.Include(c => c.Units).FirstOrDefaultAsync(r => r.Id == countryId))
                ?? throw new KeyNotFoundException($"Country with ID {countryId} not found.");
            return country.Units;
        }

        public async Task<IEnumerable<CountryUnitConnector>> RecruitUnitsForCountry(Dictionary<int, int> unitCounts, int countryId)
        {
            var country = (await context.Countries.Include(c => c.Units).FirstOrDefaultAsync(r => r.Id == countryId))
                ?? throw new KeyNotFoundException($"Country with ID {countryId} not found.");

            int totalPrice = 0;
            foreach (var (unitId, unitCount) in unitCounts)
            {
                var unit = (await context.Units.FirstOrDefaultAsync(u => u.Id == unitId))
                    ?? throw new KeyNotFoundException($"Unit with ID {unitId} not found.");

                totalPrice += unitCount * unit.Price;

                var connection = country.Units.FirstOrDefault(c => c.Unit == unit);
                if (connection == null)
                {
                    new CountryUnitConnector { Country = country, Unit = unit };
                    country.Units.Add(connection);
                }
                connection.TotalCount += unitCount;
            }

            if (totalPrice > country.Pearl)
            {
                throw new InvalidOperationException($"Total recruitment cost exceeds country pearl funds (has {country.Pearl}, costs {totalPrice}).");
            }

            await context.SaveChangesAsync();
            return country.Units;
        }
    }
}
