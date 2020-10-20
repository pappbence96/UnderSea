using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Model.Entities
{
    public class CountryResearchConnector : EntityBase
    {
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        public virtual Research Research { get; set; }
        public int ResearchId { get; set; }
        public virtual Round ResearchStartedRound { get; set; }
        public int ResearchStartedRoundId { get; set; }
        public int RoundsLeftUntilCompletion { get; set; }

        public bool IsComplete { get => RoundsLeftUntilCompletion == 0; }
    }
}
