using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class CombatUnitConnector : EntityBase
    {
        public virtual Unit Unit { get; set; }
        public int UnitId { get; set; }
        public virtual Combat Combat { get; set; }
        public int CombatId { get; set; }
        public int Count { get; set; }
    }
}
