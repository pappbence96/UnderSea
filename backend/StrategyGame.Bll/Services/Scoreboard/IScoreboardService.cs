using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Scoreboard
{
    public interface IScoreboardService
    {
        Task<IEnumerable<ScoreboardEntry>> GetScoreboard(DateTime? selectedMoment);
    }
}