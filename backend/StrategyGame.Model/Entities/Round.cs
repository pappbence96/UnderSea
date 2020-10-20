using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class Round : EntityBase
    {
        public int Number { get; set; }
        public DateTime TickedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CountryResearchConnector> StartedResearches { get; set; }
        public virtual ICollection<CountryBuildingConnector> StartedBuilds { get; set; }
        public virtual ICollection<Combat> ActiveCombats { get; set; }
    }
}
