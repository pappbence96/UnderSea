using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using StrategyGame.Api.Dto;
using StrategyGame.Api.Services.UserAccessor;
using StrategyGame.Bll.Services.Building;
using StrategyGame.Bll.Services.GameEngineService;
using StrategyGame.Bll.Services.Resource;
using StrategyGame.Bll.Services.Scoreboard;
using StrategyGame.Bll.Services.Units;
using StrategyGame.Bll.Services.User;

namespace StrategyGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MainController : ControllerBase
    {
        private readonly IUserAccessor userAccessor;
        private readonly IUserService userService;
        private readonly IUnitsService unitsService;
        private readonly IBuildingService buildingService;
        private readonly IResourceService resourceService;
        private readonly IGameEngineService gameEngineService;
        private readonly IScoreboardService scoreboardService;

        public MainController(IUserAccessor userAccessor, IUnitsService unitsService, IBuildingService buildingService, IUserService userService, IResourceService resourceService, IGameEngineService gameEngineService, IScoreboardService scoreboardService)
        {
            this.userAccessor = userAccessor;
            this.unitsService = unitsService;
            this.buildingService = buildingService;
            this.userService = userService;
            this.resourceService = resourceService;
            this.gameEngineService = gameEngineService;
            this.scoreboardService = scoreboardService;
        }

        [HttpGet]
        public async Task<ActionResult<GameDataDto>> GetGameData()
        {
            var user = await userService.GetUserById(userAccessor.UserId);
            var units = (await unitsService
                .GetAllUnitsOfCountry(user.Country.Id))
                .ToDictionary(u => u.Unit.Id, u => u.TotalCount);
            var buildings = (await buildingService
                .GetAllBuildingsOfCountry(user.Country.Id)).ToList()
                .GroupBy(b => b.BuildingId, b => b)
                .ToDictionary(g => g.Key, g => g.Count());
            return new GameDataDto
            {
                RoundNumber = (await gameEngineService.GetActiveRoundAsync()).Number,
                ScoreboardPosition = (await scoreboardService.GetLatestScoreboardForCountry(user.Country.Id))?.Position ?? -1,
                Username = user.UserName,
                CountryName = user.Country.Name,
                Units = units,
                Buildings = buildings,
                Coral = user.Country.Coral,
                CoralPerRound = await resourceService.GetCoralIncrementOfCountry(user.Country.Id),
                Pearl = user.Country.Pearl,
                PearlPerRound = await resourceService.GetPearlIncrementOfCountry(user.Country.Id),
                Population = user.Country.Population
            };
        }

        [HttpGet("tick")]
        public async Task<ActionResult> TickGame()
        {
            await gameEngineService.PerformTick();
            return Ok();
        }
    }
}
