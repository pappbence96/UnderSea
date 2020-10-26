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

        public MainController(IUserAccessor userAccessor, IUnitsService unitsService, IBuildingService buildingService, IUserService userService)
        {
            this.userAccessor = userAccessor;
            this.unitsService = unitsService;
            this.buildingService = buildingService;
            this.userService = userService;
        }

        [HttpGet("test")]
        public async Task<ActionResult<GameDataDto>> GetGameData()
        {
            var user = await userService.GetUserById(userAccessor.UserId);
            var units = (await unitsService
                .GetAllUnitsOfCountry(user.Country.Id))
                .ToDictionary(u => u.Id, u => u.TotalCount);
            var buildings = (await buildingService
                .GetAllBuildingsOfCountry(user.Country.Id)).ToList()
                .GroupBy(b => b.BuildingId, b => b)
                .ToDictionary(g => g.Key, g => g.Count());
            return new GameDataDto
            {
                RoundNumber = -1,
                Username = user.UserName,
                CountryName = user.Country.Name,
                Units = units,
                Buildings = buildings,
                Coral = 0,
                CoralPerRound = 0,
                Pearl = 0,
                PearlPerRound = 0
            };
        }
    }
}
