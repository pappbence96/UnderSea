using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class Research : EntityBase
    {
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual double CoralMultiplier { get; set; } = 1;
        public virtual double AttackMultiplier { get; set; } = 1;
        public virtual double DefenseMultiplier { get; set; } = 1;
        public virtual double TaxMultiplier { get; set; } = 1;

        public virtual ICollection<CountryResearchConnector> Countries { get; set; }
    }
}
