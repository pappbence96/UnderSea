using Microsoft.EntityFrameworkCore;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Dal.Seed
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            SeedBuildings(builder);
            SeedUnits(builder);
            SeedResearch(builder);
        }

        private static void SeedResearch(ModelBuilder builder)
        {
            var researchTypes = new List<Research>()
            {
                new Research
                {
                    Id = 1, Name = "Mud tractor", CoralMultiplier = 1.1
                },
                new Research
                {
                    Id = 2, Name = "Mud harvester", CoralMultiplier = 1.15
                },
                new Research
                {
                    Id = 3, Name = "Coral wall", DefenseMultiplier = 1.2
                },
                new Research
                {
                    Id = 4, Name = "Sonar cannon", AttackMultiplier = 1.2
                },
                new Research
                {
                    Id = 5, Name = "Underwater martial arts", AttackMultiplier = 1.1, DefenseMultiplier = 1.1
                },
                new Research
                {
                    Id = 6, Name = "Alchemy", TaxMultiplier = 1.3
                }
            };
            builder.Entity<Research>().HasData(researchTypes);
            
        }

        private static void SeedUnits(ModelBuilder builder)
        {
            var unitTypes = new List<Unit>()
            {
                new Unit
                {
                    Id = 1, Name = "Rush seal", Attack = 6, Defense = 2, Price = 50, Pay = 1, Supply = 1
                },
                new Unit
                {
                    Id = 2, Name = "War sea horse", Attack = 2, Defense = 6, Price = 50, Pay = 1, Supply = 1
                },
                new Unit
                {
                    Id = 3, Name = "Laser shark", Attack = 5, Defense = 5, Price = 100, Pay = 3, Supply = 2
                }
            };
            builder.Entity<Unit>().HasData(unitTypes);
        }

        private static void SeedBuildings(ModelBuilder builder)
        {
            var buildingTypes = new List<Building>()
            {
                new Building
                {
                    Id = 1, Name = "Flow controller", Price = 1000, CoralPerRound = 200, PopulationOnConstructionFinished = 50
                },
                new Building
                {
                    Id = 2, Name = "Reef castle", Price = 1000, GarrisonOnConstructionFinished = 200
                }
            };
            builder.Entity<Building>().HasData(buildingTypes);
        }
    }
}
