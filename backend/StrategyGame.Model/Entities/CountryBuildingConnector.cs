using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class CountryBuildingConnector : EntityBase
    {
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        public virtual Building Building { get; set; }
        public int BuildingId { get; set; }
        public virtual Round BuildStartedRound { get; set; }
        public int BuildStartedRoundId { get; set; }
        public int RoundsLeftUntilCompletion { get; set; }

        public bool IsComplete { get => RoundsLeftUntilCompletion == 0; }
    }
}
