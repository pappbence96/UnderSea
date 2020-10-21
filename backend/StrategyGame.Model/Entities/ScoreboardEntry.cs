using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class ScoreboardEntry : EntityBase
    {
        public int Position { get; set; }
        public int TotalScore { get => PopulationScore + BuildingScore + ArmyScore + ResearchScore; }
        public int PopulationScore { get; set; }
        public int BuildingScore { get; set; }
        public int ArmyScore { get; set; }
        public int ResearchScore { get; set; }
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        public virtual Round Round { get; set; }
        public int RoundId { get; set; }

    }
}
