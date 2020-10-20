using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class Building : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int CoralPerRound { get; set; }
        public int PearlPerRound { get; set; }
        public int PopulationOnConstructionFinished { get; set; }
        public int GarrisonOnConstructionFinished { get; set; }

        public virtual ICollection<CountryBuildingConnector> Countries { get; set; }
    }
}
