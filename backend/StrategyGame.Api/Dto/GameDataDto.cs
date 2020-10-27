using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.Api.Dto
{
    public class GameDataDto
    {
        public int RoundNumber { get; set; }
        public int ScoreboardPosition { get; set; }
        public string Username { get; set; }
        public string CountryName { get; set; }
        public Dictionary<int, int> Units { get; set; }
        public Dictionary<int, int> Buildings { get; set; }
        public int Pearl { get; set; }
        public int PearlPerRound { get; set; }
        public int Coral { get; set; }
        public int CoralPerRound { get; set; }
        public int Population { get; internal set; }
    }
}
