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
            var country = (await context.Countries.Include(c => c.Units).ThenInclude(u => u.Unit).FirstOrDefaultAsync(r => r.Id == countryId))
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

        public async Task<Combat> AttackAnotherCountry(int attackerId, int defenderId, Dictionary<int, int> attackingUnits)
        {
            // Validate whether both countries exist
            var attacker = (await context.Countries.Include(c => c.Units).FirstOrDefaultAsync(r => r.Id == attackerId))
                ?? throw new KeyNotFoundException($"Country with ID {attackerId} not found.");
            var defender = (await context.Countries.FirstOrDefaultAsync(r => r.Id == defenderId))
                ?? throw new KeyNotFoundException($"Country with ID {defenderId} not found.");

            var combat = new Combat
            {
                Attacker = attacker,
                Defender = defender,
                Round = await context.Rounds.FirstAsync(r => r.IsActive)
            };

            // Construct attacking army (we can skip straight over those with 0 requested count)
            foreach (var (unitId, unitCount) in attackingUnits.Where(u => u.Value > 0)) 
            {
                var unit = (await context.Units.FirstOrDefaultAsync(u => u.Id == unitId))
                    ?? throw new KeyNotFoundException($"Unit with ID {unitId} not found.");

                // Check if the attacker has enough free army of this type
                int availableUnitCount = 0;
                var attackerUnitOfType = attacker.Units.FirstOrDefault(u => u.Unit == unit);
                if (attackerUnitOfType != null)
                {
                    availableUnitCount = attackerUnitOfType.TotalCount - attackerUnitOfType.InCombat;
                }
                if(unitCount > availableUnitCount)
                {
                    throw new InvalidOperationException($"Insufficient units in garrison (requested: {unitCount}, in garrison: {availableUnitCount}).");
                }

                // Add selected units to the army and mark them as "away in combat"
                combat.Units.Add(new CombatUnitConnector
                {
                    Count = unitCount,
                    Unit = unit
                });
                attackerUnitOfType.InCombat += unitCount;
            }

            await context.SaveChangesAsync();
            return combat;
        }


    }
}
