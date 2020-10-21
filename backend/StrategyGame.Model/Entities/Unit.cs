using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class Unit : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Supply { get; set; }
        public int Pay { get; set; }
        public int ScoreboardValue { get; set; }
        

        public virtual ICollection<CountryUnitConnector> Countries { get; set; }

    }
}
