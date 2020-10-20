using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class Combat : EntityBase
    {
        public virtual Country Attacker { get; set; }
        public int AttackerId { get; set; }
        public virtual Country Defender { get; set; }
        public int DefenderId { get; set; }
        public virtual Round Round { get; set; }
        public int RoundId { get; set; }
        public bool IsConcluded { get; set; }

        public virtual ICollection<CombatUnitConnector> Units { get; set; }
    }
}
