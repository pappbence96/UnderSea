using Microsoft.EntityFrameworkCore;
using StrategyGame.Dal;
using StrategyGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Bll.Services.Scoreboard
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly UnderseaDbContext context;

        public ScoreboardService(UnderseaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ScoreboardEntry>> GetScoreboard(DateTime? selectedMoment)
        {
            Round selectedRound;
            if (selectedMoment.HasValue)
            {
                //     |----------|----------|----------|----_-----|
                //     ^          ^          ^          ^    |     ^
                //     R1         R2         R3         R4  SB>R4  R5
                // Select the last round that was ticked before the specified moment.
                selectedRound = (await context.Rounds
                    .Include(r => r.ScoreboardEntries)
                    .Where(r => r.TickedAt < selectedMoment)
                    .OrderByDescending(r => r.TickedAt)
                    .FirstOrDefaultAsync())
                    ?? throw new InvalidOperationException("No scoreboard entries were recorded after the selected point in time.");
            }
            else
            {
                selectedRound = await context.Rounds.Include(r => r.ScoreboardEntries).SingleAsync(r => r.IsActive);
            }
            return selectedRound.ScoreboardEntries;
        }

        public async Task<ScoreboardEntry> GetLatestScoreboardForCountry(int countryId)
        {
            var lastRound = await context.Rounds.Include(r => r.ScoreboardEntries).OrderBy(r => r.Number).LastOrDefaultAsync(r => !r.IsActive);
            return lastRound.ScoreboardEntries.SingleOrDefault(e => e.CountryId == countryId);
        }
    }
}
