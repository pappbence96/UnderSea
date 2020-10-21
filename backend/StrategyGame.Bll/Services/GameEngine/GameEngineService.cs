using Microsoft.EntityFrameworkCore;
using StrategyGame.Bll.Extensions;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.GameEngineService
{
    public class GameEngineService : IGameEngineService
    {
        private const int PopulationDefaultTax = 25;

        private const int BuildingScoreValue = 50;
        private const int PopulationScoreValue = 1;
        private const int ResearchScoreValue = 100;

        private readonly UnderseaDbContext context;

        public GameEngineService(UnderseaDbContext context)
        {
            this.context = context;
        }

        private void PerformPreCombatTasks()
        {
            foreach (var country in context.Countries)
            {
                // Tax calculation : (base population tax + building production) * research modifiers
                int baseTax = country.Population * PopulationDefaultTax;
                int pearlProduction = country.Buildings
                    .Where(c => c.IsComplete)
                    .Sum(b => b.Building.PearlPerRound);
                double pearlModifier = country.Researches
                    .Where(c => c.IsComplete)
                    .Product(c => c.Research.TaxMultiplier);
                country.Pearl += (int)((baseTax + pearlProduction) * pearlModifier);

                // Coral calculation: building production * research modifier
                int coralProduction = country.Buildings
                    .Where(c => c.IsComplete)
                    .Sum(b => b.Building.CoralPerRound);
                double coralModifier = country.Researches
                    .Where(c => c.IsComplete)
                    .Product(c => c.Research.CoralMultiplier);
                country.Coral += (int)(coralProduction * coralModifier);

                // Soldier pay (for now let's assume that countries can go into debt)
                var totalPay = country.Units.Sum(c => c.TotalCount * c.Unit.Pay);
                country.Pearl -= totalPay;

                // Soldier supplying (for now let's assume that countries can go into debt)
                var totalSupply = country.Units.Sum(c => c.TotalCount * c.Unit.Supply);
                country.Coral -= totalSupply;

                // Research progress
                foreach (var research in country.Researches.Where(c => !c.IsComplete))
                {
                    research.RoundsLeftUntilCompletion--;
                }

                // Building progress
                foreach (var building in country.Buildings.Where(c => !c.IsComplete))
                {
                    building.RoundsLeftUntilCompletion--;
                    if (building.IsComplete)
                    {
                        building.Country.Population += building.Building.PopulationOnConstructionFinished;
                        building.Country.Garrison += building.Building.GarrisonOnConstructionFinished;
                    }
                }
            }

        }
        private async Task PerformCombats()
        {
            // Combat
            var currentRound = await context.Rounds.FirstAsync(r => r.IsActive);
            foreach (var combat in currentRound.ActiveCombats)
            {
                // Attacking army strength
                int attackerBaseStrength = combat.Units.Sum(u => u.Count * u.Unit.Attack);
                double attackerAttackMultiplier = combat.Attacker.Researches.Where(r => r.IsComplete).Product(r => r.Research.AttackMultiplier);
                double morale = (new Random().Next(95, 106)) / 100d; // morale between 95% and 105% 
                double attackPower = attackerBaseStrength * attackerAttackMultiplier * morale;

                // Defending army strength
                int defenderBaseStrength = combat.Defender.Units.Sum(u => (u.TotalCount - u.InCombat) * u.Unit.Defense);
                double defenderDefenseMultiplier = combat.Defender.Researches.Where(r => r.IsComplete).Product(r => r.Research.DefenseMultiplier);
                double defensePower = defenderBaseStrength * defenderDefenseMultiplier;

                // If the attacking army won
                if (attackPower > defensePower)
                {
                    // Half of defending army dead, remove half of the pearl and coral amount
                    foreach (var unit in combat.Defender.Units)
                    {
                        // Number of defenders is the amount of units minus those away in combat
                        int deadUnits = (int)Math.Ceiling((unit.TotalCount - unit.InCombat) / 10f);
                        unit.TotalCount -= deadUnits;
                    }
                    // Only allow stealing if the amount is positive
                    if (combat.Defender.Coral > 0)
                    {
                        int stolenCoral = combat.Defender.Coral / 2;
                        combat.Defender.Coral -= stolenCoral;
                        combat.Attacker.Coral += stolenCoral;
                    }
                    if (combat.Defender.Pearl > 0)
                    {
                        int stolenPearl = combat.Defender.Pearl / 2;
                        combat.Defender.Pearl -= stolenPearl;
                        combat.Attacker.Pearl += stolenPearl;
                    }
                }
                // If the defending army won
                else
                {
                    // Half of attacking army dead
                    foreach (var attackerUnit in combat.Units)
                    {
                        int dead = (int)Math.Ceiling(attackerUnit.Count / 10f);
                        var totalUnitsOfType = combat.Attacker.Units.Single(u => u.Unit == attackerUnit.Unit);
                        // Return the attacking troops home, except for the dead
                        totalUnitsOfType.TotalCount -= dead;
                    }
                }
                combat.IsConcluded = true;
            }

            // Reset the "units in combat" to 0 only after all the combats of the round were finished
            foreach (var country in context.Countries)
            {
                foreach (var unit in country.Units)
                {
                    unit.InCombat = 0;
                }
            }
        }
        private async Task CalculateScoreboard()
        {
            // Scoreboard calculation
            var currentRound = await context.Rounds.FirstAsync(r => r.IsActive);
            var newScoreboardEntries = new List<ScoreboardEntry>();
            foreach (var country in context.Countries)
            {
                var newEntry = new ScoreboardEntry
                {
                    Country = country,
                    Round = currentRound,
                    PopulationScore = country.Population * PopulationScoreValue,
                    BuildingScore = country.Buildings.Count(b => b.IsComplete) * BuildingScoreValue,
                    ArmyScore = country.Units.Sum(u => u.TotalCount * u.Unit.ScoreboardValue),
                    ResearchScore = country.Researches.Count(r => r.IsComplete) * ResearchScoreValue
                };
            }

            // Order and set computed position
            newScoreboardEntries = newScoreboardEntries.OrderByDescending(e => e.TotalScore).ToList();
            for (int i = 0; i < newScoreboardEntries.Count; i++)
            {
                newScoreboardEntries[i].Position = i;
            }

            // Finalize round and start the next one
            currentRound.IsActive = false;
            currentRound.TickedAt = DateTime.UtcNow;
            var newRound = new Round() { IsActive = true };
            context.Rounds.Add(newRound);
        }

        public async Task PerformTick()
        {
            PerformPreCombatTasks();
            await PerformCombats();
            await CalculateScoreboard();
            await context.SaveChangesAsync();
        }
    }
}
