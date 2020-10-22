using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class Country : EntityBase
    {
        public virtual ApplicationUser User { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int Population { get; set; } = 100;
        public int Pearl { get; set; }
        public int Coral { get; set; }
        public int Garrison { get; set; } = 20;

        public virtual ICollection<CountryBuildingConnector> Buildings { get; set; }
        public virtual ICollection<CountryResearchConnector> Researches { get; set; }
        public virtual ICollection<CountryUnitConnector> Units { get; set; }
        public virtual ICollection<Combat> OutgoingAttacks { get; set; }
        public virtual ICollection<Combat> IncomingAttacks { get; set; }
        public virtual ICollection<ScoreboardEntry> ScoreboardEntries { get; set; }
    }
}
