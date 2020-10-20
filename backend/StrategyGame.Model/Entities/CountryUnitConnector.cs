using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class CountryUnitConnector : EntityBase
    {
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        public virtual Unit Unit { get; set; }
        public int UnitId { get; set; }
        public int TotalCount { get; set; }
        public int InCombat { get; set; }
    }
}
